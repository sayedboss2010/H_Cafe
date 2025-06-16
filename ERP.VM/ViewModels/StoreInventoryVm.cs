using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.VM.ViewModels
{
    public class StoreInventoryVm
    {
        public int StoreInventoryID { get; set; }

        public int? SparePartID { get; set; }

        public int? StoreID { get; set; }

        public int? Quantity { get; set; }

        public DateTime? EntryDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual SparePartVm SparePart { get; set; }

        public virtual StoreVm Store { get; set; }
    }
}
