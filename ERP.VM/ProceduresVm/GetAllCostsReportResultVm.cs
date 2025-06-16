namespace ERP.VM.ProceduresVm;

public class GetAllCostsReportResultVm
{
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal? TotalCost { get; set; }

    public long? Overall_count { get; set; }

    public string TotalCostDigit { get; set; }
}
