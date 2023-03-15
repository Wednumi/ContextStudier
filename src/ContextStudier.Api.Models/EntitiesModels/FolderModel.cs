using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ContextStudier.Presentation.Core.EntitiesModels
{
    public class FolderModel : FolderInfo
    {
        public int CardsCount { get; private set; }
    }
}
