using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.VM.ViewModels
{
    public class CheckListDataVM
    {
        public virtual List<EquipmentLocationVm> EquipmentLocationVm { get; set; } = new List<EquipmentLocationVm>();
        public virtual List<CheckListVm> CheckListVm { get; set; } = new List<CheckListVm>();


    }
}
