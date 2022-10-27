using Kitchen;
using Kitchen.Layouts;
using System;
using Entities = Unity.Entities;

namespace theYeas.Core.API.Game;
public sealed class WorldEvents
{
    /* 
     * Singleton
     */
    private static readonly Lazy<WorldEvents> lazy = new(() => new WorldEvents());
    public static WorldEvents Instance { get { return lazy.Value; } }

    /* 
     * Private Members
     */
    private Unity.World _CurrentWorld = null;
    private WorldEvents() { }

    /* 
     * Events
     */
    public event EventHandler<WorldCreatedArgs> OnWorldCreated;
    public event EventHandler<WorldEndedArgs> OnWorldEnded;

    internal void RaiseWorldCreated(GameCreator creator)
    {
        var newWorld = ModAPI.Reflection.GetInstanceField<Entities.World>(creator, "CurrentWorld");
        if (OnWorldEnded != null && _CurrentWorld != null)
        {
            OnWorldEnded(this, new WorldEndedArgs(_CurrentWorld));
        }
        _CurrentWorld = new Unity.World(newWorld);
        if (OnWorldCreated != null)
        {
            OnWorldCreated(this, new WorldCreatedArgs(_CurrentWorld));
        }
    }

    /* 
     * Public Members
     */
    public Unity.World CurrentWorld()
    {
        if (_CurrentWorld == null)
        {
            throw new Exception("World not yet loaded. Listen to OnWorldCreated event.");
        }
        return _CurrentWorld;
    }

}

