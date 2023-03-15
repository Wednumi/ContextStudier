using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ContextStudier.Presentation.Core.EntitiesModels
{
    public class FolderInfo
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "New Folder";

        public string? Description { get; set; } = string.Empty;

        public override bool Equals(object? obj)
        {
            if(obj is FolderModel folder)
            {
                return Id == folder.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
