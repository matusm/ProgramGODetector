﻿using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Bev.Instruments.P9710.Detector;


namespace ProgramGODetector
{
    class MainClass
    {
        readonly static string fatSeparator = new string('=', 80);
        readonly static string thinSeparator = new string('-', 80);

        public static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            DateTime timeStamp = DateTime.UtcNow;
            string appName = Assembly.GetExecutingAssembly().GetName().Name;
            var appVersion = Assembly.GetExecutingAssembly().GetName().Version;
            string appVersionString = $"{appVersion.Major}.{appVersion.Minor}";

            var options = new Options();
            if (!CommandLine.Parser.Default.ParseArgumentsStrict(args, options))
                Console.WriteLine("*** ParseArgumentsStrict returned false");

            var streamWriter = new StreamWriter(options.LogFileName, true);
            var device = new P9710Detector(options.Port);
            var newSetting = new DetectorSetting
            {
                CalibrationFactor = options.CalibrationFactor,
                SerialNumber = options.SerialNumber,
                PhotometricUnit = options.UnitCode,
                DetectorName = options.DetectorName,
                CustomString = options.CustomString
            };

            DisplayOnly("");
            LogOnly(fatSeparator);
            DisplayOnly($"Application:  {appName} {appVersionString}");
            LogOnly($"Application:  {appName} {appVersion}");
            LogAndDisplay($"StartTimeUTC: {timeStamp:dd-MM-yyyy HH:mm}");
            LogAndDisplay($"Comment:      {options.UserComment}");
            LogOnly(fatSeparator);
            DisplayOnly("");
            LogAndDisplay("Current detector status (memory dump):");
            string memoryDump = device.RamToString();
            LogAndDisplay(memoryDump);
            LogOnly(thinSeparator);

            LogAndDisplay("Requested modification:");
            if (newSetting.CalibrationFactor is double cf)
                LogAndDisplay($"Calibration factor: {cf}");
            if (newSetting.PhotometricUnit is int unit)
                LogAndDisplay($"Photometric unit: {unit} -> {newSetting.PhotometricUnitSymbol}");
            if (newSetting.SerialNumber is int sn)
                LogAndDisplay($"Serial number: {sn}");
            if (newSetting.DetectorName is string dn)
                LogAndDisplay($"Detector name: {dn}");
            if (newSetting.CustomString is string cs)
                LogAndDisplay($"Custom string: {cs}");


            DisplayOnly("press 'r' to reprogram detector - any other key to quit");

            if (Console.ReadKey(true).Key == ConsoleKey.R)
            {
                device.WriteDetectorStatusToRam(newSetting);
                device.SaveRamToEeprom();
                LogAndDisplay("Reprogramming of detector successful");
            }
            timeStamp = DateTime.UtcNow;
            DisplayOnly("bye.");
            LogOnly("");
            LogOnly(fatSeparator);
                LogOnly($"StopTimeUTC: {timeStamp:dd-MM-yyyy HH:mm}");
            LogOnly(fatSeparator);
            LogOnly("");



            streamWriter.Close();

            /***************************************************/
            void LogAndDisplay(string line)
            {
                DisplayOnly(line);
                LogOnly(line);
            }
            /***************************************************/
            void LogOnly(string line)
            {
                streamWriter.WriteLine(line);
                streamWriter.Flush();
            }
            /***************************************************/
            void DisplayOnly(string line)
            {
                Console.WriteLine(line);
            }
            /***************************************************/
        }
    }
}
