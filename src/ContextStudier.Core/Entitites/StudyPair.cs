namespace ContextStudier.Core.Entitites
{
    public class StudyPair : BaseEntity
    {
        public string NativeText { get; private set; }

        public string StudyText { get; private set; }

        public float StudyIndex { get; private set; }

        public DateTime Date { get; private set; }

        public Folder Folder { get; private set; }
    }
}
