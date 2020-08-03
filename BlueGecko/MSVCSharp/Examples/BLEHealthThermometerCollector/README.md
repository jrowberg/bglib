BLEHealthThermometerCollector C# Example
=====

This is an MS Visual C Sharp example of a Bluetooth "Health Thermometer" collector. It will connect to a device advertising the Health Thermometer service, subscribe to indications, and print the temperature readings to the console.

This requires an EFR32 running Network Co-Processor (NCP) firmware that the C# application will connect to serially to send commands over Bluetooth. It also requires a Bluetooth device advertising the Health Thermometer service. This can easily be implemented by flashing a device with the "SOC - Thermometer" example from the Silicon Labs Bluetooth SDK. Refer to section 4.1 in [QSG139: Getting Started with Bluetooth][https://www.silabs.com/documents/public/quick-start-guides/qsg139-getting-started-with-bluetooth.pdf] for details on how to do this.
