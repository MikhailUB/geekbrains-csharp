using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Map;
using WebStore.ViewModels;

namespace WebStore.Components
{
	public class SectionsViewComponent : ViewComponent
	{
		private readonly IProductData _productData;

		public SectionsViewComponent(IProductData productData)
		{
			_productData = productData;
		}

		public IViewComponentResult Invoke()
		{
			var section = GetSections();

			return View(section);
		}

		//public async Task<IViewComponentResult> InvokeAsync() { }

		private IEnumerable<SectionViewModel> GetSections()
		{
			var sections = _productData.GetSections();

			var parents = sections
				.Where(s => s.ParentId == null)
				.Select(s => s.CreateViewModel())
				.ToList();

			foreach (var parent in parents)
			{
				var childs = sections
					.Where(s => s.ParentId == parent.Id)
					.Select(s => s.CreateViewModel());
				parent.ChildSections.AddRange(childs);
				parent.ChildSections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
			}
			parents.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));

			return parents;
		}
	}
}
