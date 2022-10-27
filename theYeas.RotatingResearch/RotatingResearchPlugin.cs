using BepInEx;
using BepInEx.Unity.Mono;
using theYeas.RotatingResearch;
using theYeas.Core.API;
using theYeas.Core.API.Assets.Updates;
using System.Collections.Generic;
using System.Linq;

namespace theYeas.BasePlateUpMod
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInDependency("theYeas.Core", BepInDependency.DependencyFlags.HardDependency)]
    public class RotatingResearchPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            AddAllUpgrades(Tables());
            //AddAllUpgrades(Mixers()); fixed in 1.1.0
            //AddAllUpgrades(Sinks()); fixed in 1.1.0
            AddAllUpgrades(Consumables());
            AddAllUpgrades(Hobs());
            AddAllUpgrades(Counters());
            AddAllUpgrades(Boots());
            AddAllUpgrades(Tools());
            AddAllUpgrades(Mops());
            AddAllUpgrades(Bins());
        }

        private void AddAllUpgrades(List<AssetId> assets)
        {
            foreach(var asset in assets)
            {
                AddUpdate(assets, asset);
            }
        }

        private void AddUpdate(List<AssetId> Upgrades, AssetId Update)
        {
            ModAPI.Assets.PendUpdate(new ApplianceUpdate()
            {
                Id = Update,
                Upgrades = Upgrades.Where(t => t != Update).ToList()
            });
        }

        private List<AssetId> Tables()
        {
            return new()
            {
                AssetId.TableBar,
                AssetId.TableBasicCloth,
                AssetId.TableFancyCloth,
                AssetId.TableCheapMetal,
            };
        }

        private List<AssetId> Mixers()
        {
            return new()
            {
                AssetId.MixerHeated,
                AssetId.MixerPusher,
                AssetId.MixerRapid,
            };
        }

        private List<AssetId> Hobs()
        {
            return new()
            {
                AssetId.HobDanger,
                AssetId.HobSafe,
            };
        }
        private List<AssetId> Bins()
        {
            return new()
            {
                AssetId.BinCompactor,
                AssetId.BinComposter,
                AssetId.BinExpanded,
            };
        }
        private List<AssetId> Counters()
        {
            return new()
            {
                AssetId.Workstation,
                AssetId.Freezer,
            };
        }
        private List<AssetId> Sinks()
        {
            return new()
            {
                AssetId.SinkLarge,
                AssetId.SinkPower,
                AssetId.SinkSoak,
                AssetId.DishWasher
            };
        }
        private List<AssetId> Mops()
        {
            return new()
            {
                AssetId.MopBucketFast,
                AssetId.MopBucketLasting,
            };
        }
        private List<AssetId> Consumables()
        {
            return new()
            {
                AssetId.BreadstickBox,
                AssetId.CandleBox,
                AssetId.NapkinBox,
                AssetId.SharpCutlery,
                AssetId.SpecialsMenuBox,
            };
        }
        private List<AssetId> Boots()
        {
            return new()
            {
                AssetId.ShoeRackTrainers,
                AssetId.ShoeRackWellies,
                AssetId.ShoeRackWorkBoots,
            };
        }
        private List<AssetId> Tools()
        {
            return new()
            {
                AssetId.ScrubbingBrushProvider,
                AssetId.SharpKnifeProvider,
                AssetId.RollingPinProvider
            };
        }

        private void OnGUI()
        {

        }

        private void Update()
        {

        }
    }
}