import time
from pijuice import PiJuice  # Import pijuice module


def juice_value(property, value):
    if value['error'] == 'NO_ERROR':
        print("{0} = {1}".format(property, value['data']))


def juice_status(value):
    if value['error'] == 'NO_ERROR':
        print("Battery = {0}".format(value['data']['battery']))
        print("Power 5v Input = {0}".format(value['data']['powerInput5vIo']))
        print("Is fault = {0}".format(value['data']['isFault']))
        print("Is Button = {0}".format(value['data']['isButton']))
        print("Power Input = {0}".format(value['data']['powerInput']))


def main():
    # Instantiate PiJuice interface object
    pijuice = PiJuice(1, 0x14)

    while True:
        battery_charge = pijuice.status.GetChargeLevel()
        juice_value('battery_charge', battery_charge)

        battery_temperature = pijuice.status.GetBatteryTemperature()
        juice_value('battery_temperature', battery_temperature)

        battery_voltage = pijuice.status.GetBatteryVoltage()
        juice_value('battery_voltage', battery_voltage)

        io_voltage = pijuice.status.GetIoVoltage()
        juice_value('io_voltage', io_voltage)

        io_current = pijuice.status.GetIoCurrent()
        juice_value('io_current', io_current)

        status = pijuice.status.GetStatus()
        juice_status(status)

        print('-------------------------------------')
        time.sleep(1)


if __name__ == "__main__":
    main()
