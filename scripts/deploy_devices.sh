#!/bin/bash

rsync -av -e ssh ../src/Veggerby.Greenhouse.Devices/controller/* pi@pi-zero-lcd:/home/pi/src
rsync -av -e ssh ../src/Veggerby.Greenhouse.Devices/primary/* pi@pi-zero:/home/pi/src
rsync -av -e ssh ../src/Veggerby.Greenhouse.Devices/sensehat/* pi@pi-sensehat:/home/pi/Documents/src