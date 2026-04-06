# Overview
Synthesis patches related to the Unforgiving Devices (see https://github.com/IHateMyKite/UnforgivingDevices).

### UD_Modifiers.DataStrToArray
A simple one-time-use patch.
Copies values from the deprecated DataStr_*** properties to the new DataStr[] array property in UD_Patcher_ModPreset scripts.

### UD_Modifiers.EspToExcel
It’s not even a patch in the traditional sense. It’s a script that exports modifier settings to an Excel spreadsheet.

The Excel file must include a table header formatted correctly so that the program can fill in all the necessary fields. There is an example in the file /UD_Modifiers.EspToExcel/Data/UD_Empty.xlsx.

The header structure is designed so that any scripts with any set of properties can be used to configure modifiers and their patcher presets. All you need to do (hopefully) is specify the script name and add columns defining their properties.

### UD_Modifiers.ExcelToEsp
A patch to perform the reverse operation - importing modifiers settings from an Excel file into the specified quest.
