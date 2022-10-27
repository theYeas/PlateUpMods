using KitchenData;
using GameData = theYeas.Core.API.Kitchen.GameData;

namespace theYeas.Core.API.Assets.Updates.Finalize;

internal class ApplianceFinalizeUpdate : FinalizeUpdate
{
    public bool ResetUpgrades = false;
    public override void FinalizeUpdates(GameData gd)
    {
        if (ResetUpgrades)
        {
            var appliances = gd.GetAssetsById<Appliance>(AssetMap.Appliance);
            appliances.ForEach(app => app.IsAnUpgrade = false);
            appliances.ForEach(app => app.Upgrades.ForEach(a => a.IsAnUpgrade = true));
        }
    }

    public override bool MergeFinalization(FinalizeUpdate update)
    {
        if (update is ApplianceFinalizeUpdate)
        {
            ResetUpgrades = ResetUpgrades || (update as ApplianceFinalizeUpdate).ResetUpgrades;

            return true;
        }
        return false;
    }
}
