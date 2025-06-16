using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.VM.ViewModels
{
    public class EmployeeVm
    {
        public int EmployeeID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string JobTitle { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual List<EquipmentLocationVm> EquipmentLocations { get; set; } = new List<EquipmentLocationVm>();

        public virtual List<EquipmentTransferVm> EquipmentTransfers { get; set; } = new List<EquipmentTransferVm>();
    }
}
