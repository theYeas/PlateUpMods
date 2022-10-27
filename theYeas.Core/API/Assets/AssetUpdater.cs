using KitchenData;
using System;
using System.Collections.Generic;
using theYeas.Core.API.Assets.Updates;
using theYeas.Core.API.Assets.Updates.Finalize;
using theYeas.Core.API.Game;

namespace theYeas.Core.API.Assets;

public class AssetUpdater
{
    /* 
     * Singleton
     */
    private static readonly Lazy<AssetUpdater> lazy = new(() => new AssetUpdater());
    public static AssetUpdater Instance { get { return lazy.Value; } }
    /* 
     * Private Members
     */
    private readonly List<AssetUpdate> _PendingUpdates = new();
    private AssetUpdater() { }
    /* 
     * Events
     */
    internal void ApplyPendingUpdates(object obj, GameStartedArgs args)
    {
        ApplyPendingUpdates();
    }
    /* 
     * Public Members
     */

    public void PendUpdate(AssetUpdate update)
    {
        _PendingUpdates.Add(update);
    }

    public void ApplyPendingUpdates()
    {
        var gd = ModAPI.Game.CurrentGameData();
        Dictionary<Type, List<FinalizeUpdate>> possibleFinalizers = new();
        foreach (var update in _PendingUpdates)
        {
            var asset = gd.GetAssetById<GameDataObject>(update.Id);
            var finalizer = update.Process(asset, gd);
            if (finalizer != null)
            {
                var type = update.GetType();
                if (possibleFinalizers.ContainsKey(type))
                {
                    bool merged = false;
                    var finalizers = possibleFinalizers[type];
                    for (int i = 0; i < finalizers.Count; i++)
                    {
                        if (finalizers[i].MergeFinalization(finalizer))
                        {
                            merged = true;
                            break;
                        }
                    }
                    if (!merged)
                    {
                        finalizers.Add(finalizer);
                    }
                }
                else
                {
                    possibleFinalizers.Add(type, new List<FinalizeUpdate>() { finalizer });
                }
            }
        }

        foreach (var list in possibleFinalizers.Values)
        {
            foreach (var f in list)
            {
                f.FinalizeUpdates(gd);
            }
        }
    }
}
