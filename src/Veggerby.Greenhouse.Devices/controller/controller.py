import board
import busio
import time

import adafruit_character_lcd.character_lcd_rgb_i2c as character_lcd

from state import State


class Controller(object):
    def __init__(self):
        # Modify this if you have a different sized Character LCD
        lcd_columns = 16
        lcd_rows = 2

        # Initialise I2C bus.
        i2c = busio.I2C(board.SCL, board.SDA)

        # Initialise the LCD class
        self._lcd = character_lcd.Character_LCD_RGB_I2C(
            i2c, lcd_columns, lcd_rows)

        self._state = State()

    def print_message(self, message):
        self.on()
        self._lcd.clear()
        self._lcd.message = message

    def on(self):
        if (self._lcd.display):
            return

        self._lcd.display = True

        self._lcd.color = [100, 0, 0]
        self._lcd.clear()

    def off(self):
        if self._lcd.display:
            self._lcd.clear()
            self._lcd.display = False

    def print_menu(self, menu):
        self.print_message("{0}\n{1}".format(
            menu.selected_item.name, self._state.to_string()))

    def run(self, root):
        self.on()
        menu = root

        self.print_menu(menu)
        while True:
            if self._lcd.right_button:
                if menu.selected_item.has_children():
                    menu = menu.selected_item

                self.print_menu(menu)

            elif self._lcd.up_button:
                menu.prev()
                self.print_menu(menu)

            elif self._lcd.down_button:
                menu.next()
                self.print_menu(menu)

            elif self._lcd.left_button:
                menu = menu.parent
                self.print_menu(menu)

            elif self._lcd.select_button:
                menu.selected_item.run(self._state, self)
                self.print_menu(menu)

            else:
                time.sleep(0.1)

            if not self._state.is_running:
                break

        self.off()
