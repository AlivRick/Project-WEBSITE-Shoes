using Microsoft.AspNetCore.Mvc;
using Project_WEBSITE.Models;
using Project_WEBSITE.Repositories;
using System.Threading.Tasks;

namespace Project_WEBSITE.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ShopController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            var categories = await _categoryRepository.GetAllAsync();

            ViewBag.Categories = categories;

            return View(products);
        }

        public async Task<IActionResult> Search(string keyword)
        {
            var products = await _productRepository.SearchAsync(keyword);
            var categories = await _categoryRepository.GetAllAsync();

            ViewBag.Categories = categories;

            return View("Index", products);
        }

        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var productsByCategory = await _productRepository.GetByCategoryIdAsync(categoryId);
            var categories = await _categoryRepository.GetAllAsync();

            ViewBag.Categories = categories;

            return View("Index", productsByCategory);
        }
        private IEnumerable<Product> FilterProductsByPrice(IEnumerable<Product> products, decimal? minPrice, decimal? maxPrice)
        {
            if (minPrice.HasValue && maxPrice.HasValue)
            {
                return products.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
            }
            else if (minPrice.HasValue)
            {
                return products.Where(p => p.Price >= minPrice);
            }
            else if (maxPrice.HasValue)
            {
                return products.Where(p => p.Price <= maxPrice);
            }
            else
            {
                return products;
            }
        }
        public async Task<IActionResult> FilterByPrice(decimal? minPrice, decimal? maxPrice)
        {
            var allProducts = await _productRepository.GetAllAsync();
            var filteredProducts = FilterProductsByPrice(allProducts, minPrice, maxPrice).ToList();

            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = categories;

            return View("Index", filteredProducts);
        }
    }
}
