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
    public class ClothingController : Controller
    {
        private readonly ApplicationDbContext context;

        public ClothingController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Shop(int? id)
        {
            Category category = context.Categories.Include(c=>c.Clothes).ThenInclude(c=>c.Images).FirstOrDefault(c=>c.Id==id);
            if (category.Clothes.Count == 0)
            {
                return NotFound();
            }
            return View(category.Clothes);
        }
    }
}
