namespace ContextStudier.Core.Entitites
{
    public class Folder : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        internal User? User { get; private set; }

        public string UserId { get; set; }

        public IList<Card> Cards { get; set; }

        public Folder(string userId)
        {
            UserId = userId;
        }
    }
}
