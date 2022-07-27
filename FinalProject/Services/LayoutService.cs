using FinalProject.DAL;
using FinalProject.HomeVMs;
using FinalProject.Models;
using Microsoft.AspNetCore.Http;
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

        public LayoutService(ApplicationDbContext context, IHttpContextAccessor http)
        {
            this.context = context;
            this.http = http;
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
            string basketStr = http.HttpContext.Request.Cookies["Basket"];
            if (!string.IsNullOrEmpty(basketStr))
            {
                BasketVM basket = JsonConvert.DeserializeObject<BasketVM>(basketStr);
                LayoutBasketVM layoutBasket = new LayoutBasketVM();
                layoutBasket.BasketItemVMs = new List<BasketItemVM>();
                foreach (BasketCookieItemVM cookie in basket.BasketCookieItemVModels)
                {
                    Clothing existed = context.Clothes.FirstOrDefault(c => c.Id == cookie.Id);
                    if (existed == null)
                    {
                        basket.BasketCookieItemVModels.Remove(cookie);
                        continue;
                    }
                    BasketItemVM basketItem = new BasketItemVM
                    {
                        Clothing = existed,
                        Quantity = cookie.Quantity,
                        ClothingId = existed.Id
                        
                    };
                    layoutBasket.BasketItemVMs.Add(basketItem);
                }
                layoutBasket.TotalPrice = basket.TotalPrice;
                return layoutBasket; 
            }
            return null;
        }
       
       
    }
}
