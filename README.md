ProgramGODetector
=================

## Overview

A standalone command line app to modify data stored in detector heads for the optometer P-9710 by [Gigahertz-Optik GmbH](https://www.gigahertz-optik.com/) via its RS232 interface.

For use as optometer the P-9710 can be combined with a variety of detector heads intended for photometric and radiometric measurements. Gigahertz-Optik offers a wide range of detector heads for various measuring tasks. The calibration data of the detector heads are saved in the so-called calibration data connector.

Technically the calibration data is stored using a 24LC16B (16 Kb I2C compatible serial EEPROM). This IC is located inside the 15-pin connector.

![Image of connector interior](IMG_5412.jpg)


## Usage

The detector to be updated must be connected to a P-9710 optometer and this in turn to a computer via its serial interface. The parameters are supplied as command line options. The original detector setting is displayed and saved in the logfile as a memory dump. After using this procedure, the detector has to be unplugged and reconnected to the optometer. Alternatively, one can switch off the optometer and switch it on again.


### Command Line Options

Generic options:

`--comment`  User supplied string to be included in the log file.

`--port (-p)`  Serial port name where the optometer is connected

`--logfile`  Log file name.

Options for detector parameters:

`--serialNumber (-s)`  Detector serial number.

`--calibrationFactor (-f)`  Calibration factor (sensitivity) in A/unit.

`--name (-n)`  Detector name (4 characters at most, longer strings will be truncated).

`--customString (-c)`  Custom string (16 characters at most, longer strings will be truncated).

`--unit (-u)`  Measurement unit (code).

#### Unit codes
```
 0   W
 1   W/m2
 2   W/sr
 3   W/m2/sr 
 4   lm 
 5   lx 
 6   cd 
 7   cd/m2 
 8   MED/h  
 9   mol/m2/s 
10   A 
11   cdsr 
12   lm/sr 
13   lm/m2 
14   pc 
15   fc 
16   E/m2
17   W/cm2 
18   W/cm2*sr
19   lm/cm2 
20   cdsr/m2 
21   fL 
22   sb 
23   L 
24   nit 
```

## Examples

To show the current setting of the detector without any modification, just do not set any parameter options. You may set other option:
```
ProgramGODetector --port="COM2"
```

The main usage of this program is to update the sensitivity following a new calibration:
```
ProgramGODetector --calibrationFactor=4.7635e-11 --customString="Nr. 2001/0023"
```

## Caveats

* The program (or better the library Bev.Instruments.P9710.Detector) makes use of a secret password for writing to the EEPROM. This password is a 4-digit number and can thus be found easily by brute force guessing. Since it is hard coded one must change the source code for different optometers.

* All strings must be composed of ASCII characters only! No umlauts, smileys and that like!

* Numerical values use the dot as decimal separator.

* Parameters which cannot be parsed correctly will not overwrite the current values. No user feedback is provided in this case!

* With this version it is only possible to modify broad band calibration data. This means no spectral data can be written to the detector head.

## Memory Dump

Before modifing any setting the program dumps the annotated memory to the screen an the logfile. Here is a a typical example:
```
   0 000 ->  80 50 01010000 'P'   Identification string - letter 1
   1 001 ->  84 54 01010100 'T'   Identification string - letter 2
   2 002 ->  57 39 00111001 '9'   Identification string - letter 3
   3 003 ->  54 36 00110110 '6'   Identification string - letter 4
   4 004 ->  49 31 00110001 '1'   Identification string - letter 5
   5 005 ->  48 30 00110000 '0'   Identification string - letter 6
   6 006 -> 141 8D 10001101 ' '   Serial number - LSB
   7 007 -> 204 CC 11001100 ' '   Serial number - MSB
   8 008 ->   0 00 00000000 ' '   < not used >
   9 009 ->   0 00 00000000 ' '   < not used >
  10 00A ->   0 00 00000000 ' '   < not used >
  11 00B -> 255 FF 11111111 ' '   < not used >
  12 00C -> 255 FF 11111111 ' '   < not used >
  13 00D -> 255 FF 11111111 ' '   < not used >
  14 00E ->   0 00 00000000 ' '   < not used >
  15 00F ->   0 00 00000000 ' '   < not used >
  16 010 ->  71 47 01000111 'G'   Custom string - letter 1
  17 011 ->  79 4F 01001111 'O'   Custom string - letter 2
  18 012 ->  50 32 00110010 '2'   Custom string - letter 3
  19 013 ->  48 30 00110000 '0'   Custom string - letter 4
  20 014 ->  48 30 00110000 '0'   Custom string - letter 5
  21 015 ->  48 30 00110000 '0'   Custom string - letter 6
  22 016 ->  32 20 00100000 ' '   Custom string - letter 7
  23 017 ->  32 20 00100000 ' '   Custom string - letter 8
  24 018 ->  32 20 00100000 ' '   Custom string - letter 9
  25 019 ->  32 20 00100000 ' '   Custom string - letter 10
  26 01A ->  32 20 00100000 ' '   Custom string - letter 11
  27 01B ->  32 20 00100000 ' '   Custom string - letter 12
  28 01C ->  32 20 00100000 ' '   Custom string - letter 13
  29 01D ->  32 20 00100000 ' '   Custom string - letter 14
  30 01E ->  32 20 00100000 ' '   Custom string - letter 15
  31 01F ->  32 20 00100000 ' '   Custom string - letter 16
  32 020 ->   0 00 00000000 ' '   < not used >
  33 021 ->   0 00 00000000 ' '   < not used >
  34 022 ->   0 00 00000000 ' '   < not used >
  35 023 ->   0 00 00000000 ' '   < not used >
  36 024 ->   0 00 00000000 ' '   < not used >
  37 025 ->   0 00 00000000 ' '   < not used >
  38 026 ->   0 00 00000000 ' '   < not used >
  39 027 ->   0 00 00000000 ' '   < not used >
  40 028 ->   0 00 00000000 ' '   < not used >
  41 029 ->   0 00 00000000 ' '   < not used >
  42 02A ->   0 00 00000000 ' '   < not used >
  43 02B ->   0 00 00000000 ' '   < not used >
  44 02C ->   0 00 00000000 ' '   < not used >
  45 02D ->   0 00 00000000 ' '   < not used >
  46 02E ->   0 00 00000000 ' '   < not used >
  47 02F ->   0 00 00000000 ' '   < not used >
  48 030 ->  86 56 01010110 'V'   Detector name - letter 1
  49 031 ->  76 4C 01001100 'L'   Detector name - letter 2
  50 032 -> 159 9F 10011111 ' '   Calibration factor - LSB
  51 033 ->  46 2E 00101110 '.'   Calibration factor - MSB
  52 034 ->   7 07 00000111 ' '   Calibration factor - exponent
  53 035 ->  11 0B 00001011 ' '   Bit pattern: flag, unit, sign of calibration factor
  54 036 ->  32 20 00100000 ' '   Detector name - letter 3
  55 037 ->  32 20 00100000 ' '   Detector name - letter 4
  56 038 ->   0 00 00000000 ' '   < not used >
  57 039 ->   0 00 00000000 ' '   < not used >
  58 03A ->   0 00 00000000 ' '   < not used >
  59 03B ->   0 00 00000000 ' '   < not used >
  60 03C ->   0 00 00000000 ' '   < not used >
  61 03D ->   0 00 00000000 ' '   < not used >
  62 03E ->   0 00 00000000 ' '   < not used >
  63 03F ->   0 00 00000000 ' '   < not used >
```
For a detailed decription of the different parameters consult the documentation.

## Dependencies

Bev.Instruments.P9710.Detector: https://github.com/matusm/Bev.Instruments.P9710.Detector

CommandLineParser: https://github.com/commandlineparser/commandline 


