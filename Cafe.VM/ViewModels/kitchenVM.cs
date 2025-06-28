using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.VM.ViewModels
{
    public class kitchenVM
    {
        public int OrderID { get; set; }

        public int? OrderTypeID { get; set; }

        public string OrderTypeNameAr { get; set; }

        public string OrderTypeNameEn { get; set; }

        public int? TableID { get; set; }

        public string TableNameAr { get; set; }
        public string TableNameEn { get; set; }

        public int? LocationID { get; set; }

        public string LocationNameAr { get; set; }
        public string LocationNameEn { get; set; }
        public string OrderDate { get; set; }

        public string Notes { get; set; }
        public int? UpdateUser { get; set; }
        public virtual List<kitchenOrderDetailVm> OrderDetails { get; set; } = new List<kitchenOrderDetailVm>();
    }

    public class kitchenOrderDetailVm
    {
        public int OrderDetailID { get; set; }
        public int? ItemID { get; set; }
        public string ItemNameAr { get; set; }
        public string ItemNameEn { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Total { get; set; }

       

    }
}
