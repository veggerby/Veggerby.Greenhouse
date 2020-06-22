import time
import json
import logging
import socket
import os

import board
import board
import adafruit_dht

import RPi.GPIO as GPIO

from datetime import datetime
from azure.eventhub import EventHubProducerClient, EventData

from opencensus.trace.samplers import AlwaysOnSampler
from opencensus.ext.azure.trace_exporter import AzureExporter
from opencensus.trace import config_integration
from opencensus.trace.tracer import Tracer
from opencensus.ext.azure.log_exporter import AzureLogHandler
from opencensus.ext.azure import metrics_exporter


def add_value(batch, device, sensor, time, property, value):
    data = {'device': device, 'sensor': sensor,
            'time': time.isoformat() + 'Z', 'property': property, 'value': value}

    print('sensor {0}, {1} = {2}'.format(sensor, property, value))
    batch.add(EventData(json.dumps(data)))


def main():
    GPIO.cleanup()

    exporter = metrics_exporter.new_metrics_exporter(
        connection_string=ai_connection_str)

    tracer = Tracer(
        exporter=AzureExporter(connection_string=ai_connection_str),
        sampler=AlwaysOnSampler(),
    )

    with tracer.span(name="measurement") as span:
        DHT_SENSOR = adafruit_dht.DHT22(board.D20)

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

        with client:
            batch = client.create_batch()

            time.sleep(2.5)
            with span.span(name="temperature"):
                now_t = datetime.utcnow()
                temperature = DHT_SENSOR.temperature
                add_value(batch, device, 'dht22', now_t,
                          'temperature', temperature)

            time.sleep(2.5)
            with span.span(name="humidity"):
                now_h = datetime.utcnow()
                humidity = DHT_SENSOR.humidity
                add_value(batch, device, 'dht22',
                          now_h, 'humidity', humidity)

            with tracer.span(name="sending messages") as span:
                client.send_batch(batch)


if __name__ == "__main__":
    try:
        config_integration.trace_integrations(['logging'])

        ai_connection_str = os.getenv(
            'AZURE_APPLICATION_INSIGHTS_CONNECTION_STRING')

        device = socket.gethostname()


        if not ai_connection_str:
            raise ValueError("No AI connection supplied.")

        logger = logging.getLogger(__name__)

        handler = AzureLogHandler(connection_string=ai_connection_str)
        handler.setFormatter(logging.Formatter(
            '%(traceId)s %(spanId)s %(message)s'))
        logger.addHandler(handler)

        main()
    except Exception:
        logger.exception('Error during measurement',
                         extra={'device': device})
    finally:
        GPIO.cleanup()
