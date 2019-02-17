using System.ComponentModel.DataAnnotations;

namespace Upope.ServiceBase.Enums
{
    public enum Status
    {
        [Display(Name = "Pasif")] InActive = 0,
        [Display(Name = "Aktif")] Active = 1,
        [Display(Name = "Silinmiş")] Removed = 2
    }
}
