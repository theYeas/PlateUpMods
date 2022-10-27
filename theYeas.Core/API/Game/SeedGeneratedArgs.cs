using Kitchen;
using Kitchen.Layouts;
using System;

namespace theYeas.Core.API.Game;

public class SeedGeneratedArgs : EventArgs
{
    public bool ManuallyGenerated = true; // Currently only captures seeds entered manually by players
    public LayoutBlueprint LayoutBlueprint { get; private set; }
    public LayoutDecorator LayoutDecorator { get; private set; }
    public Seed Seed { get; private set; }
    internal SeedGeneratedArgs(LayoutBlueprint blueprint, LayoutDecorator decorator, Seed seed)
    {
        // TODO: Make Facades for these.
        LayoutBlueprint = blueprint;
        LayoutDecorator = decorator;
        Seed = seed;
    }
}
