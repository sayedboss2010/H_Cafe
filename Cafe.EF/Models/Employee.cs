using System;
using System.Collections.Generic;

namespace Cafe.EF.Models;

public partial class Employee
{
    public int EmployeeID { get; set; }

    public string NameEn { get; set; }

    public string NameAr { get; set; }

    public string Phone { get; set; }

    public int? EmployeeTypeID { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreationUserID { get; set; }

    public DateTime? CreationTime { get; set; }

    public int? UpdateUserID { get; set; }

    public DateTime? UpdateTime { get; set; }

    public int? DeletionUserID { get; set; }

    public DateTime? DeletionTime { get; set; }

    public int? EntityID { get; set; }

    public virtual EmployeeType EmployeeType { get; set; }

    public virtual Entity Entity { get; set; }
}
