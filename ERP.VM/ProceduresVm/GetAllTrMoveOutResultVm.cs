namespace ERP.VM.ProceduresVm;

public class GetAllTrMoveOutResultVm
{
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

    public string TotalValueDigit { get; set; }
    public string PaidValueDigit { get; set; }
    public string RemainValueDigit { get; set; }
}
