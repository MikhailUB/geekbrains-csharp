using Microsoft.AspNetCore.Mvc;

namespace WebStore.Components
{
	[ViewComponent(Name = "UserInfo")] // Имя компонента
	public class UserInfoViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			if (User.Identity.IsAuthenticated)
				return View("UserInfoView");

			return View();
		}
	}
}
