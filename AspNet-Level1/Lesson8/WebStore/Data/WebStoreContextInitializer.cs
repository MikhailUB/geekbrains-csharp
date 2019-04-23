using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;

namespace WebStore.Data
{
	public class WebStoreContextInitializer
	{
		private readonly WebStoreContext _db;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public WebStoreContextInitializer(WebStoreContext db, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			_db = db;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task InitializeAsync()
		{
			await _db.Database.MigrateAsync();

			await InitializeIdentityAsync();

			if (await _db.Products.AnyAsync())
				return;

			using (var transaction = _db.Database.BeginTransaction())
			{
				await _db.Sections.AddRangeAsync(TestData.Sections);

				await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Sections] ON");
				await _db.SaveChangesAsync();
				await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF");

				transaction.Commit();
			}

			using (var transaction = _db.Database.BeginTransaction())
			{
				await _db.Brands.AddRangeAsync(TestData.Brands);

				await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Brands] ON");
				await _db.SaveChangesAsync();
				await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF");

				transaction.Commit();
			}

			using (var transaction = _db.Database.BeginTransaction())
			{
				await _db.Products.AddRangeAsync(TestData.Products);

				await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Products] ON");
				await _db.SaveChangesAsync();
				await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Products] OFF");

				transaction.Commit();
			}
		}

		private async Task InitializeIdentityAsync()
		{
			if (!await _roleManager.RoleExistsAsync(User.RoleUser))
				await _roleManager.CreateAsync(new IdentityRole(User.RoleUser));

			if (!await _roleManager.RoleExistsAsync(User.RoleAdmin))
				await _roleManager.CreateAsync(new IdentityRole(User.RoleAdmin));

			if (await _userManager.FindByNameAsync(User.AdminUserName) == null)
			{
				var admin = new User
				{
					UserName = User.AdminUserName,
					Email = $"{User.AdminUserName}@server.ru"
				};

				var createResult = await _userManager.CreateAsync(admin, User.DefaultAdminPassword);
				if (createResult.Succeeded)
					await _userManager.AddToRoleAsync(admin, User.RoleAdmin);
			}
		}
	}
}
