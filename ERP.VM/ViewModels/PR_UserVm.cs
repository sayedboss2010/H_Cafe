using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.VM.ViewModels
{
    public partial class PR_UserVm
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int? UserTypeID { get; set; }

        public int? Sector_ID { get; set; }

        public string Full_Name { get; set; }

        public long? Employees_ID { get; set; }
    }
}
