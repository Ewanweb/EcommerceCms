using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BEC.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Base_Ecommerce_Cms.Components
{
    public class MenuViewComponent(DataContext context) : ViewComponent
    {
        private readonly DataContext _context  = context;
        public async Task<IViewComponentResult> InvokeAsync() => View(await _context.Pages.OrderBy(x => x.Order).Where(x => x.Slug != "home").ToListAsync());
    }
}