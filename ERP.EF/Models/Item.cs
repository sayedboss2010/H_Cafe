using System;
using System.Collections.Generic;

namespace ERP.EF.Models;

public partial class Item
{
    public int ItemID { get; set; }

    public string NameEn { get; set; }

    public string NameAr { get; set; }

    public decimal? Price { get; set; }

    public int? CategoryID { get; set; }

    public int? EntityID { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreationUserID { get; set; }

    public DateTime? CreationTime { get; set; }

    public int? UpdateUserID { get; set; }

    public DateTime? UpdateTime { get; set; }

    public int? DeletionUserID { get; set; }

    public DateTime? DeletionTime { get; set; }

    public virtual ItemCategory Category { get; set; }

    public virtual Entity Entity { get; set; }

    public virtual ICollection<EntityItem> EntityItems { get; set; } = new List<EntityItem>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
