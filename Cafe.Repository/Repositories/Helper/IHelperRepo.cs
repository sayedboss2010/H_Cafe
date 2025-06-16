using Cafe.VM;

namespace Cafe.Repository.Repositories.Helper;

public interface IHelperRepo
{
    string HashMd5(string pass);
    string GetSequencing(string seqName, string type);

    //string GetFirstTreasuryReciept();

    string NumberWithDigit(decimal number);

    List<DataToExcelVm> GetTablesToReadFrom();
    List<dynamic> WriteDataToExcel(DataToExcelVm dataToExcel);
}