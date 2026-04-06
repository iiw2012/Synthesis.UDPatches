using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UD_Modifiers.ExcelToEsp
{
    internal class ScriptData
    {
        public string ScriptType { get; set; } = "";
        public int ScriptNameColumn { get; set; } = -1;
        public List<ScriptPropData> Properties { get; set; } = new List<ScriptPropData>();

    }

    internal class ScriptPropData
    {
        public string Name { get; set; } = "";
        public string Type { get; set; } = "string";
        public bool IsList => Type.ToLower().Contains("list");
        public int Column { get; set; } = -1;
        public int Index { get; set; } = -1;
    }
}
