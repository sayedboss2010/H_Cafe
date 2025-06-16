using System;
using System.Collections.Generic;

namespace Cafe.EF.Models;

public partial class PR_User
{
    public int ID { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public int? UserTypeID { get; set; }

    public int? Sector_ID { get; set; }

    public string Full_Name { get; set; }

    public long? Employees_ID { get; set; }
}
