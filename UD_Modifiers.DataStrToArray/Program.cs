using CommandLine;
using DynamicData;
using FluentResults;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;

namespace UD_Modifiers.DataStrToArray
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            return await SynthesisPipeline.Instance
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .SetTypicalOpen(GameRelease.SkyrimSE, "UnforgivingDevices.esp")
                .Run(args);
        }

        public static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            Console.WriteLine($"DefaultSettingsFolder: '{state.DefaultSettingsDataPath}'");
            Console.WriteLine($"ExtraSettingsFolder: '{state.ExtraSettingsDataPath}'");
            string? config_path;
            state.TryRetrieveConfigFile("Docs\\UD_Modifiers.xlsx", out config_path);

            Console.WriteLine($"Loading config file with modifiers '{config_path}'");

            return;
            //var base_quest_getter = state.LoadOrder.FirstOrDefault(_ => _.Key == "UnforgivingDevices.esp")?.Value?.Mod?.Quests.FirstOrDefault(_ => _.EditorID == "UD_ModifierSlots_Quest");

            var top_quest_getter = state.LoadOrder.PriorityOrder.Quest().WinningOverrides().FirstOrDefault(_ => _.EditorID == "UD_ModifierSlots_Quest");

            if (top_quest_getter == null)
            {
                return;
            }

            //var base_quest = state.PatchMod.Quests.GetOrAddAsOverride(base_quest_getter);
            var top_quest = state.PatchMod.Quests.GetOrAddAsOverride(top_quest_getter);

            if (top_quest?.VirtualMachineAdapter == null) return;
            //if (base_quest?.VirtualMachineAdapter == null) return;

            Console.WriteLine($"Processing quest {top_quest.EditorID} from the top entry.");

            foreach (var alias in top_quest.VirtualMachineAdapter.Aliases)
            {
                Console.WriteLine($"\tProcessing alias #{alias.Property.Alias}");
                foreach (var preset_script in alias.Scripts.Where(_ => _.Name.ToLowerInvariant().StartsWith("UD_Patcher_ModPreset".ToLowerInvariant())))
                {
                    Console.WriteLine($"\t\tProcessing script {preset_script.Name}");

                    var ds_types = preset_script.Properties.FirstOrDefault(_ => _.Name == "DataStr_Types")?.Cast<ScriptStringProperty>();
                    var ds_easy = preset_script.Properties.FirstOrDefault(_ => _.Name == "DataStr_Easy")?.Cast<ScriptStringProperty>();
                    var ds_norm = preset_script.Properties.FirstOrDefault(_ => _.Name == "DataStr_Ground")?.Cast<ScriptStringProperty>();
                    var ds_hard = preset_script.Properties.FirstOrDefault(_ => _.Name == "DataStr_Hard")?.Cast<ScriptStringProperty>();

                    if (ds_types == null || ds_easy == null || ds_norm == null || ds_hard == null) 
                    {
                        Console.WriteLine($"\t\t\tNo DataStr_*** found");
                        continue;
                    }
                    try
                    {
                        ScriptStringListProperty ds_array = preset_script.Properties.FirstOrDefault(_ => _.Name.ToLowerInvariant() == "DataStr").Cast<ScriptStringListProperty>();
                        if (ds_array == null)
                        {
                            ds_array = new ScriptStringListProperty() { Name = "DataStr" };
                            preset_script.Properties.Add(ds_array);
                            ds_array.Data.Add(ConvertToDataStrArray(ds_types.Data, ds_easy.Data, ds_norm.Data, ds_hard.Data));
                        }
                        else 
                        {
                            continue;
                        }
                    }
                    catch (Exception ex)
                    { 
                        Console.WriteLine($"{ex.Message}: \r\n\tDataStr_Types: {ds_types?.Data}\r\n\tDataStr_Easy: {ds_easy?.Data}\r\n\tDataStr_Ground: {ds_norm?.Data}\r\n\tDataStr_Hard: {ds_hard?.Data}");
                    }
                }
            }
        }

        protected static IEnumerable<string> ConvertToDataStrArray(string types, string easy, string norm, string hard)
        {
            string[] tt = types.Split(",", StringSplitOptions.TrimEntries);
            string[] ee = easy.Split(",", StringSplitOptions.TrimEntries);
            string[] nn = norm.Split(",", StringSplitOptions.TrimEntries);
            string[] hh = hard.Split(",", StringSplitOptions.TrimEntries);

            for (int i = 0; i < tt.Length; i++)
            {
                string d = "";
                if (tt[i].Length > 0)
                {
                    d += $"{tt[i]}: ";
                    if (ee[i] == nn[i] && nn[i] == hh[i])
                    {
                        d += ee[i];
                    }
                    else
                    {
                        d += $"{ee[i]} > {nn[i]} > {hh[i]}";
                    }
                }
                Console.WriteLine($"\t\t\t[{i}] {d}");
                yield return d;
            }
        }
    }
}
