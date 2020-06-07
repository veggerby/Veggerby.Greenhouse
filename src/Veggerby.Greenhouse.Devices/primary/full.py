import time, json, logging, socket, os
import Adafruit_DHT

import RPi.GPIO as GPIO

import busio, digitalio, board
import adafruit_mcp3xxx.mcp3008 as MCP
from adafruit_mcp3xxx.analog_in import AnalogIn

from datetime import datetime
from azure.eventhub import EventHubProducerClient, EventData

from pijuice import PiJuice # Import pijuice module

logger = logging.getLogger("azure")
device = socket.gethostname()

# define channels

led_channel = 21
soil_channel = 19

DHT_SENSOR = Adafruit_DHT.DHT22
DHT_PIN = 20

soil_channel = MCP.P5

# initialize GPIO

GPIO.setmode(GPIO.BCM)
GPIO.setwarnings(False)
GPIO.setup(led_channel, GPIO.OUT)

# initialize Azure Event Hub

# Address can be in either of these formats:
# "amqps://<URL-encoded-SAS-policy>:<URL-encoded-SAS-key>@<namespace>.servicebus.windows.net/eventhub"
# "amqps://<namespace>.servicebus.windows.net/<eventhub>"
# SAS policy and key are not required if they are encoded in the URL

connection_str = os.getenv('AZURE_EVENTHUB_CONNECTION_STRING')
eventhub_name = "demo"

GPIO.output(led_channel, GPIO.HIGH)

if not connection_str:
    raise ValueError("No EventHubs URL supplied.")

# Create Event Hubs client
client = EventHubProducerClient.from_connection_string(
    connection_str, eventhub_name=eventhub_name)

# create the spi bus
spi = busio.SPI(clock=board.SCK, MISO=board.MISO, MOSI=board.MOSI)

# create the cs (chip select)
cs = digitalio.DigitalInOut(board.D5)

# create the mcp object
mcp = MCP.MCP3008(spi, cs)

# Instantiate PiJuice interface object
pijuice = PiJuice(1, 0x14)


def add_value(batch, device, sensor, time, property, value):
    data = {'device': device, 'sensor': sensor,
            'time': now.isoformat() + 'Z', 'property': property, 'value': value}
    batch.add(EventData(json.dumps(data)))

def add_juice_value(batch, device, sensor, time, property, value):
    if value['error'] == 'NO_ERROR':
        add_value(batch, device, sensor, time, property, value['data'])

try:
    with client:
        start_time = time.time()
        GPIO.output(led_channel, GPIO.HIGH)

        batch = client.create_batch()
        now = datetime.utcnow()

        humidity, temperature = Adafruit_DHT.read_retry(DHT_SENSOR, DHT_PIN)
        soil_humidity = AnalogIn(mcp, soil_channel).value

        battery_charge = pijuice.status.GetChargeLevel()
        add_juice_value(batch, device, 'pijuice', now, 'battery_charge', battery_charge)

        battery_temperature = pijuice.status.GetBatteryTemperature()
        add_juice_value(batch, device, 'pijuice', now, 'battery_temperature', battery_temperature)

        battery_voltage = pijuice.status.GetBatteryVoltage()
        add_juice_value(batch, device, 'pijuice', now, 'battery_voltage', battery_voltage)

        io_voltage = pijuice.status.GetIoVoltage()
        add_juice_value(batch, device, 'pijuice', now, 'io_voltage', io_voltage)

        io_current = pijuice.status.GetIoCurrent()
        add_juice_value(batch, device, 'pijuice', now, 'io_current', io_current)

        add_value(batch, device, 'dht22', now, 'temperature', temperature)
        add_value(batch, device, 'dht22', now, 'humidity', humidity)
        add_value(batch, device, 'sf_soil', now,
                  'soil_humidity', soil_humidity)

        client.send_batch(batch)

        logger.info("Batch sent")

        GPIO.output(led_channel, GPIO.LOW)
except:
    raise
finally:
    GPIO.cleanup()
