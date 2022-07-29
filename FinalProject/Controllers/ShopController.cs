using FinalProject.DAL;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class ShopController :Controller
    {
        private readonly ApplicationDbContext context;

        public ShopController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Shop()
        {
            List<Clothing> clothes = context.Clothes.Include(c => c.Information).Include(c => c.Images).Include(c => c.Category).ToList();
            return View(clothes);
        }
    }
}
