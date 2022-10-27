using Entities = Unity.Entities;

namespace theYeas.Core.API.Unity
{
    /*
     * Facade for Unity.Entities.World
     */
    public class World
    {
        private Entities.World _World;

        internal World(Entities.World world)
        {
            _World = world;
        }

        public EntityManager EntityManager
        {
            get { return new EntityManager(_World.EntityManager); }
        }
    }
}
