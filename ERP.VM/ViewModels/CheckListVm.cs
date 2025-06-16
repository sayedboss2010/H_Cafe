using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.VM.ViewModels
{
    public class CheckListVm
    {
        public int CheckListID { get; set; }

        public int? EquipmentTypeID { get; set; }

        public string CheckItem { get; set; }

        public bool? IsRequired { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual List<CheckListMasterVm> CheckListMasters { get; set; } = new List<CheckListMasterVm>();

        public virtual EquipmentTypeVm EquipmentType { get; set; }
    }
}
