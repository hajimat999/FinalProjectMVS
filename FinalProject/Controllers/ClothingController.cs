using FinalProject.DAL;
using FinalProject.HomeVMs;
using FinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Color = FinalProject.Models.Color;
using Size = FinalProject.Models.Size;

namespace FinalProject.Controllers
{
    public class ClothingController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<AppUser> userManager;

        public ClothingController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
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
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == 0 || id is null) return NotFound();
            Clothing clothing = context.Clothes.Include(c => c.Images).Include(c => c.Information).Include(c => c.Category).Include(c=>c.ClothingSizes).ThenInclude(cs=>cs.Size).Include(c => c.ClothingColors).ThenInclude(cs => cs.Color).FirstOrDefault(c => c.Id == id);
            if (clothing == null) return NotFound();

            List<BasketItem> basketItems = new List<BasketItem>();
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
                basketItems = context.BasketItems.Where(ba => ba.UserId == user.Id && ba.ClothingId == id).ToList();
            }
            ClothesCategory clothesCategory = new ClothesCategory
            {
                Cloth = clothing,
                Clothes = context.Clothes.Include(x=>x.Images).Include(x=>x.Information).Include(x=>x.Category).Where(x=>x.CategoryId==clothing.CategoryId).ToList(),
                BasketItems = basketItems,
                
            };
            return View(clothesCategory);

        }
        [HttpPost]
        public async Task<IActionResult> AddBasket(int? productId, int colorId, int sizeId)
        {

            if (productId is null || productId == 0) return NotFound();
            Clothing clothing = await context.Clothes.FirstOrDefaultAsync(c => c.Id == productId);
            if (clothing == null)
            {
                return NotFound();
            }
            
            if (User.Identity.IsAuthenticated)
            {
                string basketStr = HttpContext.Request.Cookies["Basket"];
               
                AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
                BasketItem newExisted = await context.BasketItems.FirstOrDefaultAsync(b => b.UserId == user.Id && b.ClothingId == productId && b.ColorId == colorId && b.SizeId == sizeId);
                ClothingColor clothingColor = await context.ClothingColors.FirstOrDefaultAsync(cc => cc.ColorId == colorId && cc.ClothingId == productId);
                ClothingSize clothingSize = await context.ClothingSizes.FirstOrDefaultAsync(cc => cc.SizeId == sizeId && cc.ClothingId == productId);
                //if (clothingColor.Count < 1 && clothingSize.Count < 1)
                //{
                //    return NotFound();
                //}
                Color color = await context.Colors.FirstOrDefaultAsync(c => c.Id == colorId);
                Size size = await context.Sizes.FirstOrDefaultAsync(c => c.Id == sizeId);
                BasketVM basket = new BasketVM();
                basket = JsonConvert.DeserializeObject<BasketVM>(basketStr);
                if (basketStr != null && basket.BasketCookieItemVModels != null)
                {                                   
                    List<BasketItem> basketItems = await context.BasketItems.Where(b=>b.UserId == user.Id).ToListAsync();
                    if (basketItems.Count() >= 1)
                    {
                        foreach (BasketCookieItemVM basketCookieItemVM in basket.BasketCookieItemVModels)
                        {
                            foreach (BasketItem basketItem in basketItems)
                            {
                                if (basketItem.ClothingId == basketCookieItemVM.Id && basketItem.ColorId == basketCookieItemVM.ColorId && basketItem.ColorId == basketCookieItemVM.ColorId)
                                {
                                    basketItem.Quantity += basketCookieItemVM.Quantity;
                                    
                                }
                                else
                                {
                                    Clothing cloth = await context.Clothes.FirstOrDefaultAsync(c => c.Id == basketCookieItemVM.Id);
                                    Color color2 = await context.Colors.FirstOrDefaultAsync(c => c.Id == basketCookieItemVM.ColorId);
                                    Size size2 = await context.Sizes.FirstOrDefaultAsync(c => c.Id == basketCookieItemVM.SizeId);
                                    BasketItem newBasketItem = new BasketItem
                                    {
                                        ColorId = basketCookieItemVM.ColorId,
                                        Quantity = basketCookieItemVM.Quantity,
                                        Price = cloth.Price,
                                        UserId = user.Id,
                                        User = user,
                                        Color = color2,
                                        Size = size2,
                                        SizeId = basketCookieItemVM.SizeId,
                                        ClothingId = basketCookieItemVM.Id,
                                        Clothing = cloth
                                    };
                                    await context.BasketItems.AddAsync(newBasketItem);
                                    //await context.SaveChangesAsync();
                                }

                            }
                           
                            //basket.BasketCookieItemVModels.Remove(basketCookieItemVM);

                        }
                        BasketVM basketVM = null;
                        string basketStr2 = "";
                        basketStr2 = JsonConvert.SerializeObject(basketVM);
                        CookieOptions cookieOptions = new CookieOptions();
                        cookieOptions.Expires = DateTime.Now.AddDays(90);
                        HttpContext.Response.Cookies.Append("Basket", basketStr2, cookieOptions);
                        await context.SaveChangesAsync();                      
                    }
                    else
                    {
                        
                        foreach (BasketCookieItemVM item in basket.BasketCookieItemVModels)
                        {
                            Clothing cloth = await context.Clothes.FirstOrDefaultAsync(c => c.Id == item.Id);
                            Color color2 = await context.Colors.FirstOrDefaultAsync(c => c.Id == item.ColorId);
                            Size size2 = await context.Sizes.FirstOrDefaultAsync(c => c.Id == item.SizeId);
                            BasketItem basketItem = new BasketItem
                            {
                                ClothingId = item.Id,
                                Color = color2,
                                Size = size2,
                                SizeId = item.SizeId,
                                ColorId = item.ColorId,
                                Quantity = item.Quantity,
                                User = user,
                                UserId = user.Id,
                                Clothing = cloth,
                                Price = cloth.Price,
                            };
                            await context.BasketItems.AddAsync(basketItem);
                            
                            //basket.BasketCookieItemVModels.Remove(item);
                        }
                        BasketVM basketVM = new BasketVM();
                        string basketStr2 = "";
                        basketStr2 = JsonConvert.SerializeObject(basketVM);
                        CookieOptions cookieOptions = new CookieOptions();
                        cookieOptions.Expires = DateTime.Now.AddDays(90);
                        HttpContext.Response.Cookies.Append("Basket", basketStr2, cookieOptions);
                        await context.SaveChangesAsync();
                        
                    }
                }

                //basket.BasketCookieItemVModels.Clear();

                BasketItem existed = await context.BasketItems.FirstOrDefaultAsync(b => b.UserId == user.Id && b.ClothingId == productId && b.ColorId == colorId && b.SizeId == sizeId);
                if (existed == null)
                {
                    existed = new BasketItem
                    {
                        User = user,
                        UserId = user.Id,
                        Clothing = clothing,
                        ClothingId = clothing.Id,
                        Color = color,
                        ColorId = color.Id,
                        Size = size,
                        SizeId = size.Id,
                        Price = clothing.Price,
                        Quantity = 1,
                    };
                    await context.BasketItems.AddAsync(existed);
                }
                else
                {
                    existed.Quantity++;
                }
                clothingColor.Count--;
                clothingSize.Count--;
                await context.SaveChangesAsync();
            }
            else
            {
                //return NotFound();

                //Important Note
                //CookieOptions cookieOptions = new CookieOptions();
                //cookieOptions.Expires = DateTime.Now.AddDays(9);
                //HttpContext.Response.Cookies.Append("Basket", "Value", cookieOptions);

                string basketStr = HttpContext.Request.Cookies["Basket"];
                ClothingColor clothingColor = await context.ClothingColors.FirstOrDefaultAsync(cc => cc.ColorId == colorId && cc.ClothingId == productId);
                ClothingSize clothingSize = await context.ClothingSizes.FirstOrDefaultAsync(cc => cc.SizeId == sizeId && cc.ClothingId == productId);
                //if (clothingColor.Count < 1 && clothingSize.Count < 1)
                //{
                //    return NotFound();
                //}
                Color color = await context.Colors.FirstOrDefaultAsync(c => c.Id == colorId);
                Size size = await context.Sizes.FirstOrDefaultAsync(c => c.Id == sizeId);
                BasketVM basket;
                if (string.IsNullOrEmpty(basketStr))
                {
                    basket = new BasketVM();
                    BasketCookieItemVM cookieItemVM = new BasketCookieItemVM
                    {
                        Id = clothing.Id,
                        ColorId = color.Id,
                        SizeId = size.Id,
                        Quantity = 1
                    };
                    basket.BasketCookieItemVModels = new List<BasketCookieItemVM>();
                    basket.BasketCookieItemVModels.Add(cookieItemVM);
                    basket.TotalPrice = clothing.Price;

                }
                else
                {
                    basket = JsonConvert.DeserializeObject<BasketVM>(basketStr);
                    BasketCookieItemVM existed = basket.BasketCookieItemVModels.FirstOrDefault(b => b.Id == productId /*&& b.ColorId == colorId && b.SizeId == sizeId*/);
                    if (existed == null)
                    {
                        BasketCookieItemVM cookieItemVM = new BasketCookieItemVM
                        {
                            Id = clothing.Id,
                            ColorId = colorId,
                            SizeId = sizeId,
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
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(90);
                HttpContext.Response.Cookies.Append("Basket", basketStr, cookieOptions);
                clothingColor.Count--;
                clothingSize.Count--;
                await context.SaveChangesAsync();
            }
            ClothesCategory clothesCategory;
            if (User.Identity.IsAuthenticated)
            {
                AppUser user1 = await userManager.FindByNameAsync(User.Identity.Name);
                clothesCategory = new ClothesCategory
                {
                    Cloth = clothing,
                    Clothes = context.Clothes.Include(x => x.Images).Include(x => x.Information).Include(x => x.Category).Where(x => x.CategoryId == clothing.CategoryId).ToList(),
                    BasketItems = context.BasketItems.Where(ba => ba.UserId == user1.Id && ba.ClothingId == productId).ToList(),

                };

            }
            else
            {
                clothesCategory = new ClothesCategory
                {
                    Cloth = clothing,
                    Clothes = context.Clothes.Include(x => x.Images).Include(x => x.Information).Include(x => x.Category).Where(x => x.CategoryId == clothing.CategoryId).ToList(),
                    BasketItems = null
                };

            }
           
            return Json("Ok");

        }

        public IActionResult ShowBasket()
        {
            string basketStr = HttpContext.Request.Cookies["Basket"];
            if(HttpContext.Request.Cookies["Basket"] == null)
            {
                return NotFound();
            }
            return Json(JsonConvert.DeserializeObject<BasketVM>(basketStr));
        }


        [HttpPost]
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
            return Json("Ok");
            //return Redirect(Request.Headers["Referer"].ToString());

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

                        return Json("Ok");
                        
                    }
                    basket.TotalPrice -= clothing.Price;
                    basketCookieItem.Quantity--;

                }
                
            }
            basketStr = JsonConvert.SerializeObject(basket);
            HttpContext.Response.Cookies.Append("Basket", basketStr);
            return Json("Ok");
            //return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult RemoveProductbyId(int? id)
        {
            if (id is null || id == 0) return NotFound();
            



            //string basketStr = HttpContext.Request.Cookies["Basket"];
            //Clothing clothing = context.Clothes.FirstOrDefault(c => c.Id == id);
            //BasketVM basket;
            //if (!string.IsNullOrEmpty(basketStr))
            //{
            //    basket = JsonConvert.DeserializeObject<BasketVM>(basketStr);
            //    BasketCookieItemVM basketCookie = basket.BasketCookieItemVModels.FirstOrDefault(b => b.Id == id);
            //    if (basketCookie == null)
            //    {
            //        return NotFound();
            //    }
            //    basket.BasketCookieItemVModels.Remove(basketCookie);
            //    basket.TotalPrice-=(clothing.Price*basketCookie.Quantity);
            //    basketStr = JsonConvert.SerializeObject(basket);
            //    HttpContext.Response.Cookies.Append("Basket", basketStr);
            //    return Redirect(Request.Headers["Referer"].ToString());

            //}
            
            return NotFound();

        }
    }
    
    
}
