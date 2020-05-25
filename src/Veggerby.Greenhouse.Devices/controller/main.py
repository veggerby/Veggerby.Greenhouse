import requests
import threading
import time

from menu import MenuItem, RootMenuItem, ExitMenuItem, SensorMenuItem, PropertyMenuItem, MeasurementMenuItem, PhotoMenuItem
from controller import Controller


def process():
    try:
        control = Controller()
        control.print_message('Loading...')

        response = requests.get(
            'https://veggerby-greenhouse.azurewebsites.net/api/properties')
        properties = response.json()

        properties_item = MenuItem('properties', 'Properties')

        for property in properties:
            properties_item.add_child(PropertyMenuItem(property))

        response = requests.get(
            'https://veggerby-greenhouse.azurewebsites.net/api/sensors')
        sensors = response.json()

        sensors_item = MenuItem('senors', 'Sensors')

        for sensor in sensors:
            sensors_item.add_child(SensorMenuItem(sensor))

        measurement_item = MeasurementMenuItem()
        photo_item = PhotoMenuItem()
        exit_item = ExitMenuItem()

        root = RootMenuItem()
        root.add_child(properties_item)
        root.add_child(sensors_item)
        root.add_child(measurement_item)
        root.add_child(photo_item)
        root.add_child(exit_item)

        control.run(root)
    finally:
        control.off()


thread = threading.Thread(target=process)
thread.daemon = True
thread.start()

while thread.isAlive():
    try:
        time.sleep(1)

    except KeyboardInterrupt:
        break
