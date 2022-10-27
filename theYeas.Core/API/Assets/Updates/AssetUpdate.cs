using kd = KitchenData;
using theYeas.Core.API.Kitchen;

namespace theYeas.Core.API.Assets.Updates;

public abstract class AssetUpdate
{
    /// <summary>
    /// Asset being updated
    /// </summary>
    public AssetId Id { get; set; }

    /// <summary>
    /// Called only for Asset matching Id
    /// </summary>
    public abstract Finalize.FinalizeUpdate Process(kd.GameDataObject obj, GameData gd);
}
