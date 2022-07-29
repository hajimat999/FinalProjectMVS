using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Cart()
        {
            return View();
        }

    }
}
