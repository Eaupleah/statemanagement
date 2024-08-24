using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Session_.Models;

namespace Cache_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private const string CacheCategoryKey = "_Category";
        private readonly IMemoryCache _memoryCache;

        public CategoriesController(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        static List<Category> categories = new()
        {
            new Category { Id = 1, Name = "Beverages", Description = "Soft drinks, coffees, teas, beers, and ales" },
            new Category
            {
                Id = 2, Name = "Condiments", Description = "Sweet and savory sauces, relishes, spreads, and seasonings"
            },
            new Category { Id = 3, Name = "Confections", Description = "Desserts, candies, and sweet breads" },
            new Category { Id = 4, Name = "Dairy Products", Description = "Cheeses" },
            new Category { Id = 5, Name = "Grains/Cereals", Description = "Breads, crackers, pasta, and cereal" },
            new Category { Id = 6, Name = "Meat/Poultry", Description = "Prepared meats" },
            new Category { Id = 7, Name = "Produce", Description = "Dried fruit and bean curd" },
            new Category { Id = 8, Name = "Seafood", Description = "Seaweed and fish" }
        };


        public const string SessionCategoryKey = "_Category";

        [HttpGet]
        public IActionResult Get()
        {
            if (!_memoryCache.TryGetValue(CacheCategoryKey, out List<Category> _categories))
            {
                _categories = categories;
                var cacheOption = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
                };
                _memoryCache.Set(CacheCategoryKey, categories, cacheOption);
            }

            return Ok(categories);
        }

        [HttpPost]
        public IActionResult Post(Category category)
        {
            int cId = categories.Select(c => c.Id).Max();
            category.Id = cId + 1;
            categories.Add(category);
            return Ok(category);
        }
    }
}