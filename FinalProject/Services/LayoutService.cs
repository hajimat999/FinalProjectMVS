using FinalProject.DAL;
using FinalProject.HomeVMs;
using FinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Services
{
    public class LayoutService
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor http;
        private readonly UserManager<AppUser> userManager;


        public LayoutService(ApplicationDbContext context, IHttpContextAccessor http, UserManager<AppUser> userManager)
        {
            this.context = context;
            this.http = http;
            this.userManager = userManager;

        }
        public List<Setting> GetSettings()
        {
            List<Setting> settings = context.Settings.ToList();
            return settings;
        }
        public List<Category> GetCategories()
        {
            List<Category> categories = context.Categories.ToList();
            return categories;
        }
        public LayoutBasketVM GetBasket()
        {        
            decimal totalPrice = 0;
            
            List<BasketItem> basketItems = context.BasketItems.Include(ba=>ba.User).ToList();
            foreach (BasketItem basketItem in basketItems)
            {
                totalPrice += basketItem.Price * basketItem.Quantity;

            }
            LayoutBasketVM layoutBasketVM = new LayoutBasketVM
            {
                BasketItems = basketItems,
                TotalPrice = totalPrice,

            };
            return layoutBasketVM;
            //string basketStr = http.HttpContext.Request.Cookies["Basket"];
            //if (!string.IsNullOrEmpty(basketStr))
            //{
            //    BasketVM basket = JsonConvert.DeserializeObject<BasketVM>(basketStr);
            //    LayoutBasketVM layoutBasket = new LayoutBasketVM();
            //    layoutBasket.BasketItemVMs = new List<BasketItemVM>();
            //    foreach (BasketCookieItemVM cookie in basket.BasketCookieItemVModels)
            //    {
            //        if (cookie!= null)
            //        {
            //            Clothing existed = context.Clothes.Include(c=>c.Images).FirstOrDefault(c => c.Id == cookie.Id);
            //            if (existed == null)
            //            {
            //                basket.BasketCookieItemVModels.Remove(cookie);
            //                continue;
            //            }
            //            BasketItemVM basketItem = new BasketItemVM
            //            {
            //                Clothing = existed,
            //                Quantity = cookie.Quantity,
            //                ClothingId = existed.Id,

            //            };
            //            layoutBasket.BasketItemVMs.Add(basketItem);
            //        }
            //    }
            //    layoutBasket.TotalPrice = basket.TotalPrice;
            //    return layoutBasket;
            //}
            //return null;
        } 
        
        public LayoutBasketVM2 GetBasketCoookies()
        {
            string basketStr = http.HttpContext.Request.Cookies["Basket"];
            if (!string.IsNullOrEmpty(basketStr))
            {
                BasketVM basket = JsonConvert.DeserializeObject<BasketVM>(basketStr);
                LayoutBasketVM2 layoutBasket = new LayoutBasketVM2();
                layoutBasket.BasketItemVMs = new List<BasketItemVM>();
                foreach (BasketCookieItemVM cookie in basket.BasketCookieItemVModels)
                {
                    if (cookie != null)
                    {
                        Clothing existed = context.Clothes.Include(c => c.Images).FirstOrDefault(c => c.Id == cookie.Id);
                        if (existed == null)
                        {
                            basket.BasketCookieItemVModels.Remove(cookie);
                            continue;
                        }
                        BasketItemVM basketItem = new BasketItemVM
                        {
                            Clothing = existed,
                            Quantity = cookie.Quantity,
                            ClothingId = existed.Id,

                        };
                        layoutBasket.BasketItemVMs.Add(basketItem);
                    }
                }
                layoutBasket.TotalPrice = basket.TotalPrice;
                return layoutBasket;
            }
            return null;

        }

    }
}
