# Veggerby Greenhouse

https://azure.microsoft.com/da-dk/pricing/calculator/

https://pinout.xyz/

https://raspberrypi.stackexchange.com/questions/66395/how-to-use-a-gpio-pin-after-i-put-on-a-hat

https://www.raspberrypi.org/documentation/hardware/raspberrypi/spi/README.md

## Develop VS Code over SSH

https://www.hanselman.com/blog/VisualStudioCodeRemoteDevelopmentOverSSHToARaspberryPiIsButter.aspx

### Connect via SSH

https://unix.stackexchange.com/questions/182483/scp-without-password-prompt-using-different-username

What you want are ssh-key pairs, these create 'trusted networks' that allow for password-less authentication:

On your client (server1):

```
[user@server1]# ssh-keygen -t rsa -b 2048
Generating public/private rsa key pair.
Enter file in which to save the key (/root/.ssh/id_rsa): # Hit Enter
Enter passphrase (empty for no passphrase): # Hit Enter
Enter same passphrase again: # Hit Enter
Your identification has been saved in /root/.ssh/id_rsa.
Your public key has been saved in /root/.ssh/id_rsa.pub.
```

Now copy your public key to your remote server (server2):

```
ssh-copy-id user2@server2
```

[OR]

```
cat ~/.ssh/id_rsa.pub | ssh user2@server2 "mkdir -p ~/.ssh \
    && cat >>  ~/.ssh/authorized_keys"
```

Now when you run the scp (or any other ssh) command you shouldn't be prompted for a password:

```
scp file user2@server2:/drop/location
```

## GPIO Zero

https://gpiozero.readthedocs.io/en/stable/index.html

https://www.raspberrypi.org/documentation/usage/gpio/python/README.md

## LED
https://thepihut.com/blogs/raspberry-pi-tutorials/27968772-turning-on-an-led-with-your-raspberry-pis-gpio-pins

![LED Wiring](./resources/LED_wiring.png)

## SenseHAT

https://pythonhosted.org/sense-hat/

https://pinout.xyz/pinout/sense_hat

## Camera

https://raspberrypi.dk/produkt/zerocam-nightvision/

https://stamm-wilbrandt.de/en/Raspberry_camera.html
https://github.com/Hermann-SW/Raspberry_v1_camera_global_external_shutter
https://github.com/Hermann-SW2/userland/tree/master/host_applications/linux/apps/hello_pi/i420toh264#i420toh264
https://github.com/Hermann-SW/fork-raspiraw

https://www.raspberrypi.org/documentation/usage/camera/raspicam/timelapse.md

https://projects.raspberrypi.org/en/projects/getting-started-with-picamera

`pip install picamera`

https://picamera.readthedocs.org/

## Temperature/humidity sensor

### References

https://let-elektronik.dk/shop/1520-vejr/10167-humidity-and-temperature-sensor---rht03/

https://learn.sparkfun.com/tutorials/rht03-dht22-humidity-and-temperature-sensor-hookup-guide

https://howtomechatronics.com/tutorials/arduino/dht11-dht22-sensors-temperature-and-humidity-tutorial-using-arduino/

https://cdn.sparkfun.com/datasheets/Sensors/Weather/RHT03.pdf

https://pimylifeup.com/raspberry-pi-humidity-sensor-dht22/

![DHT22 Wirking](./resources/DHT22_wiring.webp)

### Pinout

The pins of the RHT03 (DHT22) are labeled in the image below.

![DHT22 Pinout](./resources/DHT22_pinout.jpg)

| Pin | RHT03 (DHT22)| Notes                             |
| --- | ------------ | --------------------------------- |
| 1   | VCC          | Input Voltage between 3.3-6V DC   |
| 2   | DAT          | Data Output                       |
| 3   | N/C          | Not Connected                     |
| 4   | GND          | Ground                            |

### Install Adafruit DHT library:

https://learn.adafruit.com/dht-humidity-sensing-on-raspberry-pi-with-gdocs-logging/python-setup

https://github.com/adafruit/Adafruit_CircuitPython_DHT

`sudo pip3 install adafruit-circuitpython-dht`

(may require `sudo apt-get install libgpiod2` and `pip3 install adafruit-circuitpython-lis3dh`)

Deprecated:

https://github.com/adafruit/Adafruit_Python_DHT

`pip install Adafruit_DHT`

## Raspberry PI Zero

https://www.raspberrypi.org/documentation/configuration/wireless/wireless-cli.md

## PIJuice Zero

https://uk.pi-supply.com/products/pijuice-zero

https://learn.pi-supply.com/make/pijuice-zero-quick-start-guide/

https://github.com/PiSupply/PiJuice/tree/master/Software

https://github.com/PiSupply/PiJuice/tree/master/Software#i2c-command-api

https://github.com/PiSupply/PiJuice/blob/master/Hardware/README.md#pinout

### Install CLI

`sudo apt-get install pijuice-base`

### Run CLI

`pijuice_cli`

## Python3/pip

Install python 3.7 + pip

`sudo apt-get install python3.7 python3-pip`

https://learn.sparkfun.com/tutorials/python-programming-tutorial-getting-started-with-the-raspberry-pi/configure-your-pi

https://realpython.com/python-logging/

### Upgrade Pip
`pip install --upgrade pip`

## Azure EventHub

https://pypi.org/project/azure-eventhub/

https://docs.microsoft.com/en-us/azure/event-hubs/event-hubs-python-get-started-send

### Install Python library

`sudo pip3 install azure-eventhub`

## Azure Functions

https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection

https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-class-library#environment-variables

https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=linux%2Ccsharp%2Cbash

Add the IP to the database access

### Azure Functions Tools

`brew link --overwrite azure-functions-core-tools@3`

Can also be installed via npm

## Azure Blob

https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-python

## EF Core

### Update tools

`dotnet tool update --global dotnet-ef`

### Add initial migration

`dotnet ef migrations add InitialCreate --startup-project ../Veggerby.Greenhouse.Migrations/`

## Crontab

https://crontab.guru/every-10-minutes

## Sparkfun Soil Moisture sensor

[product](https://let-elektronik.dk/shop/1500-biometri--gas/13637--soil-moisture-sensor-with-screw-terminals/)

[sparkfun](https://www.sparkfun.com/products/13637)

[hookup](https://learn.sparkfun.com/tutorials/soil-moisture-sensor-hookup-guide/all)

[sample code](https://github.com/jerbly/tutorials/blob/master/moisture/moist_final.py)

https://computers.tutsplus.com/tutorials/build-a-raspberry-pi-moisture-sensor-to-monitor-your-plants--mac-52875

https://thepihut.com/blogs/raspberry-pi-tutorials/raspberry-pi-plant-pot-moisture-sensor-with-email-notification-tutorial


https://www.instructables.com/id/Soil-Moisture-Sensor-Raspberry-Pi/

## Adafruit RGB Negative 16x2 LCD

[product](https://www.adafruit.com/product/1110)

[guide](https://learn.adafruit.com/adafruit-16x2-character-lcd-plus-keypad-for-raspberry-pi) ([assembly](https://learn.adafruit.com/adafruit-16x2-character-lcd-plus-keypad-for-raspberry-pi/assembly), [solder](https://www.makerspaces.com/how-to-solder/))

[library](https://github.com/adafruit/Adafruit_CircuitPython_CharLCD)

[docs](https://circuitpython.readthedocs.io/projects/charlcd/en/latest/) ([all projects](https://circuitpython.readthedocs.io/projects/bundle/en/latest/drivers.html))

## MCP3008

Deprecated

https://learn.adafruit.com/raspberry-pi-analog-to-digital-converters/mcp3008

Future

[SPI Devices](https://learn.adafruit.com/circuitpython-basics-i2c-and-spi/spi-devices)

[Adafruit MCP3xxxx library](https://github.com/adafruit/Adafruit_CircuitPython_MCP3xxx/) ([sample](https://github.com/adafruit/Adafruit_Python_MCP3008/blob/master/examples/simpletest.py))

```import time
import busio
import digitalio
import board
import adafruit_mcp3xxx.mcp3008 as MCP
from adafruit_mcp3xxx.analog_in import AnalogIn

# create the spi bus
spi = busio.SPI(clock=board.SCK, MISO=board.MISO, MOSI=board.MOSI)

# create the cs (chip select)
cs = digitalio.DigitalInOut(board.D5)

# create the mcp object
mcp = MCP.MCP3008(spi, cs)

print('Reading MCP3008 values, press Ctrl-C to quit...')
# Print nice channel column headers.
print('| {0:>4} | {1:>4} | {2:>4} | {3:>4} | {4:>4} | {5:>4} | {6:>4} | {7:>4} |'.format(*range(8)))
print('-' * 57)

while True:
    # Read all the ADC channel values in a list.
    values = [0]*8
    for i in range(8):
        # The read_adc function will get the value of the specified channel (0-7).
        values[i] = AnalogIn(mcp, i).value
    # Print the ADC values.
    print('| {0:>4} | {1:>4} | {2:>4} | {3:>4} | {4:>4} | {5:>4} | {6:>4} | {7:>4} |'.format(*values))
    # Pause for half a second.
    time.sleep(0.5)

# create an analog input channel on pin 0
#chan = AnalogIn(mcp, MCP.P7)

#print('Raw ADC Value: ', chan.value)
#print('ADC Voltage: ' + str(chan.voltage) + 'V')
```

## Web

[ASP.NET MVC Core + React](https://docs.microsoft.com/en-us/aspnet/core/client-side/spa/react?view=aspnetcore-3.1&tabs=netcore-cli)

[react-bootstrap](https://react-bootstrap.github.io/)

https://reactjs.org/docs/hooks-effect.html

https://overreacted.io/a-complete-guide-to-useeffect/

https://www.robinwieruch.de/react-hooks-fetch-data

https://docs.microsoft.com/en-us/azure/app-service/configure-common

> Note
> In a default Linux container or a custom Linux container, any nested JSON key
> structure in the app setting name like ApplicationInsights:InstrumentationKey
> needs to be configured in App Service as ApplicationInsights__InstrumentationKey
> for the key name. In other words, any : should be replaced by __ (double underscore).

### Application Insights

https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core

https://www.npmjs.com/package/@microsoft/applicationinsights-react-js [demo](https://github.com/Azure-Samples/application-insights-react-demo)

## Auth0

https://auth0.com/blog/how-to-build-and-secure-web-apis-with-aspnet-core-3/#Securing-the-API-with-Auth0

How to implement Client Credentials

https://auth0.com/docs/api-auth/tutorials/client-credentials

### Postman

https://auth0.com/blog/manage-a-collection-of-secure-api-endpoints-with-postman/#Authorization-in-Postman

| Param                 | Value                                                           |
| --------------------- | --------------------------------------------------------------- |
| Token Name            | veggerby.greenhouse                                             |
| Grant Type            | Authorization Code                                              |
| Callback URL          | https://www.getpostman.com/oauth2/callback                      |
| Auth URL              | https://{{auth0_domain}}/authorize?audience={{auth0_audience}}  |
| Access Token URL      | https://{{auth0_domain}}/oauth/token                            |
| Client ID             | {{auth0_client_id}}                                             |
| Client Secret         | {{auth0_client_secret}}                                         |
| Scope                 | openid profile email                                            |
| State                 | random                                                          |
| Client Authentication | Send client credentials in body                                 |

https://jwt.io/

### React

[Integrating Auth0 in a React App with an ASP.NET Core API backend](https://medium.com/datadigest/integrating-auth0-in-a-react-app-with-an-asp-net-core-api-backend-20a64c0e1f9f) ([code](https://github.com/DataDIGEST/Auth0Sample))

[Auth0 Tutorial](https://auth0.com/docs/quickstart/spa/react/01-login)

### NPM

https://www.carlrippon.com/upgrading-npm-dependencies/

#### Update all dependencies

Run `npx npm-check-updates -u` followed by `npm install`

## Other stuff

[RPi - IoT Weather Station](https://www.instructables.com/id/RPi-IoT-Weather-Station/)
