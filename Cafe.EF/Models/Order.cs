using System;
using System.Collections.Generic;

namespace Cafe.EF.Models;

public partial class Order
{
    public int OrderID { get; set; }

    public int? OrderTypeID { get; set; }

    public int? TabelID { get; set; }

    public DateTime? OrderDate { get; set; }

    public string Notes { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreationUserID { get; set; }

    public DateTime? CreationTime { get; set; }

    public int? UpdateUserID { get; set; }

    public DateTime? UpdateTime { get; set; }

    public int? DeletionUserID { get; set; }

    public DateTime? DeletionTime { get; set; }

    public bool? ISDone { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual OrderType OrderType { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Table Tabel { get; set; }
}
