using BEC.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Base_Ecommerce_Cms.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string slug = "")
        {
            slug = slug.IsNullOrEmpty() ? "home" : slug;
            Page page = await _context.Pages.OrderBy(x => x.Order).Where(x => x.Slug == slug).FirstOrDefaultAsync();

            if (page == null) 
                return NotFound();

            return View(page);
        }
    }
}
