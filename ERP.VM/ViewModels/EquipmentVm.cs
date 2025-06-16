using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.VM.ViewModels
{
    public class EquipmentVm
    {
        public int EquipmentID { get; set; }

        public string EquipmentName { get; set; }

        public string SerialNumber { get; set; }

        public int? EquipmentTypeID { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual List<EquipmentLocationVm> EquipmentLocations { get; set; } = new List<EquipmentLocationVm>();

        public virtual EquipmentTypeVm EquipmentType { get; set; }

        public virtual List<SparePartVm> SpareParts { get; set; } = new List<SparePartVm>();
    }
}
