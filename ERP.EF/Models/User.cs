using System;
using System.Collections.Generic;

namespace ERP.EF.Models;

public partial class User
{
    public int UserID { get; set; }

    public string UserName { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }
}
