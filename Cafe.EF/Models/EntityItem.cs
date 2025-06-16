using System;
using System.Collections.Generic;

namespace Cafe.EF.Models;

public partial class EntityItem
{
    public int EntityItemID { get; set; }

    public int? EntityID { get; set; }

    public int? ItemID { get; set; }

    public bool? Available { get; set; }

    public decimal? PriceOverride { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreationUserID { get; set; }

    public DateTime? CreationTime { get; set; }

    public int? UpdateUserID { get; set; }

    public DateTime? UpdateTime { get; set; }

    public int? DeletionUserID { get; set; }

    public DateTime? DeletionTime { get; set; }

    public virtual Entity Entity { get; set; }

    public virtual Item Item { get; set; }
}
