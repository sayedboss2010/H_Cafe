using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.VM.ViewModels
{
    public class SparePartTypeVm
    {
        public int SparePartTypeID { get; set; }

        public string TypeName { get; set; }

        public string Description { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual List<SparePartVm> SpareParts { get; set; } = new List<SparePartVm>();
    }
}
