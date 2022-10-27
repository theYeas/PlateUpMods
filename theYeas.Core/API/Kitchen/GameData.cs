using KitchenData;
using System.Collections.Generic;
using System.Linq;
using theYeas.Core.API.Assets;
using KD = KitchenData;

namespace theYeas.Core.API.Kitchen;

/*
 * Facade for Kitchen.GameData
 */
public class GameData
{
    private KD.GameData _GameData;

    internal GameData(KD.GameData GameData)
    {
        _GameData = GameData;
    }

    public T GetAssetById<T>(AssetId assetId) where T : GameDataObject
    {
        return _GameData.Get((int)assetId) as T;
    }

    public List<T> GetAssetsById<T>(List<AssetId> assetIds) where T : GameDataObject
    {
        return assetIds.Select(id => GetAssetById<T>(id)).ToList();
    }
}
