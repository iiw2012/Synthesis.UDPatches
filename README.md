# Overview
Synthesis patches related to the Unforgiving Devices (see https://github.com/IHateMyKite/UnforgivingDevices).

## UD_Modifiers.DataStrToArray
A simple one-time-use patch.
Copies values from the deprecated DataStr_*** properties to the new DataStr[] array property in UD_Patcher_ModPreset scripts.

## UD_Modifiers.EspToExcel
It’s not even a patch in the traditional sense. It’s a script that exports modifier settings to an Excel spreadsheet.

The Excel file must include a table header formatted correctly so that the program can fill in all the necessary fields. There is an example in the file /UD_Modifiers.EspToExcel/Data/UD_Empty.xlsx.

The header structure is designed so that any scripts with any set of properties can be used to configure modifiers and their patcher presets. All you need to do (hopefully) is specify the script name and add columns defining their properties.

## UD_Modifiers.ExcelToEsp
A patch to perform the reverse operation - importing modifiers settings from an Excel file into the specified quest.

## Excel Format

### Types
* string - The text in the cell is converted to a string property value
* int, integer - The text in the cell is converted to a integer property value
* float - The text in the cell is converted to a float property value
* form - The text in the cell is converted to a from property value by its EditorId in from loaded modules.
* string list, float list, int list - The text in the cell is split into elements by commas, converted to the appropriate type, and stored in an array property.
* form list - The text in the cell is split into elements by \r\n, converted into links to forms based on the editorID, and stored in an array property.

### Special column names
* Script Name - name of the script. To the right of this column, the script's properties are listed under the "Properties" merged cell.

### Column name for property 
* Simple - The column name is the same as the property name.
* Array elements - The column name contains the element's index in the array (e.g. DataStr[0]).
