using System;
using System.Collections.Generic;

namespace ERP.EF.Models;

public partial class Entity
{
    public int EntityID { get; set; }

    public string NameEn { get; set; }

    public string NameAr { get; set; }

    public string DescriptionEn { get; set; }

    public string DescriptionAr { get; set; }

    public string Phone { get; set; }

    public string AddressEn { get; set; }

    public string AddressAr { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreationUserID { get; set; }

    public DateTime? CreationTime { get; set; }

    public int? UpdateUserID { get; set; }

    public DateTime? UpdateTime { get; set; }

    public int? DeletionUserID { get; set; }

    public DateTime? DeletionTime { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<EntityItem> EntityItems { get; set; } = new List<EntityItem>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}
