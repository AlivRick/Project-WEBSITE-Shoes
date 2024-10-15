using Microsoft.AspNetCore.Mvc;
using Project_WEBSITE.Models;
using Project_WEBSITE.Repositories;

namespace Project_WEBSITE.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IProductRepository _productRepository;

        public FeedbackController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> SubmitFeedback(Feedback feedback)
        {
            var product = await _productRepository.GetByIdAsync(feedback.Id);
            if (product == null)
            {
                return NotFound();
            }

            if (product.Feedbacks == null)
            {
                product.Feedbacks = new List<Feedback>();
            }

            product.Feedbacks.Add(feedback);
            await _productRepository.UpdateAsync(product);

            return RedirectToAction("Details", "Product", new { id = product.Id });
        }
        public IActionResult ShowFeedbackForm(int productId)
        {
            // Truyền productId để biết phản hồi thuộc về sản phẩm nào
            ViewBag.ProductId = productId;
            return View();
        }

    }
}
