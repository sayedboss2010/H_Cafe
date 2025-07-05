using Cafe.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.VM.ViewModels
{
    public class LocationCafeVm
    {
        public int LocationID { get; set; }

        public string NameEn { get; set; }

        public string NameAr { get; set; }

        public int? EntityID { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public int? CreationUserID { get; set; }

        public DateTime? CreationTime { get; set; }

        public int? UpdateUserID { get; set; }

        public DateTime? UpdateTime { get; set; }

        public int? DeletionUserID { get; set; }

        public DateTime? DeletionTime { get; set; }

        public string QRCode { get; set; }

        public virtual Entity Entity { get; set; }

        public virtual ICollection<Table> Tables { get; set; } = new List<Table>();
    }
}
