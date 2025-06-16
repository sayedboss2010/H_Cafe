using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.VM.ViewModels
{
    public class LocationVm
    {
        public int LocationID { get; set; }

        public string LocationName { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual List<EquipmentLocationVm> EquipmentLocations { get; set; } = new List<EquipmentLocationVm>();

        public virtual List<StoreVm> Stores { get; set; } = new List<StoreVm>();
    }
}
