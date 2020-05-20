class State(object):
    def __init__(self):
        self._property = None
        self._sensor = None
        self._measurement = None
        self._running = True

    def to_string(self):
        if (self.selected_measurement):
            return '{0:.2f} {1}'.format(self.selected_measurement['averageValue'], self.selected_property['unit'])

        property = ''
        if self.selected_property:
            property = "p={0}".format(self.selected_property['id'])

        sensor = ''
        if self.selected_sensor:
            sensor = "s={0}".format(self.selected_sensor['id'])

        return property + sensor

    @property
    def selected_property(self):
        return self._property

    @selected_property.setter
    def selected_property(self, val):
        self._property = val

    @property
    def selected_sensor(self):
        return self._sensor

    @selected_sensor.setter
    def selected_sensor(self, val):
        self._sensor = val

    @property
    def selected_measurement(self):
        return self._measurement

    @selected_measurement.setter
    def selected_measurement(self, val):
        self._measurement = val

    @property
    def is_running(self):
        return self._running

    def exit(self):
        self._running = False
