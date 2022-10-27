using System;
using theYeas.Core.API.Kitchen;

namespace theYeas.Core.API.Game
{
    public class GameStartedArgs : EventArgs
    {
        public GameData GameData;

        internal GameStartedArgs(GameData gameData)
        {
            GameData = gameData;
        }
    }
}
