namespace ERP.VM;

public class DataToExcelVm
{
    public int Id { get; set; }

    public string TableName { get; set; }
    public string EnttityName { get; set; }

    public long? LastReadedId { get; set; }
}