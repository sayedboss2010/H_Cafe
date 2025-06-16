using System;
using System.Collections.Generic;

namespace Cafe.EF.Models;

public partial class OrderDetail
{
    public int OrderDetailID { get; set; }

    public int? OrderID { get; set; }

    public int? ItemID { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? Total { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreationUserID { get; set; }

    public DateTime? CreationTime { get; set; }

    public int? UpdateUserID { get; set; }

    public DateTime? UpdateTime { get; set; }

    public int? DeletionUserID { get; set; }

    public DateTime? DeletionTime { get; set; }

    public virtual Item Item { get; set; }

    public virtual Order Order { get; set; }
}
