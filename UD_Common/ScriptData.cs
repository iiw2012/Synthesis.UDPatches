using ClosedXML.Excel;
using CommandLine;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Plugins.Exceptions;
using Mutagen.Bethesda.Skyrim;
using Noggog;

namespace UD_Common
{
    public class ScriptData
    {
        public string ScriptType { get; set; } = "";
        public int ScriptNameColumn { get; set; } = -1;
        public List<ScriptPropData> Properties { get; set; } = new List<ScriptPropData>();

    }

    public class ScriptPropData
    {
        public string Name { get; set; } = "";
        public string Type { get; set; } = "string";
        public bool IsList => Type.Trim().ToLower().EndsWith("[]");
        public int Column { get; set; } = -1;
        public int Index { get; set; } = -1;
    }

    public static class ScriptDataHelper
    {
        public static void PrintScriptProperties(this ScriptData header, IXLWorksheet sheet, int row, IScriptEntryGetter script, ILinkCache cache)
        {
            foreach (var prop in script.Properties)
            {
                Console.WriteLine($"\t\t\tProcessing property '{prop.Name}'");
                try
                {
                    var prop_data = header.Properties.ToLookup(_ => _.Name).FirstOrDefault(_ => _.Key.Equals(prop.Name, StringComparison.OrdinalIgnoreCase));
                    if (prop_data == default)
                    {
                        Console.WriteLine($"\t\t\t\tCan't find given property in excel header.");
                        continue;
                    }
                    if (prop is ScriptBoolProperty)
                    {
                        sheet.Cell(row, prop_data.First().Column).Value = (prop as dynamic).Data.ToString();
                    }
                    else if (prop is ScriptIntProperty || prop is ScriptFloatProperty || prop is ScriptStringProperty)
                    {
                        sheet.Cell(row, prop_data.First().Column).Value = (prop as dynamic).Data;
                    }
                    else if (prop is ScriptObjectProperty)
                    {
                        if (prop.Cast<ScriptObjectProperty>().Object.IsNull)
                        {
                            sheet.Cell(row, prop_data.First().Column).Value = "";
                        }
                        else if (prop.Cast<ScriptObjectProperty>().Alias >= 0)
                        {
                            int alias_index = prop.Cast<ScriptObjectProperty>().Alias;
                            var temp_quest = cache.Resolve(prop.Cast<ScriptObjectProperty>().Object);
                            if (temp_quest is not IQuestGetter)
                            {
                                Console.WriteLine($"\t\t\t\tTrying to resolve alias but form '{temp_quest.EditorID}' is not a quest for some reason.");
                                continue;
                            }
                            sheet.Cell(row, prop_data.First().Column).Value = $"{temp_quest.EditorID}:{temp_quest.Cast<IQuestGetter>().Aliases[alias_index].Name}";
                        }
                        else
                        {
                            sheet.Cell(row, prop_data.First().Column).Value = cache.Resolve(prop.Cast<ScriptObjectProperty>().Object).EditorID;
                        }
                    }
                    else if (prop is ScriptBoolListProperty || prop is ScriptIntListProperty || prop is ScriptFloatListProperty || prop is ScriptStringListProperty)
                    {
                        if (prop_data.First().Index < 0)
                        {
                            // all elements in one cell
                            sheet.Cell(row, prop_data.First().Column).Value = string.Join(", ", (prop as dynamic).Data.ToArray());
                        }
                        else
                        {
                            // each element in a separate cell
                            foreach (var prop_index in prop_data)
                            {
                                if ((prop as dynamic).Data.Count > prop_index.Index)
                                {
                                    sheet.Cell(row, prop_index.Column).Value = (prop as dynamic).Data[prop_index.Index];
                                }
                            }
                        }
                    }
                    else if (prop is ScriptObjectListProperty)
                    {
                        if (prop_data.First().Index < 0)
                        {
                            // all elements in one cell
                            sheet.Cell(row, prop_data.First().Column).Value = string.Join("\r\n", prop.Cast<ScriptObjectListProperty>().Objects.Select(_ => (!_.Object.IsNull) ? cache.Resolve(_.Object).EditorID : ""));
                        }
                        else
                        {
                            var obj_list = prop.Cast<ScriptObjectListProperty>().Objects;
                            // each element in a separate cell
                            foreach (var prop_index in prop_data)
                            {
                                if (obj_list.InRange(prop_index.Index))
                                {
                                    if (obj_list[prop_index.Index].Object.IsNull)
                                    {
                                        sheet.Cell(row, prop_index.Column).Value = "";
                                    }
                                    else if (obj_list[prop_index.Index].Alias >= 0)
                                    {
                                        int alias_index = obj_list[prop_index.Index].Alias;
                                        var temp_quest = cache.Resolve(obj_list[prop_index.Index].Object);
                                        if (temp_quest is not IQuestGetter)
                                        {
                                            Console.WriteLine($"\t\t\t\tTrying to resolve alias but form '{temp_quest.EditorID}' is not a quest for some reason.");
                                            continue;
                                        }
                                        sheet.Cell(row, prop_index.Column).Value = $"{temp_quest.EditorID}:{temp_quest.Cast<IQuestGetter>().Aliases[alias_index].Name}";
                                    }
                                    else
                                    {
                                        sheet.Cell(row, prop_index.Column).Value = cache.Resolve(obj_list[prop_index.Index].Object).EditorID;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (MissingRecordException mre)
                {
                    Console.WriteLine($"\t\t\t\t!MissingRecordException: form key = {mre.FormKey}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\t\t\t\t!Exception: {ex.Message}");
                }
            }
        }
    }
}
