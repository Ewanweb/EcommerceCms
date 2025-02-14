using BEC.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Base_Ecommerce_Cms.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> IndexAsync(string? slug)
        {
            var page = string.IsNullOrEmpty(slug)
                ? await _context.Pages.OrderBy(x => x.Order).FirstOrDefaultAsync()
                : await _context.Pages.FirstOrDefaultAsync(x => x.Slug == slug);

            return View(page);
        }
    }
}
