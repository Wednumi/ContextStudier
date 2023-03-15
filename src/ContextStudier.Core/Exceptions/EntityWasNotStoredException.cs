namespace ContextStudier.Core.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
            :base("Can not find entity in the database")
        {            
        }
    }
}
