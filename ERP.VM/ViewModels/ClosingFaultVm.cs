using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.VM.ViewModels
{
    public class ClosingFaultVm
    {
        public int CheckListMasterID { get; set; }
        public int? CheckListID { get; set; }
        public int? EquipmentLocationID { get; set; }
        public string CreatedBy { get; set; }
        public string CheckItem { get; set; }
        public int? EquipmentTypeID { get; set; }
        public string TypeName { get; set; }
        public string EquipmentName { get; set; }
        public string LocationName { get; set; }
        public string CreatedAt { get; set; }
        public string planName { get; set; }
        public int CheckListMasterDetailID { get; set; }
        public IFormFile oldImage { get; set; }
        public IFormFile newImage { get; set; }

        public int CreatedByuser { get; set; }

        public List<CheckListMasterDetailVm> CheckListMasterDetails { get; set; } = new List<CheckListMasterDetailVm>();


    }
}
