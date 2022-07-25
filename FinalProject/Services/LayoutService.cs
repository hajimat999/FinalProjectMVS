using FinalProject.DAL;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Services
{
    public class LayoutService
    {
        private readonly ApplicationDbContext context;

        public LayoutService(ApplicationDbContext context)
        {
            this.context = context;
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
    }
}
