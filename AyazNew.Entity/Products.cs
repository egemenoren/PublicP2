using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyazNew.Entity
{
    public class Products : BaseEntity
    {
        public string ProductName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public double Price { get; set; }
        public double OffPrice { get; set; }
        public string Image { get; set; }
        public int CategoryID { get; set; }
        public bool isFeatured { get; set; } = false;
    }
}
