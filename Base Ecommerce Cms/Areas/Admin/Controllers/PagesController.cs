using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BEC.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Base_Ecommerce_Cms.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly ILogger<PagesController> _logger;
        private readonly DataContext _context;

        public PagesController(ILogger<PagesController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Page> pages = await _context.Pages.ToListAsync();

            if(pages is null)
                return  NotFound();

            return View(pages);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}