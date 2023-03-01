namespace ContextStudier.Core.Entitites
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; private set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if(obj is BaseEntity entity)
            {
                return Id == entity.Id && 
                    GetType() == entity.GetType(); 
            }
            return false;
        }
    }
}
