# Overview
Synthesis patches related to the Unforgiving Devices (see https://github.com/IHateMyKite/UnforgivingDevices).

## UD_Common
Shared code library.

## UD_Modifiers.DataStrToArray
A simple one-time-use patch.
Copies values from the soon deprecated DataStr_*** properties to the new DataStr[] array property in UD_Patcher_ModPreset scripts.
It is disabled now.

## UD_Modifiers.EspToExcel
It’s not even a patch in the traditional sense. It’s a script that exports modifier settings to an Excel spreadsheet.

### Input
All ESP files with ModifierStorage Quest in it.

### Output
XLSX file with exported values. The Excel file must include a table header formatted correctly so that the program can fill in all the necessary fields. There is an example in the file /UD_Modifiers.EspToExcel/Data/UD_Empty.xlsx.

The header structure is designed so that any scripts with any set of properties can be used to configure modifiers and their patcher presets. All you need to do (hopefully) is specify the script name and add columns defining their properties.

### Settings
#### Source Quest -\> Quest Editor ID
EditorID of the modifier storage quest somewhere in the load order that need to be exported.

Default value: UD_ModifierSlots_Quest.

#### Target Excel File -\> File Name
Path to the EXISTING Excel file relative to patch data folder. I.e. 'UD_CustomDevices.xlsx' will be loaded from '/\<This Patch Project\>/Data/' folder.

Default value: UD_Modifiers.xlsx.

#### Target Excel File -\> List
Excel file list name.

Default value: Import.

#### Target Excel File -\> Header Cell
Address of the top left cell for the header.

Default value: B2.

#### Target Excel File -\> Data Cell
Address of the top left cell for the data.

Default value: B7.

## UD_Modifiers.ExcelToEsp
A patch to perform the reverse operation - importing modifiers settings from an Excel file into the specified quest.

### Input
XLSX file with settings. The Excel file must include a table header formatted correctly so that the program can read in all the necessary fields. There is an example in the file /UD_Modifiers.EspToExcel/Data/UD_Empty.xlsx.

### Output
Modifier storage quest. It could be an existing quest or a new one created in the patch.

### Settings
#### Target Quest -\> Mod ESP
If the mod's name is not specified, or if it does not exist in the load order, the changes will be saved in the resulting patch. The file path is specified relative to the Skyrim's Data folder.

Default value: UD_Import.esp.

#### Target Quest -\> Quest EditorID
If the quest exists in the load order mods, an override will be created for it.

Default value: UD_ModifierSlots_Quest.

#### Target Quest -\> UD Internal
This indicates that direct links to UD forms can be used when creating a patch. In other words, the quest will be located either in a dependent mod or directly inside UnforgivingDevices.esp.

Default value: True.

#### Target Quest -\> Trim Arrays
If the last elements of the array are empty, they are not written to the form.

Defalt value: True.

#### Source Excel File -\> File Name
Path to excel file relative to patch data folder. I.e. 'UD_Modifiers.xlsx' will be loaded from '/\<This Patch Project\>/Data/' folder.

Default value: UD_Modifiers.xlsx.

#### Source Excel File -\> List
Excel file list name.

Default value: Import.

#### Target Excel File -\> Header Cell
Address of the top left cell for the header.

Default value: B2.

#### Target Excel File -\> Data Cell
Address of the top left cell for the data.

Default value: B7.

## Excel Format
See /UD_Modifiers.EspToExcel/Data/UD_Empty.xlsx for reference.
### Types
* string - The text in the cell is converted to/from a string property value
* int - The text in the cell is converted to/from a integer property value
* float - The text in the cell is converted to/from a float property value
* form - The text in the cell is converted to/from a form property value by its EditorId in loaded modules
* alias - The text in the cell is converted to/from an alias property value by its quest EditorId and name in loaded modules. Alias stored in cell in format like "<quest_editor_id>:<alias_name>"
* string[], float[], int[] - The text in the cell is split into elements by commas, converted to the appropriate type, and stored in an array property
* form[], alias[] - The text in the cell is split into elements by \r\n, converted into links to forms based on the editorID, and stored in an array property

### Special column names
* Script Name - name of the script. To the right of this column, the script's properties are listed under the "Properties" merged cell.

### Column name for property 
* Simple - The column name is the same as the property name.
* Array elements - The column name contains the element's index in the array (e.g. DataStr[0]).
