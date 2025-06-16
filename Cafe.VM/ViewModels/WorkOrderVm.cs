using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.VM.ViewModels
{
    public class WorkOrderVm
    {
        public int WorkOrderID { get; set; }

        public int? CheckListMasterDetailID { get; set; }

        public int? SparePartID { get; set; }

        /// <summary>
        /// رقم امر العمل
        /// </summary>
        public int? InventoryID { get; set; }

        /// <summary>
        /// الكمية المطلوبة
        /// </summary>
        public int? Quantity { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? InstallationStatus { get; set; }

        public virtual CheckListMasterDetailVm CheckListMasterDetail { get; set; }

        public virtual SparePartVm SparePart { get; set; }
    }
}
