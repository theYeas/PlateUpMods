using theYeas.Core.API.Kitchen;

namespace theYeas.Core.API.Assets.Updates.Finalize;

public abstract class FinalizeUpdate
{
    /*
     * Returns true if update should be discarded.
     */
    public abstract bool MergeFinalization(FinalizeUpdate update);

    public abstract void FinalizeUpdates(GameData gd);

}
