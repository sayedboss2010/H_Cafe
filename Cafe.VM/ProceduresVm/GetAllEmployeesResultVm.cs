namespace Cafe.VM.ProceduresVm;

public class GetAllEmployeesResultVm
{
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