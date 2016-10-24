﻿using System;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;

namespace ConsoleApplication1
{
    /// <summary>
    /// 장치 이름, 페어링 유무, 연결된 장치의 MAC주소, 최근에 봤던, 최근에 쓴 정보들과 장치분류, 각종 이용가능 서비스들
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            BluetoothClient bluetoothClient = new BluetoothClient();            
            var devices = bluetoothClient.DiscoverDevices();
            
            Console.WriteLine("Bluetooth devices");
            
            foreach (var device in devices)
            {
                var blueToothInfo = string.Format("- DeviceName: {0}{1}  Connected: {2}{1}  Address: {3:C}{1}  Last seen: {4}{1}  Last used: {5}{1}",
                          device.DeviceName, Environment.NewLine, device.Connected, device.DeviceAddress, device.LastSeen.ToLocalTime(),
                        device.LastUsed);
                blueToothInfo += string.Format("- Class of device{0}   Device: {1}{0}   Major Device: {2}{0}   Service: {3}",
                       Environment.NewLine, device.ClassOfDevice.Device, device.ClassOfDevice.MajorDevice,
                       device.ClassOfDevice.Service);
                int rssi = device.Rssi;
                Console.WriteLine(blueToothInfo);
                Console.WriteLine("-------------------------");
                Console.WriteLine("RSSI : {0}",rssi);
                Console.WriteLine("-------------------------");
                DisplayBluetoothRadio();
            }
            Console.ReadLine();
        }
        static void DisplayBluetoothRadio()
        {
            BluetoothRadio myRadio = BluetoothRadio.PrimaryRadio;
            if (myRadio == null)
            {
                Console.WriteLine("No radio hardware or unsupported software stack");
                return;
            }
            RadioMode mode = myRadio.Mode;
            // Warning: LocalAddress is null if the radio is powered-off.
            Console.WriteLine("* Radio, address: {0:C}", myRadio.LocalAddress);
            Console.WriteLine("Mode: " + mode.ToString());
            Console.WriteLine("Name: " + myRadio.Name);
            Console.WriteLine("HCI Version: " + myRadio.HciVersion
                + ", Revision: " + myRadio.HciRevision);
            Console.WriteLine("LMP Version: " + myRadio.LmpVersion
                + ", Subversion: " + myRadio.LmpSubversion);
            Console.WriteLine("ClassOfDevice: " + myRadio.ClassOfDevice.ToString()
                + ", device: " + myRadio.ClassOfDevice.Device.ToString()
                + " / service: " + myRadio.ClassOfDevice.Service.ToString());
            //
            //
            // Enable discoverable mode
            Console.WriteLine();
            myRadio.Mode = RadioMode.Discoverable;
            Console.WriteLine("Radio Mode now: " + myRadio.Mode.ToString());
        }
    }
}
