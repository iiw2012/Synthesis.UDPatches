using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutagen.Bethesda.Skyrim
{
    public static class ScriptEntryExtensions
    {
        public static TPropType GetOrCreate<TPropType>(this ScriptEntry script, string name)
            where TPropType : ScriptProperty, new()
        {
            TPropType? prop = script.Properties.OfType<TPropType>().FirstOrDefault(_ => _.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (prop == default)
            {
                prop = new TPropType { Name = name, Flags = ScriptProperty.Flag.Edited };
                script.Properties.Add(prop);
            }
            return prop;
        }
    }
}
