using Kitchen;
using KD = KitchenData;
using System;
using theYeas.Core.API.Kitchen;
using Kitchen.Layouts;

namespace theYeas.Core.API.Game;

public class GameEvents
{
    /* 
     * Singleton
     */
    private static readonly Lazy<GameEvents> lazy = new(() => new GameEvents());
    public static GameEvents Instance { get { return lazy.Value; } }

    /* 
     * Private Members
     */
    private GameData _GameData = null;
    private Seed? _GeneratingSeed = null;
    private GameEvents() { }
    /* 
     * Events
     */
    public event EventHandler<GameStartedArgs> OnGameStarted;
    public event EventHandler<SeedGeneratedArgs> OnSeedGenerated;

    internal void RaiseGameStarted(GameCreator gameCreator)
    {
        var gd = ModAPI.Reflection.GetInstanceField<KD.GameData>(gameCreator, "GameData");
        _GameData = new GameData(gd);
        OnGameStarted?.Invoke(this, new GameStartedArgs(_GameData));
    }
    internal void SeedGenerationStarted(Seed seed)
    {
        _GeneratingSeed = seed;
    }

    internal void RaiseSeedGenerated(LayoutDecorator decorator)
    {
        if (_GeneratingSeed != null)
        {
            var blueprint = ModAPI.Reflection.GetInstanceField<LayoutBlueprint>(decorator, "Blueprint");
            OnSeedGenerated?.Invoke(this, new SeedGeneratedArgs(blueprint, decorator, (Seed)_GeneratingSeed));
            _GeneratingSeed = null;
        }
    }

    /* 
     * Public Members
     */
    public GameData CurrentGameData()
    {
        if (_GameData == null)
        {
            throw new Exception("Game not yet loaded. Listen to OnGameStarted event or use after MonoBehavior.start");
        }
        return _GameData;
    }
}
