using System.ComponentModel.DataAnnotations;

namespace DocRefList.Models.AdminViewModels
{
    public class UpdateViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Почтовый адрес не должен быть пустым")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полное имя не должно быть пустым")]
        [Display(Name = "Полное имя")]
        public string FullName { get; set; }
    }
}
