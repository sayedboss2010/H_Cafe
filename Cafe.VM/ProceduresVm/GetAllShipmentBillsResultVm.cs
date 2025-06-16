namespace Cafe.VM.ProceduresVm;

public class GetAllShipmentBillsResultVm
{
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