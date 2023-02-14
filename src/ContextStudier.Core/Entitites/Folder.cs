namespace ContextStudier.Core.Entitites
{
    public class Folder : BaseEntity
    {
        public string Name { get; private set; }

        public User User { get; private set; }
    }
}
