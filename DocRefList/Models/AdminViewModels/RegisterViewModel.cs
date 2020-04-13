using System.ComponentModel.DataAnnotations;

namespace DocRefList.Models.AdminViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Почтовый адрес не должен быть пустым")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полное имя не должно быть пустым")]
        [Display(Name = "Полное имя")]
        public string FullName { get; set; }
        
        [Required(ErrorMessage = "Пароль не должен быть пустым")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        [Compare("Password", ErrorMessage = "Ошибка при подтверждении пароля")]
        public string ConfirmPassword { get; set; }
    }
}
