using kd = KitchenData;
using System.Collections.Generic;
using theYeas.Core.API.Assets.Updates.Finalize;
using theYeas.Core.API.Kitchen;

namespace theYeas.Core.API.Assets.Updates;

public class ApplianceUpdate : AssetUpdate
{
    public List<AssetId> Upgrades = new List<AssetId>();
    public override FinalizeUpdate Process(kd.GameDataObject obj, GameData gd)
    {
        var upgradeTo = gd.GetAssetsById<kd.Appliance>(Upgrades);
        kd.Appliance appliance = obj as kd.Appliance;
        appliance.Upgrades.Clear();
        appliance.Upgrades.AddRange(upgradeTo);

        return new ApplianceFinalizeUpdate()
        {
            ResetUpgrades = true
        };
    }
}
