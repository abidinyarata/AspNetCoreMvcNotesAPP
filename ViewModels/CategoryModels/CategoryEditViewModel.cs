using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMvcNotesAPP.ViewModels.CategoryModels
{
    public class CategoryEditViewModel : CategoryCreateEditViewModelBase
    {
        [Display(Name = "Gizli")]
        public bool IsHidden { get; set; }
        public bool NotFound { get; set; }
    }
}
