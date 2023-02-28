using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ContextStudier.Presentation.Core.EntitiesModels
{
    public class FolderModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "New Folder";

        public string? Description { get; set; } = string.Empty;

        public int CardsCount { get; private set; }
    }
}
