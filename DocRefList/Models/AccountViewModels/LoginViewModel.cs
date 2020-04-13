using System.ComponentModel.DataAnnotations;

namespace DocRefList.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Почтовый адрес не должен быть пустым")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль не должен быть пустым")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }
}
