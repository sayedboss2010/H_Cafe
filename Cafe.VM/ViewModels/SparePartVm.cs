using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.VM.ViewModels
{
    public class SparePartVm
    {
        public int SparePartID { get; set; }

        public int? EquipmentID { get; set; }

        public int? SparePartTypeID { get; set; }

        public string SparePartName { get; set; }

        public int? Quantity { get; set; }

        public string SerialNumber { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual ICollection<CheckListMasterDetailVm> CheckListMasterDetails { get; set; } = new List<CheckListMasterDetailVm>();

        public virtual EquipmentVm Equipment { get; set; }

        public virtual SparePartTypeVm SparePartType { get; set; }

        public virtual ICollection<StoreInventoryVm> StoreInventories { get; set; } = new List<StoreInventoryVm>();

        public virtual ICollection<WorkOrderVm> WorkOrders { get; set; } = new List<WorkOrderVm>();
    }
}
