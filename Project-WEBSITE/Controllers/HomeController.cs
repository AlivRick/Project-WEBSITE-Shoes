using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using Project_WEBSITE.Models;
using Project_WEBSITE.Repositories;
using SQLitePCL;
using System.Diagnostics;

namespace Project_WEBSITE.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ApplicationDbContext _context;

        public HomeController(IProductRepository productRepository, ICategoryRepository categoryRepository, ApplicationDbContext context)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var topProducts = _context.OrderDetails
             .GroupBy(od => od.ProductId)
             .OrderByDescending(g => g.Sum(od => od.Quantity))
             .Select(g => g.FirstOrDefault().Product) // Chọn sản phẩm đầu tiên trong mỗi nhóm
             .Take(4)
             .ToList();
            return View(topProducts);
        }

    }
}