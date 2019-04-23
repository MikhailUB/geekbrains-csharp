using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
	public class RegistrerUserViewModel
	{
		[Display(Name = "Имя пользователя")]
		[MaxLength(100, ErrorMessage = "Максимальная длина 100 символов")]
		public string UserName { get; set; }

		[Display(Name = "Пароль")]
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Подтверждение пароля")]
		[Required]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
		public string ConfirmPassword { get; set; }
	}
}
