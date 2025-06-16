using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ERP.VM.ViewModels
{
    public class EquipmentLocationPlanVm
    {
        public int EquipmentLocationPlanID { get; set; }

        public int PlanID { get; set; }

        public int EquipmentLocationID { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual EquipmentLocationVm EquipmentLocation { get; set; }

        public virtual PlanVm Plan { get; set; }
    }
}
