using ERP.VM;

namespace ERP.Services.Helper;

public interface IHelperService
{
    string HashMd5(string pass);
    //string GetFirstTreasuryReciept();

    string NumberWithDigit(decimal number);

    List<DataToExcelVm> GetTablesToReadFrom();
    List<dynamic> WriteDataToExcel(DataToExcelVm dataToExcel);
}