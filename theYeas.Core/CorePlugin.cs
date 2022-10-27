using BepInEx;
using BepInEx.Unity.Mono;
using HarmonyLib;
using System.Reflection;
using theYeas.Core.API;

namespace theYeas.Core;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class CorePlugin : BaseUnityPlugin
{
    private void Awake()
    {
        ModAPI.Log = Logger;
        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), MyPluginInfo.PLUGIN_GUID);

        //Apply core event handlers
        ModAPI.Game.OnGameStarted += ModAPI.Assets.ApplyPendingUpdates;
    }

    private void OnGUI()
    {

    }

    private void Update()
    {

    }
}