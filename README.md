ProgramGODetector
=================

## Overview

A standalone command line app to modify data stored in detector heads for the optometer P-9710 by [Gigahertz-Optik GmbH](https://www.gigahertz-optik.com/) via its RS232 interface.

For use as optometer the P-9710 can be combined with a varity of detector heads intended for photometric and radiometric measurements. Gigahertz-Optik offers a wide range of detector heads for various measuring tasks. The calibration data of the detector heads are saved in the so called calibration data connector.

Technically the calibration data is stored using a 24LC16B (16 Kb I2C compatible serial EEPROM). This IC is located inside the 15 pin connector.

![Image of connector interior](IMG_5412.jpg)


## Usage

The detector to be updated must be connected to a P-9710 optometer and this in turn to a computer via its serial interface. The parameters are supplied as command line options. The original detector setting is displayed and saved in the logfile as a memory dump. After using this procedure the detector has to be unpluged and reconected to the optometer. Alternativly one can switch off the optometer and switch it on again.


### Options

Options for detector parameters

`--serialNumber (-s)` : Detector serial number.

`--calibrationFactor (-f)` : Calibration factor (sensitivity) in A/unit.

`--unit (-u)` : Measurement unit (code).

`--name (-n)` : Detector name (4 characters at most, longer strings will be truncated).

`--customString (-c)` : Custom string (16 characters at most, longer strings will be truncated).

Administrative related options

`--comment` : User supplied string to be included in the log file metadata.

`--port (-p)` : Serial port name.

`--logfile` : Log file name.

## Examples

To show the current setting of the detector without any modification, just do not set any parameter options:
```
ProgramGODetector --port="COM2"
```

The main usage is to update the sensitivity following a new calibration:
```
ProgramGODetector --calibrationFactor=4.7635e-11 --customString="Nr. 2001/0023"
```

## Caveats

* The program (or better the library Bev.Instruments.P9710.Detector) makes use of a secret password for writing to the EEPROM. This password is a 4-digit number and can be found easily by brute force guessing. Since it is hard coded one must change the source code for different optometers.

* All strings must be composed of ASCII characters only! No umlauts, smilies and that like!

* Numerical values (currently the sensitivity only) use the dot as decimal separator.

* Parameters which can not be parsed correctly will not destroy the current values. No user feedback is provided in this case.

* With this version it is only possible to modify broad band calibration data. This means no spectral data can be written to the detector head.


## Dependencies

Bev.Instruments.P9710.Detector: https://github.com/matusm/Bev.Instruments.P9710.Detector

CommandLineParser: https://github.com/commandlineparser/commandline 


