Thank you for downloading!
This cs file was created by Ty Morrison aka Endless#1418
special thanks to BowDown097
------------------------------------------------------------------------------------------
Info:
	This cs file needs a directory argument passed to the globalgamemanagers file found
in steam\steamapps\common\BloonsTD6\BloonsTD6_Data\globalgamemanagers which you will need
to put the entire directory!
------------------------------------------------------------------------------------------
Usage:
string locationOfGGM = @"D:\steam\steamapps\common\BloonsTD6\BloonsTD6_Data\globalgamemanagers";
BTD6VersionDetector btd6versiondetector = new BTD6VersionDetector();
string versionDetected = btd6versiondetector.VersionDetector(locationOfGGM);