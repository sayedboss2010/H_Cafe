using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.VM.ViewModels
{
    public class EquipmentTransferVm
    {
        public int TransferID { get; set; }

        public int? EquipmentLocationID { get; set; }

        public string FromLocation { get; set; }

        public string ToLocation { get; set; }

        public DateTime? TransferDate { get; set; }

        public int? ResponsiblePersonID { get; set; }

        public string TransferReason { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual EquipmentLocationVm EquipmentLocation { get; set; }

        public virtual EmployeeVm ResponsiblePerson { get; set; }
    }
}
