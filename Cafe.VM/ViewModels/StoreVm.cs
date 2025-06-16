using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.VM.ViewModels
{
    public class StoreVm
    {
        public int StoreID { get; set; }

        public string StoreName { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public int? LocationID { get; set; }

        public virtual LocationVm Location { get; set; }

        public virtual List<StoreInventoryVm> StoreInventories { get; set; } = new List<StoreInventoryVm>();
    }
}
