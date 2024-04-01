using POS_API.DatabaseLogic;
using POS_API.Models;
using POS_API.POS_Database;
using System.Collections.Generic;
using System.Linq;

namespace POS_API.BusinessLogic
{
    public class CategoriesBusiness
    {
        private readonly CategoriesDAT _categoriesDAT;

        public CategoriesBusiness(POSEntities db)
        {
            _categoriesDAT = new CategoriesDAT(db);
        }

        public List<CategoriesViewModel> GetAllCategories()
        {
            List<Category> categories = _categoriesDAT.GetAllCategories();
            List<CategoriesViewModel> categoriesViewModels = categories.Select(c => new CategoriesViewModel
            {
                CategoryId = c.category_ID,
                ProductName = c.product_name,
                CategoryCode = c.category_code,
                CategoryName = c.category_name
            }).ToList();

            return categoriesViewModels;
        }

        public void AddCategory(CategoriesViewModel categoryViewModel)
        {
            Category category = new Category
            {
                product_name = categoryViewModel.ProductName,
                category_code = categoryViewModel.CategoryCode,
                category_name = categoryViewModel.CategoryName
            };

            _categoriesDAT.AddCategory(category);
        }

        // Add other business logic methods as needed
    }
}
