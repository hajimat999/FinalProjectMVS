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

            ShopViewModel shopViewModel = new ShopViewModel()
            {
                Clothings = clothes,
                Colors = context.Colors.ToList(),
                Sizes = context.Sizes.ToList()
            };
            
            return View(shopViewModel);
        }
        [HttpPost]
        public IActionResult Shop1(int? colorId, int? sizeId, int? minPrice, int? maxPrice)
        {
            List<Clothing> clothes = new List<Clothing>();
            List<ClothingColor> clothesColor = new List<ClothingColor>();
            List<ClothingSize> clothesSize = new List<ClothingSize>();
            List<Clothing> newClothes = new List<Clothing>();
            if(colorId != 0)
            {
                clothesColor = context.ClothingColors.Include(c=>c.Clothing).Where(c=>c.ColorId == colorId).ToList();
                clothes = context.Clothes.Include(c => c.Information).Include(c => c.Images).Include(c => c.Category).ToList();

                foreach (ClothingColor clothingColor in clothesColor)
                {
                    newClothes.Add(clothes.FirstOrDefault(c => c.Id == clothingColor.ClothingId));
                }
                return PartialView("_ProductPartialView", newClothes);
            }
            else if(sizeId != 0)
            {
                clothesSize = context.ClothingSizes.Include(c => c.Clothing).Where(c => c.SizeId == sizeId).ToList();
                clothes = context.Clothes.Include(c => c.Information).Include(c => c.Images).Include(c => c.Category).ToList();

                foreach (ClothingSize clothingSize in clothesSize)
                {
                    newClothes.Add(clothes.FirstOrDefault(c => c.Id == clothingSize.ClothingId));
                }
                return PartialView("_ProductPartialView", newClothes);
            }
            else if(minPrice != 0 || maxPrice != 0)
            {
                clothes = context.Clothes.Include(c => c.Information).Include(c => c.Images).Include(c => c.Category).Where(c=>c.Price > minPrice && c.Price < maxPrice).ToList();

            }
            return PartialView("_ProductPartialView", clothes);

        }
    }
}
