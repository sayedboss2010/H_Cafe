namespace ERP.VM;

public class PrUserVm
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public int? UserTypeId { get; set; }

    public int? SectorId { get; set; }

    public string FullName { get; set; }

    public long? EmployeesId { get; set; }

    //***************************************//
    public string AuthKey { get; set; }
    public int Sts { get; set; }
}
