using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebStore.DAL.Context;

namespace WebStore.Data
{
	public class WebStoreContextInitializer
	{
		private readonly WebStoreContext _db;

		public WebStoreContextInitializer(WebStoreContext db)
		{
			_db = db;
		}

		public async Task InitializeAsync()
		{
			await _db.Database.MigrateAsync();

			if (await _db.Products.AnyAsync())
				return;

			using (var transaction = _db.Database.BeginTransaction())
			{
				_db.Sections.AddRange(TestData.Sections);

				_db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Sections] ON");
				_db.SaveChanges();
				_db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Sections] OFF");

				transaction.Commit();
			}

			using (var transaction = _db.Database.BeginTransaction())
			{
				_db.Brands.AddRange(TestData.Brands);

				_db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Brands] ON");
				_db.SaveChanges();
				_db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Brands] OFF");

				transaction.Commit();
			}

			using (var transaction = _db.Database.BeginTransaction())
			{
				_db.Products.AddRange(TestData.Products);

				_db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Products] ON");
				_db.SaveChanges();
				_db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Products] OFF");

				transaction.Commit();
			}
		}
	}
}
