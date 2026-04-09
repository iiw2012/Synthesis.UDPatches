using DocumentFormat.OpenXml.Bibliography;
using Mutagen.Bethesda.WPF.Reflection.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UD_CustomDevices.EspToExcel
{
    public class PatchSettings
    {
        [SettingName("Device Filter")]
        public PatchSettingsDeviceFilter DeviceFilter { get; set; } = new();

        [SettingName("Target Excel File")]
        public PatchSettingsExcelFile Target { get; set; } = new();
    }

    public class PatchSettingsExcelFile
    {
        [SettingName("File Name")]
        [Tooltip("Path to the EXISTING Excel file relative to patch data folder. I.e. 'UD_CustomDevices.xlsx' will be loaded from '/<This Patch Project>/Data/' folder")]
        public string ExcelFilePath { get; set; } = "UD_CustomDevices.xlsx";

        [SettingName("List")]
        public string ExcelList { get; set; } = "Import";

        [SettingName("Header Cell")]
        [Tooltip("Top left cell for the header")]
        public string HeaderCell { get; set; } = "B2";

        [SettingName("Data Cell")]
        [Tooltip("Top left cell for the data")]
        public string DataCell { get; set; } = "B7";
    }
    public class PatchSettingsDeviceFilter
    {
        [SettingName("By Keyword")]
        [Tooltip("")]
        public string ByKeyword { get; set; } = "";

        [SettingName("By EditorID")]
        [Tooltip("")]
        public string ByEditorID { get; set; } = "";
    }
}
