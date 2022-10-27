using HarmonyLib;
using Kitchen;
using theYeas.Core.API;

namespace theYeas.Core.Patches;

[HarmonyPatch(typeof(LayoutDecorator))]
public static class LayoutDecoratorPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(LayoutDecorator.AttemptDecoration))]
    public static void AttemptDecoration_PostFix(LayoutDecorator __instance)
    {
        ModAPI.Game.RaiseSeedGenerated(__instance);
    }
}
