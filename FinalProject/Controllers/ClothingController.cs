using FinalProject.DAL;
using FinalProject.HomeVMs;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
            Category category = context.Categories.Include(c => c.Clothes).ThenInclude(c => c.Images).FirstOrDefault(c => c.Id == id);
            if (category.Clothes.Count == 0)
            {
                return NotFound();
            }
            return View(category.Clothes);
        }
        public IActionResult Detail(int? id)
        {
            if (id == 0 || id is null) return NotFound();
            Clothing clothing = context.Clothes.Include(c => c.Images).Include(c => c.Information).Include(c => c.Category).FirstOrDefault(c => c.Id == id);
            if (clothing == null) return NotFound();

          
            ClothesCategory clothesCategory = new ClothesCategory
            {
                Cloth = clothing,
                Clothes = context.Clothes.Include(x=>x.Images).Include(x=>x.Information).Include(x=>x.Category).Where(x=>x.CategoryId==clothing.CategoryId).ToList()

            };
            return View(clothesCategory);

        }
         
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null || id == 0) return NotFound();
            Clothing clothing =await context.Clothes.FirstOrDefaultAsync(c => c.Id == id);
            if (clothing == null)
            {
                return NotFound();
            }
            string basketStr = HttpContext.Request.Cookies["Basket"];
            BasketVM basket;
            if (string.IsNullOrEmpty(basketStr))
            {
                basket = new BasketVM();
                BasketCookieItemVM cookieItemVM = new BasketCookieItemVM
                {
                    Id = clothing.Id,
                    Quantity = 1
                };
                basket.BasketCookieItemVModels = new List<BasketCookieItemVM>();
                basket.BasketCookieItemVModels.Add(cookieItemVM);
                basket.TotalPrice = clothing.Price;

            }
            else
            {
                basket = JsonConvert.DeserializeObject<BasketVM>(basketStr);
                BasketCookieItemVM existed = basket.BasketCookieItemVModels.FirstOrDefault(b => b.Id == id);
                if (existed == null)
                {
                    BasketCookieItemVM cookieItemVM = new BasketCookieItemVM
                    {
                        Id = clothing.Id,
                        Quantity = 1
                    };
                 
                    basket.BasketCookieItemVModels.Add(cookieItemVM);
                    basket.TotalPrice += clothing.Price;
                }
                else
                {
                    basket.TotalPrice += clothing.Price;
                    existed.Quantity++;
                }

            }
            basketStr = JsonConvert.SerializeObject(basket);
            HttpContext.Response.Cookies.Append("Basket", basketStr);
            
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Plyus(int? id)
        {
            if (id == null || id == 0) return NotFound();
            string basketStr = HttpContext.Request.Cookies["Basket"];
            Clothing clothing = context.Clothes.FirstOrDefault(c => c.Id == id);
     
            BasketVM basket;
            if (basketStr == null)
            {
                basket = new BasketVM();
                BasketCookieItemVM cookieItemVM = new BasketCookieItemVM
                {
                    Id = clothing.Id,
                    Quantity = 1
                };
                basket.BasketCookieItemVModels = new List<BasketCookieItemVM>();
                basket.BasketCookieItemVModels.Add(cookieItemVM);
                basket.TotalPrice = clothing.Price;
            }
            else
            {
                
                basket = JsonConvert.DeserializeObject<BasketVM>(basketStr);
                
                BasketCookieItemVM cookieItemVM = basket.BasketCookieItemVModels.Find(c =>c!=null&& c.Id == id);
                if (cookieItemVM == null)
                {
                    BasketCookieItemVM basketCookieItemVM = new BasketCookieItemVM
                    {
                        Id = clothing.Id,
                        Quantity = 1

                    };
                    basket.BasketCookieItemVModels.Add(basketCookieItemVM);
                    basket.TotalPrice += clothing.Price;
                }
                else
                {
                    basket.TotalPrice += clothing.Price;
                    cookieItemVM.Quantity++;

                }
                
            }
            basketStr = JsonConvert.SerializeObject(basket);
            HttpContext.Response.Cookies.Append("Basket", basketStr);
            
            return Redirect(Request.Headers["Referer"].ToString());

        }
        public IActionResult Minus(int? id)
        {
            if (id is null || id == 0) return NotFound();
            Clothing clothing = context.Clothes.FirstOrDefault(c => c.Id == id);
            string basketStr = HttpContext.Request.Cookies["Basket"];
            BasketVM basket;
            if (basketStr == null)
            {
                basket = new BasketVM();
                BasketCookieItemVM basketCookie = new BasketCookieItemVM
                {
                    Id = clothing.Id,
                    Quantity = 1
                };
                basket.BasketCookieItemVModels = new List<BasketCookieItemVM>();
                basket.BasketCookieItemVModels.Add(basketCookie);
                basket.TotalPrice = clothing.Price;

            }
            else
            {
                basket = JsonConvert.DeserializeObject<BasketVM>(basketStr);
                BasketCookieItemVM basketCookieItem = basket.BasketCookieItemVModels.FirstOrDefault(b => b.Id == id);
                if (basketCookieItem == null)
                {
                    return NotFound();
                }
                else
                {
                    if (basketCookieItem.Quantity == 1)
                    {
                        basket.BasketCookieItemVModels.Remove(basketCookieItem);
                        basket.TotalPrice -= clothing.Price;
                        basketStr = JsonConvert.SerializeObject(basket);
                        HttpContext.Response.Cookies.Append("Basket", basketStr);

                        return Redirect(Request.Headers["Referer"].ToString());
                        
                    }
                    basket.TotalPrice -= clothing.Price;
                    basketCookieItem.Quantity--;

                }
                
            }
            basketStr = JsonConvert.SerializeObject(basket);
            HttpContext.Response.Cookies.Append("Basket", basketStr);
            
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult RemoveProductbyId(int? id)
        {
            if (id is null || id == 0) return NotFound();
            string basketStr = HttpContext.Request.Cookies["Basket"];
            Clothing clothing = context.Clothes.FirstOrDefault(c => c.Id == id);
            BasketVM basket;
            if (!string.IsNullOrEmpty(basketStr))
            {
                basket = JsonConvert.DeserializeObject<BasketVM>(basketStr);
                BasketCookieItemVM basketCookie = basket.BasketCookieItemVModels.FirstOrDefault(b => b.Id == id);
                if (basketCookie == null)
                {
                    return NotFound();
                }
                basket.BasketCookieItemVModels.Remove(basketCookie);
                basket.TotalPrice-=(clothing.Price*basketCookie.Quantity);
                basketStr = JsonConvert.SerializeObject(basket);
                HttpContext.Response.Cookies.Append("Basket", basketStr);
                return Redirect(Request.Headers["Referer"].ToString());

            }
            
            return NotFound();

        }
    }
    
    
}
