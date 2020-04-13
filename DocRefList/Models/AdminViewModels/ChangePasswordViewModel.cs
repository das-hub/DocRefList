using System.ComponentModel.DataAnnotations;

namespace DocRefList.Models.AdminViewModels
{
    public class ChangePasswordViewModel
    {
        public string EmployeeId { get; set; }
        public string Token { get; set; }

        [Required(ErrorMessage = "Пароль не должен быть пустым")]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение")]
        [Compare("NewPassword", ErrorMessage = "Ошибка при подтверждении пароля")]
        public string ConfirmPassword { get; set; }
    }
}
