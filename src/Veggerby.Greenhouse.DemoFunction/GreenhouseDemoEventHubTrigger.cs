using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Veggerby.Greenhouse.Core;

namespace Veggerby.Greenhouse
{
    public class GreenhouseDemoEventHubTrigger
    {
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

        private async Task EnsureDevice(Measurement measurement)
        {
            var device = await _context.Devices.FindAsync(measurement.DeviceId);
            if (device == null)
            {
                device = new Device
                {
                    DeviceId = measurement.DeviceId,
                    Name = measurement.Device?.Name ?? measurement.DeviceId
                };

                await _context.Devices.AddAsync(device);
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
                    var measurement = JsonSerializer.Deserialize<Measurement>(messageBody);

                    if (string.IsNullOrEmpty(measurement.DeviceId))
                    {
                        measurement.DeviceId = "(default)";
                    }

                    await EnsureDevice(measurement);

                    _log.LogInformation($"Time\t\t: {measurement.TimeUtc.ToLocalTime()}\nTemperature\t: {measurement.Temperature}\nHumidity\t\t: {measurement.Humidity}\nPressure\t\t: {measurement.Pressure}");

                    _context.Measurements.Add(measurement);
                    await Task.Yield();
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