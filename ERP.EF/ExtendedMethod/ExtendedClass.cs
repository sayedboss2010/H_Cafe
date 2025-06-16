using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ERP.EF.Models;

public partial class ErpDbContext
{
    public virtual DbSet<GetAllEmployeesResult> GetAllEmployeesResult { get; set; }
    public virtual DbSet<GetAllTrMoveOutResult> GetAllTrMoveOutResult { get; set; }
    public virtual DbSet<GetAllTreasuryMovesResult> GetAllTreasuryMovesResult { get; set; }
    public virtual DbSet<GetAllShipmentBillsResult> GetAllShipmentBillsResult { get; set; }
    public virtual DbSet<GetAllCostsReportResult> GetAllCostsReportResult { get; set; }
    public virtual DbSet<DrawMenuResul> DrawMenuResul { get; set; }
    public virtual DbSet<ModulResult> ModulResult { get; set; }
    public virtual DbSet<SubMenuResult> SubMenuResult { get; set; }
}

public static class DBContextExtensions
{
    public static IQueryable<Object> Get(this ErpDbContext _context, Type t)
    {
        return (IQueryable<Object>)_context.GetType().GetMethod($"get_{t.Name}").Invoke(_context, null);
    }

    // Or the modified set which handles ambigous ref issues:
    public static IQueryable<Object> Set(this ErpDbContext _context, Type t)
    {
        return (IQueryable<Object>)_context.GetType().GetMethods()
                .First(x => x.Name == "Set" && x.ContainsGenericParameters)
                .MakeGenericMethod(t).Invoke(_context, null);
    }
}

public class GetAllEmployeesResult
{
    [Key]
    public long Id { get; set; }

    public long? EmpCode { get; set; }

    public string NameAr { get; set; }

    public string NameEn { get; set; }

    public string AddressAr { get; set; }

    public string AddressEn { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public DateTime? Birthdate { get; set; }

    public DateTime? HireDate { get; set; }

    public int? CurrentJobId { get; set; }

    public int? CurrentBranchDeptId { get; set; }
    public decimal? CurrentSalary { get; set; }

    public bool IsManager { get; set; }

    public bool IsActive { get; set; }

    public string JobNameAr { get; set; }
    public string JobNameEn { get; set; }
    public string BranchNameAr { get; set; }
    public string BranchNameEn { get; set; }
    public string DepartmentNameAr { get; set; }
    public string DepartmentNameEn { get; set; }
    public long? Overall_count { get; set; }
}

public class GetAllTrMoveOutResult
{
    [Key]
    public long Id { get; set; }
    public long ReceiptId { get; set; }

    public DateTime? MoveOutDate { get; set; }
    public DateTime? DeletionDate { get; set; }

    public byte? TypeId { get; set; }
    public string ImgPath { get; set; }

    public string DestintionMoveout { get; set; }
    public string TypeText { get; set; }
    public string TypeName { get; set; }

    public int? CurrencyId { get; set; }
    public string CurrencyName { get; set; }

    public decimal? TotalValue { get; set; }
    public decimal? PaidValue { get; set; }

    public bool? IS_Need_Adjustment { get; set; }

    public long? Overall_count { get; set; }
}

public class GetAllTreasuryMovesResult
{
    [Key]
    public long ID { get; set; }
    public long? RecieptNumber { get; set; }
    public string Type { get; set; }
    public string Notes { get; set; }
    public DateTime? Date { get; set; }
    public DateTime? DeletionDate { get; set; }
    public decimal? Value { get; set; }
    public string EmployeeName { get; set; }
    public string MoveType { get; set; }
    public long? Overall_count { get; set; }
}

public class GetAllShipmentBillsResult
{
    [Key]
    public long Id { get; set; }

    public string RecipetNumber { get; set; }

    public string Name { get; set; }

    public string Company { get; set; }

    public string ReplacmentCompany { get; set; }

    public long? AcidNum { get; set; }

    public DateTime? AcidDate { get; set; }

    public DateTime? OutDate { get; set; }

    public DateTime? ArrivalDate { get; set; }

    public string ArrivalPort { get; set; }

    public string Notes { get; set; }

    public string ImgPath { get; set; }

    public bool BookShip { get; set; }

    public bool ReviewPapers { get; set; }

    public bool PaperShipment { get; set; }

    public bool PaperArrival { get; set; }

    public bool PaperToAdmin { get; set; }

    public bool AdminApproval { get; set; }

    public bool SendPaper { get; set; }

    public bool RecieveShipment { get; set; }

    public string RecieveShipmentTxt { get; set; }
    public long? Overall_count { get; set; }
}

public class GetAllCostsReportResult
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal? TotalCost { get; set; }

    public long? Overall_count { get; set; }

}

public class DrawMenuResul
{
    [Key]
    public int MenuID { get; set; }
    public int GroupID { get; set; }
    public string GroupName { get; set; }
    public string MenuTitle { get; set; }
    public string MenuURL { get; set; }
}

public class ModulResult
{
    public int Id { get; set; }
    public string ModuleName { get; set; }
}

public class SubMenuResult
{
    public int? Id { get; set; }
    public string MenuTitle { get; set; }
    public string MenuURL { get; set; }
}