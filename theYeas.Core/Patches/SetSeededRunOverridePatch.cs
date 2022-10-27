using HarmonyLib;
using Kitchen;
using theYeas.Core.API;

namespace theYeas.Core.Patches;

[HarmonyPatch(typeof(SetSeededRunOverride))]
public static class SetSeededRunOverridePatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(SetSeededRunOverride), "CreateSeededRun", MethodType.Normal)]
    public static void CreateSeededRun_Prefix(Seed seed)
    {
        ModAPI.Game.SeedGenerationStarted(seed);
    }
}
