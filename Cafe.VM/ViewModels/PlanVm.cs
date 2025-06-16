using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.VM.ViewModels
{
    public class PlanVm
    {
        public int PlanID { get; set; }

        public string PlanName { get; set; }

        public string DescriptionPlan { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual List<CheckListMasterVm> CheckListMasters { get; set; } = new List<CheckListMasterVm>();

        public virtual List<EquipmentLocationPlanVm> EquipmentLocationPlans { get; set; } = new List<EquipmentLocationPlanVm>();
    }
}
