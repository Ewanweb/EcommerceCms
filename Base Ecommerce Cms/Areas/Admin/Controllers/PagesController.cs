using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BEC.Domain.Admin;
using BEC.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        // نمایش لیست صفحات
        public async Task<IActionResult> Index()
        {
            List<Page> pages = await _context.Pages.OrderBy(x => x.Order).ToListAsync();

            if (pages == null || !pages.Any())
                return NotFound();

            return View(pages);
        }

        // نمایش فرم ایجاد صفحه جدید
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // ایجاد صفحه جدید
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page page)
        {
            if (string.IsNullOrWhiteSpace(page.Title))
            {
                ModelState.AddModelError("Title", "عنوان صفحه نمی‌تواند خالی باشد.");
                return View(page);
            }

            // مقداردهی اولیه اسلاگ با جایگزینی فاصله‌ها به '-'
            page.Slug = page.Title.ToLower().Replace(" ", "-");

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "لطفاً اطلاعات را به درستی وارد کنید.";
                return View(page);
            }

            // بررسی اینکه آیا اسلاگ قبلاً وجود دارد
            bool slugExists = await _context.Pages.AnyAsync(x => x.Slug == page.Slug);
            if (slugExists)
            {
                ModelState.AddModelError("", "این صفحه قبلاً ایجاد شده است!");
                return View(page);
            }
            page.Order = 100;

            _context.Add(page);
            await _context.SaveChangesAsync();

            TempData["Success"] = "صفحه با موفقیت اضافه شد!";
            return RedirectToAction("Index");
        }

        // دریافت اطلاعات یک صفحه برای ویرایش
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var page = await _context.Pages.FindAsync(id);
            if (page == null) return NotFound();

            return View(page);
        }

        // ویرایش صفحه
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Page page)
        {
            if (string.IsNullOrWhiteSpace(page.Title))
            {
                ModelState.AddModelError("Title", "عنوان صفحه نمی‌تواند خالی باشد.");
                return View(page);
            }

            // مقداردهی اولیه اسلاگ
            string newSlug = page.Title.ToLower().Replace(" ", "-");

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "لطفاً اطلاعات را به درستی وارد کنید.";
                return View(page);
            }

            // بررسی اینکه آیا اسلاگ جدید با صفحه دیگری تکراری است
            bool slugExists = await _context.Pages.AnyAsync(x => x.Slug == newSlug && x.Id != page.Id);
            if (slugExists)
            {
                ModelState.AddModelError("", "صفحه‌ای با این نام قبلاً وجود دارد!");
                return View(page);
            }

            page.Slug = newSlug;
            _context.Update(page);
            await _context.SaveChangesAsync();

            TempData["Success"] = "صفحه با موفقیت ویرایش شد!";
            return RedirectToAction("Index");
        }

        // حذف یک صفحه
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var page = await _context.Pages.FindAsync(id);

            if (page == null || id == 1) // صفحه اول قابل حذف نیست
            {
                TempData["Error"] = "این صفحه وجود ندارد یا حذف آن مجاز نیست.";
            }
            else
            {
                _context.Pages.Remove(page);
                await _context.SaveChangesAsync();
                TempData["Success"] = "صفحه با موفقیت حذف شد!";
            }

            return RedirectToAction("Index");
        }

        public void RecorderPages(int[] id)
        {
            int count = 1;

            foreach(var pageId in id)
            {
                var page = _context.Pages.Find(pageId);

                page.Order = count;

                _context.SaveChanges();

                count++;
            }
        }

        // صفحه خطا
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
