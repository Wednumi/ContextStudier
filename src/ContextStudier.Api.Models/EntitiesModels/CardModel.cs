using System.ComponentModel.DataAnnotations;

namespace ContextStudier.Presentation.Core.EntitiesModels
{
    public class CardModel
    {
        [Required]
        public string NativeText { get; set; }

        [Required]
        public string StudyText { get; set; }

        public float StudyIndex { get; private set; }

        public DateTime Date { get; private set; }

        [Required]
        public int FolderId { get; set; }
    }
}
