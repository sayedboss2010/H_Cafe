using ERP.Repository.Repositories.Helper;
using ERP.VM;

namespace ERP.Services.Helper;

public class HelperService : IHelperService
{
    private readonly IHelperRepo _helper;

    public HelperService(IHelperRepo helper)
    {
        _helper = helper;
    }

    //public string GetFirstTreasuryReciept()
    //{
    //    try
    //    {
    //        return _helper.GetFirstTreasuryReciept();
    //    }
    //    catch (Exception)
    //    {
    //        return "";
    //    }
    //}

    public List<DataToExcelVm> GetTablesToReadFrom()
    {
        try
        {
            return _helper.GetTablesToReadFrom();
        }
        catch (Exception)
        {
            return new List<DataToExcelVm>();
        }
    }

    public string HashMd5(string pass)
    {
        return _helper.HashMd5(pass);
    }

    public string NumberWithDigit(decimal number)
    {
        try
        {
            return _helper.NumberWithDigit(number);
        }
        catch (Exception)
        {
            return "";
        }
    }

    public List<dynamic> WriteDataToExcel(DataToExcelVm dataToExcel)
    {
        try
        {
            return _helper.WriteDataToExcel(dataToExcel);
        }
        catch (Exception)
        {
            return new List<dynamic>();
        }
    }
}