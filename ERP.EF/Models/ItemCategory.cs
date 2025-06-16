using System;
using System.Collections.Generic;

namespace ERP.EF.Models;

public partial class ItemCategory
{
    public int CategoryID { get; set; }

    public string NameEn { get; set; }

    public string NameAr { get; set; }

    public string DescriptionEn { get; set; }

    public string DescriptionAr { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreationUserID { get; set; }

    public DateTime? CreationTime { get; set; }

    public int? UpdateUserID { get; set; }

    public DateTime? UpdateTime { get; set; }

    public int? DeletionUserID { get; set; }

    public DateTime? DeletionTime { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
