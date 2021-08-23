using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace ProgramGODetector
{
    public class Options
    {
        [Option('p', "port", DefaultValue = "COM1", HelpText = "Serial port name.")]
        public string Port { get; set; }

        [Option('s', "serialNumber", DefaultValue = null, HelpText = "Detector serial number")]
        public int? SerialNumber { get; set; }

        [Option('f', "calibrationFactor", DefaultValue = null, HelpText = "Calibration factor in A/unit")]
        public double? CalibrationFactor { get; set; }

        [Option('u', "unit", DefaultValue = null, HelpText = "Measurement unit (code)")]
        public int? UnitCode { get; set; }

#nullable enable
        [Option('n', "name", DefaultValue = null, HelpText = "Detector name (4 characters)")]
        public string? DetectorName { get; set; }

        [Option('c', "customString", DefaultValue = null, HelpText = "Custom string (16 characters)")]
        public string? CustomString { get; set; }
#nullable disable

        [Option("comment", DefaultValue = "---", HelpText = "User supplied comment string.")]
        public string UserComment { get; set; }

        [Option("logfile", DefaultValue = "ProgramGODetector.log", HelpText = "Log file name.")]
        public string LogFileName { get; set; }

        [ValueList(typeof(List<string>), MaximumElements = 2)]
        public IList<string> ListOfFileNames { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            string AppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            string AppVer = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            HelpText help = new HelpText
            {
                Heading = new HeadingInfo($"{AppName}, version {AppVer}"),
                Copyright = new CopyrightInfo("Michael Matus", 2021),
                AdditionalNewLineAfterOption = false,
                AddDashesToOption = true
            };
            string preamble = "Program to re-program detector heads for the photo-current meter P-9710 (Gigahertz-Optik). Operations are perfomed via the RS232 interface of the optometer. " +
               "All modifications are logged in a file.";
            help.AddPreOptionsLine(preamble);
            help.AddPreOptionsLine("");
            help.AddPreOptionsLine($"Usage: {AppName} [options]");
            help.AddPostOptionsLine("");
            help.AddOptions(this);

            return help;
        }

    }
}

