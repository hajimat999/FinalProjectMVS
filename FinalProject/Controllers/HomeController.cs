using FinalProject.DAL;
using FinalProject.HomeVMs;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;

        public HomeController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            HomeVM model = new HomeVM
            {
                Sliders = context.Sliders.ToList(),
                Clothes = context.Clothes.Include(c=>c.Images).ToList(),
                Categories = context.Categories.Include(c=>c.Clothes).ToList()
            };           
            return View(model);
        }
        public IActionResult Shop()
        {
            return View();
        }

    }
}
