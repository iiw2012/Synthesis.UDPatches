using DocumentFormat.OpenXml.Bibliography;
using Mutagen.Bethesda.WPF.Reflection.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UD_ModPreset.ExcelToEsp
{
    public class PatchSettings
    {
        [SettingName("Target Quest")]
        public PatchSettingsQuest Target { get; set; } = new();

        [SettingName("Source Excel File")]
        public PatchSettingsExcelFile Source { get; set; } = new();
    }

    public class PatchSettingsExcelFile
    {
        [SettingName("File Name")]
        [Tooltip("Path to excel file relative to patch data folder. I.e. 'UD_Modifiers.xlsx' will be loaded from '/<This Patch Project>/Data/' folder")]
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
        [SettingName("Mod esp")]
        [Tooltip("If the mod's name is not specified, or if it does not exist in the load order, the changes will be saved in the resulting patch. The file path is specified relative to the Skyrim's Data folder")]
        public string ModName { get; set; } = "UD_Import.esp";

        [SettingName("Quest EditorID")]
        public string QuestEditorID { get; set; } = "UD_ModifierImport1_Quest";

        [SettingName("UD Internal")]
        [Tooltip("May contain direct links to UD forms")]
        public bool UDInternal { get; set; } = true;
    }
}
