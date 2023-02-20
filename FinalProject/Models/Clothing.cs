﻿using FinalProject.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Clothing : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int InformationId { get; set; }
        public Information Information { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Image> Images { get; set;}
        public List<ClothingColor> ClothingColors { get; set; }
        public List<ClothingSize> ClothingSizes { get; set; }


    }
}
