using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.VM.ViewModels
{
    public class CheckListMasterVm
    {
        public int CheckListMasterID { get; set; }

        public int? CheckListID { get; set; }

        public int? LocationID { get; set; }

        public int? EquipmentID { get; set; }

        public int? EquipmentLocationID { get; set; }

        /// <summary>
        /// فيها مشكلة ام لا
        /// </summary>
        public bool? ProcessProblem { get; set; }

        public DateTime? ProcessDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// مين عمل مراجعه للعملية
        /// </summary>
        public int? ReviewedBy { get; set; }

        public DateTime? ReviewedAt { get; set; }

        /// <summary>
        /// الخطة
        /// </summary>
        public int? PlanID { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        /// <summary>
        /// الاجراءات كلها خلصت
        /// </summary>
        public bool? isClosed { get; set; }

        /// <summary>
        /// تاريخ غلق العملية
        /// </summary>
        public DateTime? isClosedDate { get; set; }

        public virtual CheckListVm CheckList { get; set; }

        public virtual List<CheckListMasterDetailVm> CheckListMasterDetails { get; set; } = new List<CheckListMasterDetailVm>();

        public virtual EquipmentLocationVm EquipmentLocation { get; set; }

        public virtual PlanVm Plan { get; set; }
    }
}
