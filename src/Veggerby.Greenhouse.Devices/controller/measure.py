import time, json, logging, socket, os

from datetime import datetime
from azure.eventhub import EventHubProducerClient, EventData

from pijuice import PiJuice # Import pijuice module

logger = logging.getLogger("azure")
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

any_messages = False

def add_value(batch, device, sensor, time, property, value):
    data = {'device': device, 'sensor': sensor,
            'time': now.isoformat() + 'Z', 'property': property, 'value': value}
    batch.add(EventData(json.dumps(data)))

def add_juice_value(batch, device, sensor, time, property, value):
    if value['error'] == 'NO_ERROR':
        add_value(batch, device, sensor, time, property, value['data'])
        return True

    return False

try:
    with client:
        start_time = time.time()

        batch = client.create_batch()
        now = datetime.utcnow()

        battery_charge = pijuice.status.GetChargeLevel()
        any_messages = add_juice_value(batch, device, 'pijuice', now, 'battery_charge', battery_charge) or any_messages

        battery_temperature = pijuice.status.GetBatteryTemperature()
        any_messages = add_juice_value(batch, device, 'pijuice', now, 'battery_temperature', battery_temperature) or any_messages

        battery_voltage = pijuice.status.GetBatteryVoltage()
        any_messages = add_juice_value(batch, device, 'pijuice', now, 'battery_voltage', battery_voltage) or any_messages

        io_voltage = pijuice.status.GetIoVoltage()
        any_messages = add_juice_value(batch, device, 'pijuice', now, 'io_voltage', io_voltage) or any_messages

        io_current = pijuice.status.GetIoCurrent()
        any_messages = add_juice_value(batch, device, 'pijuice', now, 'io_current', io_current) or any_messages

        if any_messages:
            client.send_batch(batch)
        else:
            print('No messages...')

        logger.info("Batch sent")
except:
    raise
finally:
     logger.info("Done")