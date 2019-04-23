using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Entities
{
	public class User : IdentityUser
	{
		public const string AdminUserName = "Admin";
		public const string DefaultAdminPassword = "Admin";

		public const string RoleAdmin = "Administrator";
		public const string RoleUser = "User";
	}
}
