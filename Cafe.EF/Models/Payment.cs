using System;
using System.Collections.Generic;

namespace Cafe.EF.Models;

public partial class Payment
{
    public int PaymentID { get; set; }

    public int? OrderID { get; set; }

    public int? PaymentTypeID { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreationUserID { get; set; }

    public DateTime? CreationTime { get; set; }

    public int? UpdateUserID { get; set; }

    public DateTime? UpdateTime { get; set; }

    public int? DeletionUserID { get; set; }

    public DateTime? DeletionTime { get; set; }

    public virtual Order Order { get; set; }

    public virtual PaymentType PaymentType { get; set; }
}
