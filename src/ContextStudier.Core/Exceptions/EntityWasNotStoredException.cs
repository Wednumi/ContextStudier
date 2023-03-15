namespace ContextStudier.Core.Exceptions
{
    public class EntityWasNotStoredException : Exception
    {
        public EntityWasNotStoredException()
            :base("Can not find entity in the database")
        {            
        }
    }
}
