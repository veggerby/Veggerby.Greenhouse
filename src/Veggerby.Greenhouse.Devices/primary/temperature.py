import time, json, logging, socket, os

import RPi.GPIO as GPIO

import board
import board
import adafruit_dht

from datetime import datetime
from azure.eventhub import EventHubProducerClient, EventData

logger = logging.getLogger("azure")
device = socket.gethostname()

# define channels

DHT_SENSOR = adafruit_dht.DHTBase(False, board.D20, 10000)

# initialize GPIO

GPIO.setmode(GPIO.BCM)
GPIO.setwarnings(False)
GPIO.setup(20, GPIO.IN)

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

    print('sensor {0}, {1} = {2}'.format(sensor, property, value))
    batch.add(EventData(json.dumps(data)))

try:
    with client:
        start_time = time.time()

        batch = client.create_batch()
        now = datetime.utcnow()

        temperature = DHT_SENSOR.temperature
        humidity = DHT_SENSOR.humidity

        add_value(batch, device, 'dht22', now, 'temperature', temperature)
        add_value(batch, device, 'dht22', now, 'humidity', humidity)

        client.send_batch(batch)

        logger.info("Batch sent")
except:
    raise
finally:
    GPIO.cleanup()
