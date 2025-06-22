using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.VM.ViewModels
{
    public class MenuCafeCategoryVM
    {
       
            public int Id { get; set; }
            public string Name { get; set; }
            public List<Subcategory> Subcategories { get; set; }
     
    }
    public class Subcategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }

        public int CategoryId { get; set; }
        //public Category Category { get; set; }
    }

    public class OrderItemViewModel
    {
        public int SubcategoryId { get; set; }
        public int Quantity { get; set; }
    }
}
