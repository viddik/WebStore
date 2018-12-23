using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.ViewModel.Account
{
    public class RegisterUserViewModel
    {
        [Required, MaxLength(256)]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        [Display(Name = "Введите пароль еще раз")]
        public string ConfirmPassword { get; set; }
    }
}
