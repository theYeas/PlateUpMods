using BepInEx.Logging;
using theYeas.Core.API.Assets;
using theYeas.Core.API.Game;
using theYeas.Core.Helpers;

namespace theYeas.Core.API;

public static class ModAPI
{
    public static GameEvents Game = GameEvents.Instance;
    public static WorldEvents World = WorldEvents.Instance;
    public static Reflection Reflection = Reflection.Instance;
    public static AssetUpdater Assets = AssetUpdater.Instance;
    public static ManualLogSource Log;
}
