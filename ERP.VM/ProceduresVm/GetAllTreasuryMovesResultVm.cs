namespace ERP.VM.ProceduresVm;

public class GetAllTreasuryMovesResultVm
{
    public long ID { get; set; }
    public long RecieptNumber { get; set; }
    public string Type { get; set; }
    public string Notes { get; set; }
    public DateTime? Date { get; set; }
    public DateTime? DeletionDate { get; set; }
    public decimal? Value { get; set; }
    public string EmployeeName { get; set; }
    public string MoveType { get; set; }
    public long? Overall_count { get; set; }

    public string ValueDigit { get; set; }
}
