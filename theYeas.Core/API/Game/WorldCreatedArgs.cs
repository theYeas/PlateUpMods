using System;

namespace theYeas.Core.API.Game;

public class WorldCreatedArgs : EventArgs
{
    public Unity.World World;

    internal WorldCreatedArgs(Unity.World world)
    {
        World = world;
    }
}
