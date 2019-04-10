using System.Collections.Generic;
using WebStore.Domain.Entities;

namespace WebStore.Data
{
	public static class TestData
	{
		public static List<Section> Sections { get; } = new List<Section>
		{
			new Section {Id = 1, Name = "Спорт", Order = 0},
			new Section {Id = 2, Name = "Nike", Order = 0, ParentId = 1},
			new Section {Id = 3, Name = "Under Armour", Order = 1, ParentId = 1},
			new Section {Id = 4, Name = "Adidas", Order = 2, ParentId = 1},
			new Section {Id = 5, Name = "Puma", Order = 3, ParentId = 1},
			new Section {Id = 6, Name = "ASICS", Order = 4, ParentId = 1},
			new Section {Id = 7, Name = "Мужские", Order = 1},
			new Section {Id = 8, Name = "Fendi", Order = 0, ParentId = 7},
			new Section {Id = 9, Name = "Guess", Order = 1, ParentId = 7},
			new Section {Id = 10, Name = "Valentino", Order = 2, ParentId = 7},
			new Section {Id = 11, Name = "Dior", Order = 3, ParentId = 7},
			new Section {Id = 12, Name = "Версаче", Order = 4, ParentId = 7},
			new Section {Id = 13, Name = "Армани", Order = 5, ParentId = 7},
			new Section {Id = 14, Name = "Прада", Order = 6, ParentId = 7},
			new Section {Id = 15, Name = "Дольче-Габбана", Order = 7, ParentId = 7},
			new Section {Id = 16, Name = "Шанель", Order = 8, ParentId = 7},
			new Section {Id = 17, Name = "Гуччи", Order = 9, ParentId = 7},
			new Section {Id = 18, Name = "Женские", Order = 2},
			new Section {Id = 19, Name = "Фенди", Order = 0, ParentId = 18},
			new Section {Id = 20, Name = "Guess", Order = 1, ParentId = 18},
			new Section {Id = 21, Name = "Валентино", Order = 2, ParentId = 18},
			new Section {Id = 22, Name = "Диор", Order = 3, ParentId = 18},
			new Section {Id = 23, Name = "Versace", Order = 4, ParentId = 18},
			new Section {Id = 24, Name = "Детские", Order = 3},
			new Section {Id = 25, Name = "Мода", Order = 4},
			new Section {Id = 26, Name = "Для дома", Order = 5},
			new Section {Id = 27, Name = "Интерьер", Order = 6},
			new Section {Id = 28, Name = "Одежда", Order = 7},
			new Section {Id = 29, Name = "Сумки", Order = 8},
			new Section {Id = 30, Name = "Обувь", Order = 9}
		};

		public static List<Brand> Brands { get; } = new List<Brand>
		{
			new Brand {Id = 1, Name = "Acne", Order = 0},
			new Brand {Id = 2, Name = "Grune Erde", Order = 1},
			new Brand {Id = 3, Name = "Albiro", Order = 2},
			new Brand {Id = 4, Name = "Ronhill", Order = 3},
			new Brand {Id = 5, Name = "Oddmolly", Order = 4},
			new Brand {Id = 6, Name = "Boudestijn", Order = 5},
			new Brand {Id = 7, Name = "Rosch creative culture", Order = 6},
		};

		public static List<Product> Products { get; } = new List<Product>
		{
			new Product { Id = 1, Name = "Товар 1", Price = 100, ImageUrl = "product1.jpg", Order = 0, SectionId = 2, BrandId = 1 },
			new Product { Id = 2, Name = "Товар 2", Price = 200, ImageUrl = "product2.jpg", Order = 1, SectionId = 2, BrandId = 1 },
			new Product { Id = 3, Name = "Товар 3", Price = 300, ImageUrl = "product3.jpg", Order = 2, SectionId = 2, BrandId = 1 },
			new Product { Id = 4, Name = "Товар 4", Price = 400, ImageUrl = "product4.jpg", Order = 3, SectionId = 2, BrandId = 1 },
			new Product { Id = 5, Name = "Товар 5", Price = 500, ImageUrl = "product5.jpg", Order = 4, SectionId = 2, BrandId = 2 },
			new Product { Id = 6, Name = "Товар 6", Price = 600, ImageUrl = "product6.jpg", Order = 5, SectionId = 2, BrandId = 1 },
			new Product { Id = 7, Name = "Товар 7", Price = 700, ImageUrl = "product7.jpg", Order = 6, SectionId = 2, BrandId = 1 },
			new Product { Id = 8, Name = "Товар 8", Price = 800, ImageUrl = "product8.jpg", Order = 7, SectionId = 25, BrandId = 1 },
			new Product { Id = 9, Name = "Товар 9", Price = 900, ImageUrl = "product9.jpg", Order = 8, SectionId = 25, BrandId = 1 },
			new Product { Id = 10, Name = "Товар 10", Price = 1000, ImageUrl = "product10.jpg", Order = 9, SectionId = 25, BrandId = 3 },
			new Product { Id = 11, Name = "Товар 11", Price = 1100, ImageUrl = "product11.jpg", Order = 10, SectionId = 25, BrandId = 3 },
			new Product { Id = 12, Name = "Товар 12", Price = 1200, ImageUrl = "product12.jpg", Order = 11, SectionId = 25, BrandId = 3 },
		};
	}
}
