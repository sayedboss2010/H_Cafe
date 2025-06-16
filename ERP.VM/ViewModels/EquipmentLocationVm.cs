using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.VM.ViewModels
{
    public class EquipmentLocationVm
    {
        public int EquipmentLocationID { get; set; }

        public int? LocationID { get; set; }

        public int? EquipmentID { get; set; }

        public string SerialNumber { get; set; }

        public string EquipmentName { get; set; }

        public DateTime? EntryDate { get; set; }

        public int? ResponsiblePersonID { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual List<CheckListMasterVm> CheckListMasters { get; set; } = new List<CheckListMasterVm>();

        public virtual EquipmentVm Equipment { get; set; }

        public virtual List<EquipmentLocationPlanVm> EquipmentLocationPlans { get; set; } = new List<EquipmentLocationPlanVm>();

        public virtual List<EquipmentTransferVm> EquipmentTransfers { get; set; } = new List<EquipmentTransferVm>();

        public virtual LocationVm Location { get; set; }

        public virtual EmployeeVm ResponsiblePerson { get; set; }
    }
}
