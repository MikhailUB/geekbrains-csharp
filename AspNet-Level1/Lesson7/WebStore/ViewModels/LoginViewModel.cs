using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
	public class LoginViewModel
	{
		[Display(Name = "Имя пользователя"), MaxLength(100, ErrorMessage = "Максимальная длина 100 символов")]
		public string UserName { get; set; }

		[Display(Name = "Пароль"), Required, DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Запомнить меня")]
		public bool RememberMe { get; set; }

		public string ReturnUrl { get; set; }
	}
}
