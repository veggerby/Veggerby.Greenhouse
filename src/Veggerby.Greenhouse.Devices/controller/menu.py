import requests, socket
from camera import take_and_upload_photo

class MenuItem(object):
    def __init__(self, id, name):
        self.id = id
        self.name = name
        self.parent = self
        self.selected = 0
        self.items = []

    def run(self, state, controller):
        print('Selected: ' + self.name)

    def add_child(self, item):
        self.items.append(item)
        item.parent = self

    def has_children(self):
        return len(self.items) > 0

    def print_menu(self, indent):
        print(indent + self.name)
        for child in self.items:
            child.print_menu(indent + '  ')

    @property
    def selected_item(self):
        return self.items[self.selected]

    def next(self):
        self.selected = (self.selected + 1) % len(self.items)
        return self.selected_item

    def prev(self):
        self.selected = (self.selected - 1) % len(self.items)
        return self.selected_item

    def is_running(self):
        if self.parent == self:
            return False
        else:
            return self.parent.is_running()


class ExitMenuItem(MenuItem):
    def __init__(self):
        super().__init__('exit', 'Exit')

    def run(self, state, controller):
        state.exit()


class RootMenuItem(MenuItem):
    def __init__(self):
        super().__init__('root', 'Main')


class PropertyMenuItem(MenuItem):
    def __init__(self, property):
        self.property = property
        super().__init__(property['id'], property['name'])

    def run(self, state, controller):
        state.selected_property = self.property
        state.selected_measurement = None
        super().run(state, controller)


class SensorMenuItem(MenuItem):
    def __init__(self, sensor):
        self.sensor = sensor
        super().__init__(sensor['id'], sensor['name'])

    def run(self, state, controller):
        state.selected_sensor = self.sensor
        state.selected_measurement = None
        super().run(state, controller)


class MeasurementMenuItem(MenuItem):
    def __init__(self):
        super().__init__("measure", "Get measure")

    def run(self, state, controller):
        response = requests.get(
            'https://veggerby-greenhouse.azurewebsites.net/api/measurements?p={0}&s={1}&c=1'.format(state.selected_property['id'], state.selected_sensor['key']))
        measurement = response.json()
        state.selected_measurement = measurement[0]['measurements'][0]

        super().run(state, controller)


class PhotoMenuItem(MenuItem):
    def __init__(self):
        super().__init__("photo", "Take photo")

    def run(self, state, controller):
        take_and_upload_photo(socket.gethostname(), True)
        super().run(state, controller)
