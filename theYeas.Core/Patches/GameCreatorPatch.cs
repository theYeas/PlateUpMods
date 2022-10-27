using HarmonyLib;
using Kitchen;
using theYeas.Core.API;

namespace theYeas.Core.Patches;

[HarmonyPatch(typeof(GameCreator))]
internal static class GameCreatorPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(GameCreator), "Start")]
    public static void Start_PostFix(GameCreator __instance)
    {
        ModAPI.Game.RaiseGameStarted(__instance);
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(GameCreator), "CreateGameWorld")]
    public static void CreateGameWorld_PostFix(GameCreator __instance)
    {
        ModAPI.World.RaiseWorldCreated(__instance);
    }
}
