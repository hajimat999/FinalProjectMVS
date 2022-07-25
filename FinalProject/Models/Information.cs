using FinalProject.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Information:BaseEntity
    {
        public string AdditionalInformation { get; set; }
        public string Information1 { get; set; }
        public string Information2 { get; set; }
        public string Information3 { get; set; }
        public string Information4 { get; set; }
        public List<Clothing> Clothes { get; set; }
    }
}
