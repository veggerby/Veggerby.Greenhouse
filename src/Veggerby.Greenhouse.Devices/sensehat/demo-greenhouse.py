import logging
import time, os, json, socket

from datetime import datetime
from sense_hat import SenseHat
from azure.eventhub import EventHubProducerClient, EventData

logger = logging.getLogger("azure")
device = socket.gethostname()
sensor = 'sensehat'

# initialize SenseHat
sense = SenseHat()
sense.clear()

sense.low_light = True
sense.set_rotation(180)

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


def add_value(batch, device, sensor, time, property, value):
    data = {'device': device, 'sensor': sensor,
            'time': now.isoformat() + 'Z', 'property': property, 'value': value}
    batch.add(EventData(json.dumps(data)))


try:
    with client:
        batch = client.create_batch()
        now = datetime.utcnow()

        temperature = sense.get_temperature()
        pressure = sense.get_pressure()
        humidity = sense.get_humidity()
        temperature2 = sense.get_temperature_from_pressure()

        add_value(batch, device, sensor, now, 'temperature', temperature)
        add_value(batch, device, sensor + '_h', now, 'temperature', temperature2)
        add_value(batch, device, sensor + '_h', now, 'humidity', humidity)
        add_value(batch, device, sensor, now, 'pressure', pressure)

        client.send_batch(batch)
except:
    raise
finally:
    sense.clear()
