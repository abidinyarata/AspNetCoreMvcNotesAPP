using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMvcNotesAPP.ViewModels.CategoryModels
{
    public abstract class CategoryCreateEditViewModelBase
    {
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(40, ErrorMessage = "{0} en fazla {1} karakter olabilir.")]
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; }
        [StringLength(250)]
        [Display(Name = "Kategori Açıklama")]
        public string Desc { get; set; }
    }
}
