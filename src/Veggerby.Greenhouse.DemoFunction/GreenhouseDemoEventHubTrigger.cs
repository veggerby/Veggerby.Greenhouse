using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Veggerby.Greenhouse.Core;
using Veggerby.Greenhouse.Core.Messages;

namespace Veggerby.Greenhouse
{
    public class GreenhouseDemoEventHubTrigger
    {
        private const double DefaultTolerance = 0.01;

        private readonly GreenhouseContext _context;
        private readonly ILogger<GreenhouseDemoEventHubTrigger> _log;
        public GreenhouseDemoEventHubTrigger(GreenhouseContext context, ILogger<GreenhouseDemoEventHubTrigger> log)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (log is null)
            {
                throw new ArgumentNullException(nameof(log));
            }
            _context = context;
            _log = log;
        }

        private async Task<Device> EnsureDevice(Signal signal)
        {
            var device = await _context.Devices.FindAsync(signal.Device);
            if (device == null)
            {
                device = new Device
                {
                    DeviceId = signal.Device,
                    Name = signal.Device
                };

                await _context.Devices.AddAsync(device);
            }

            return device;
        }

        private async Task<Sensor> EnsureSensor(Device device, Signal signal)
        {
            var sensor = await _context.Sensors.FindAsync(signal.Device, signal.Sensor);
            if (sensor == null)
            {
                sensor = new Sensor
                {
                    DeviceId = signal.Device,
                    SensorId = signal.Sensor,
                    Name = $"{signal.Sensor}@{signal.Device}"
                };

                await _context.Sensors.AddAsync(sensor);
            }

            return sensor;
        }

        private async Task<Property> EnsureProperty(Signal signal)
        {
            var property = await _context.Properties.FindAsync(signal.Property);
            if (property == null)
            {
                property = new Property
                {
                    PropertyId = signal.Property,
                    Name = signal.Property
                };

                await _context.Properties.AddAsync(property);
            }

            return property;
        }

        private async Task<Measurement> GetLatestMeasurement(Sensor sensor, Property property)
        {
            var latest = await _context
                .Measurements
                .Where(x => x.DeviceId == sensor.DeviceId && x.SensorId == sensor.SensorId && x.PropertyId == property.PropertyId)
                .OrderByDescending(x => x.CreatedUtc)
                .FirstOrDefaultAsync();

            return latest;
        }

        private async Task SaveMeasurement(Signal signal)
        {
            if (signal.Value == null || string.IsNullOrEmpty(signal.Property))
            {
                return;
            }

            if (string.IsNullOrEmpty(signal.Device))
            {
                signal.Device = "(default)";
            }

            if (string.IsNullOrEmpty(signal.Sensor))
            {
                signal.Sensor = "(default)";
            }

            var device = await EnsureDevice(signal);
            var sensor = await EnsureSensor(device, signal);
            var property = await EnsureProperty(signal);

            if (!device.Enabled || !sensor.Enabled)
            {
                return;
            }

            var latest = await GetLatestMeasurement(sensor, property);

            //var pow = Math.Pow(10, property.Decimals);
            //signal.Value = Math.Round(signal.Value.Value * pow) / pow;

            if (latest?.Value != null && signal.Value != null && Math.Abs(latest.Value.Value - signal.Value.Value) < (property.Tolerance ?? DefaultTolerance))
            {
                latest.LastTimeUtc = signal.TimeUtc;
                latest.SumValue += signal.Value.Value;
                latest.MinValue = Math.Min(signal.Value.Value, latest.MinValue.Value);
                latest.MaxValue = Math.Max(signal.Value.Value, latest.MaxValue.Value);
                latest.Count++;
                latest.UpdatedUtc = DateTime.UtcNow;
            }
            else
            {
                var now = DateTime.UtcNow;

                var measurement = new Measurement
                {
                    DeviceId = sensor.DeviceId,
                    Device = device,
                    SensorId = sensor.DeviceId,
                    Sensor = sensor,
                    PropertyId = property.PropertyId,
                    FirstTimeUtc = signal.TimeUtc,
                    LastTimeUtc = signal.TimeUtc,
                    Value = signal.Value,
                    SumValue = signal.Value,
                    Count = 1,
                    MinValue = signal.Value,
                    MaxValue = signal.Value,
                    CreatedUtc = now,
                    UpdatedUtc = now
                };

                await _context.Measurements.AddAsync(measurement);
            }
        }

        [FunctionName("GreenhouseDemoEventHubTrigger")]
        public async Task Run([EventHubTrigger("demo", Connection = "veggerbygreenhouse_demo_EVENTHUB")] EventData[] events)
        {
            var exceptions = new List<Exception>();

            foreach (EventData eventData in events)
            {
                try
                {
                    string messageBody = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);

                    // Replace these two lines with your processing logic.
                    _log.LogInformation($"C# Event Hub trigger function processed a message: {messageBody}");
                    var signal = JsonSerializer.Deserialize<Signal>(messageBody);

                    _log.LogInformation($"Time\t\t: {signal.TimeUtc.ToLocalTime()}\nProperty\t: {signal.Property}\nValue\t\t: {signal.Value}");

                    await SaveMeasurement(signal);
                }
                catch (Exception e)
                {
                    // We need to keep processing the rest of the batch - capture this exception and continue.
                    // Also, consider capturing details of the message that failed processing so it can be processed again later.
                    exceptions.Add(e);
                }
            }

            await _context.SaveChangesAsync();

            // Once processing of the batch is complete, if any messages in the batch failed processing throw an exception so that there is a record of the failure.

            if (exceptions.Count > 1)
            {
                throw new AggregateException(exceptions);
            }

            if (exceptions.Count == 1)
            {
                throw exceptions.Single();
            }
        }
    }
}