using Entities = Unity.Entities;

namespace theYeas.Core.API.Unity
{
    /*
     * Facade for Unity.EntityManager
     */
    public class EntityManager
    {
        private Entities.EntityManager _EntityManager;

        internal EntityManager(Entities.EntityManager EntityManager)
        {
            _EntityManager = EntityManager;
        }
    }
}
