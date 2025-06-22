using System;
using System.Collections.Generic;

namespace Cafe.EF.Models;

public partial class Table
{
    public int TabelID { get; set; }

    public int? LocationID { get; set; }

    public string Notes { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreationUserID { get; set; }

    public DateTime? CreationTime { get; set; }

    public int? UpdateUserID { get; set; }

    public DateTime? UpdateTime { get; set; }

    public int? DeletionUserID { get; set; }

    public DateTime? DeletionTime { get; set; }

    public string QRCode { get; set; }

    public virtual Location Location { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
