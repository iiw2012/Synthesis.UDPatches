using DocumentFormat.OpenXml.Bibliography;
using Mutagen.Bethesda.WPF.Reflection.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UD_Modifiers.EspToExcel
{
    public class PatchSettings
    {
        [SettingName("Source Quest")]
        public PatchSettingsQuest Source { get; set; } = new();
        [SettingName("Target Excel File")]
        public PatchSettingsExcelFile Target { get; set; } = new();
    }

    public class PatchSettingsExcelFile
    {
        [SettingName("File Name")]
        [Tooltip("Path to the EXISTING Excel file relative to patch data folder. I.e. 'UD_Modifiers.xlsx' will be loaded from '/<This Patch Project>/Data/' folder")]
        public string ExcelFilePath { get; set; } = "UD_Modifiers.xlsx";

        [SettingName("List")]
        public string ExcelList { get; set; } = "Import";

        [SettingName("Header Cell")]
        [Tooltip("Top left cell for the header")]
        public string HeaderCell { get; set; } = "B2";

        [SettingName("Data Cell")]
        [Tooltip("Top left cell for the data")]
        public string DataCell { get; set; } = "B7";
    }
    public class PatchSettingsQuest
    {
        [SettingName("Quest Editor ID")]
        public string? QuestEditorID { get; set; } = "UD_ModifierSlots_Quest";
    }
}
