import time
import json
import logging
import socket
import os

from datetime import datetime
from azure.eventhub import EventHubProducerClient, EventData

from pijuice import PiJuice  # Import pijuice module

from opencensus.trace.samplers import AlwaysOnSampler
from opencensus.ext.azure.trace_exporter import AzureExporter
from opencensus.trace import config_integration
from opencensus.trace.tracer import Tracer
from opencensus.ext.azure.log_exporter import AzureLogHandler
from opencensus.ext.azure import metrics_exporter


def main():
    config_integration.trace_integrations(['logging'])

    ai_connection_str = os.getenv(
        'AZURE_APPLICATION_INSIGHTS_CONNECTION_STRING')

    if not ai_connection_str:
        raise ValueError("No AI connection supplied.")

    logger = logging.getLogger(__name__)

    handler = AzureLogHandler(connection_string=ai_connection_str)
    handler.setFormatter(logging.Formatter(
        '%(traceId)s %(spanId)s %(message)s'))
    logger.addHandler(handler)

    exporter = metrics_exporter.new_metrics_exporter(
        connection_string=ai_connection_str)

    tracer = Tracer(
        exporter=AzureExporter(connection_string=ai_connection_str),
        sampler=AlwaysOnSampler(),
    )

    device = socket.gethostname()

    with tracer.span(name="measurement") as span:
        device = socket.gethostname()

        # initialize Azure Event Hub

        # Address can be in either of these formats:
        # "amqps://<URL-encoded-SAS-policy>:<URL-encoded-SAS-key>@<namespace>.servicebus.windows.net/eventhub"
        # "amqps://<namespace>.servicebus.windows.net/<eventhub>"
        # SAS policy and key are not required if they are encoded in the URL

        connection_str = os.getenv('AZURE_EVENTHUB_CONNECTION_STRING')
        eventhub_name = "demo"

        if not connection_str:
            raise ValueError("No EventHubs URL supplied.")

        # Create Event Hubs client
        client = EventHubProducerClient.from_connection_string(
            connection_str, eventhub_name=eventhub_name)

        # Instantiate PiJuice interface object
        pijuice = PiJuice(1, 0x14)

        def add_value(batch, device, sensor, time, property, value):
            data = {'device': device, 'sensor': sensor,
                    'time': now.isoformat() + 'Z', 'property': property, 'value': value}
            batch.add(EventData(json.dumps(data)))

        def add_juice_value(batch, device, sensor, time, property, value):
            if value['error'] == 'NO_ERROR':
                add_value(batch, device, sensor, time, property, value['data'])

        # https://github.com/PiSupply/PiJuice/tree/master/Software#pijuice-status
        def add_juice_value_charging(batch, device, sensor, time, property, value):
            if value['error'] == 'NO_ERROR':
                is_charging = 0 # NORMAL

                if value['data']['battery'] == 'CHARGING_FROM_IN' or value['data']['battery'] == 'CHARGING_FROM_5V_IO':
                    is_charging = 1

                if value['data']['battery'] == 'NOT_PRESENT':
                    is_charging = -1

                add_value(batch, device, sensor, time, property, is_charging)

        # https://github.com/PiSupply/PiJuice/tree/master/Software#pijuice-status
        def add_juice_value_power_input(batch, device, sensor, time, property, key, value):
            if value['error'] == 'NO_ERROR':
                power_input = -1

                if value['data'][key] == 'NOT_PRESENT':
                    power_input = -1
                elif value['data'][key] == 'BAD':
                    power_input = 0
                elif value['data'][key] == 'WEAK':
                    power_input = 1
                elif value['data'][key] == 'PRESENT':
                    power_input = 2

                add_value(batch, device, sensor, time, property, power_input)

        try:
            with client:
                start_time = time.time()

                batch = client.create_batch()
                now = datetime.utcnow()

                battery_charge = pijuice.status.GetChargeLevel()
                add_juice_value(batch, device, 'pijuice', now,
                                'battery_charge', battery_charge)

                battery_temperature = pijuice.status.GetBatteryTemperature()
                add_juice_value(batch, device, 'pijuice', now,
                                'battery_temperature', battery_temperature)

                battery_voltage = pijuice.status.GetBatteryVoltage()
                add_juice_value(batch, device, 'pijuice', now,
                                'battery_voltage', battery_voltage)

                battery_current = pijuice.status.GetBatteryCurrent()
                add_juice_value(batch, device, 'pijuice', now,
                                'battery_current', battery_current)

                io_voltage = pijuice.status.GetIoVoltage()
                add_juice_value(batch, device, 'pijuice',
                                now, 'io_voltage', io_voltage)

                io_current = pijuice.status.GetIoCurrent()
                add_juice_value(batch, device, 'pijuice',
                                now, 'io_current', io_current)

                status = pijuice.status.GetStatus()
                add_juice_value_charging(batch, device, 'pijuice',
                                         now, 'charging', status)
                add_juice_value_power_input(batch, device, 'pijuice',
                                            now, 'power_input_usb', 'powerInput', status)
                add_juice_value_power_input(batch, device, 'pijuice',
                                            now, 'power_input_gpio', 'powerInput5vIo', status)

                client.send_batch(batch)
        except Exception:
            logger.exception('Error during measurement',
                             extra={'device': device})


if __name__ == "__main__":
    main()
