using System;

namespace theYeas.Core.API.Game;

public class WorldEndedArgs : EventArgs
{
    public Unity.World World;

    internal WorldEndedArgs(Unity.World world)
    {
        World = world;
    }
}
