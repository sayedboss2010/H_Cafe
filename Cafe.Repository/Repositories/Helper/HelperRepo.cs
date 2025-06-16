using Cafe.EF.Models;
using Cafe.VM;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography;
using System.Text;

namespace Cafe.Repository.Repositories.Helper;

public class HelperRepo : IHelperRepo
{
    private const string tempKey = "Cit$$50/*Sed#dN#_JeX!!X0$c145^2@@@!!eCaeNaappa";

    //public string GetFirstTreasuryReciept()
    //{
    //    using var dbContext = new ErpDbContext();
    //    return dbContext.GrSystemCodes.Find(7).Value;
    //}

    public string GetSequencing(string seqName, string type)
    {
        var dqlType = type switch
        {
            "byte" => System.Data.SqlDbType.TinyInt,
            "short" => System.Data.SqlDbType.SmallInt,
            "long" => System.Data.SqlDbType.BigInt,
            _ => System.Data.SqlDbType.Int
        };

        var p = new SqlParameter("@result", dqlType);
        p.Direction = System.Data.ParameterDirection.Output;

        using var dbContext = new ErpDbContext();
        // execute the SQL query that sets the parameter to the next value for the sequence
        dbContext.Database.ExecuteSqlRaw("set @result = next value for dbo." + seqName, p);

        // get the parameter value from your code
        var nextVal = p.Value;

        return nextVal.ToString();
    }

    public string HashMd5(string pass)
    {
        var sha1 = MD5.Create();
        var step1 = Encoding.UTF8.GetBytes(pass + tempKey);
        var step2 = sha1.ComputeHash(step1);
        return string.Join("", step2.Select(x => x.ToString("X2"))).ToLower();
    }

    //************************************************//
    public List<DataToExcelVm> GetTablesToReadFrom()
    {
        using var dbContext = new ErpDbContext();
        return null;
            //dbContext.DataToExcels
            //.Select(d => new DataToExcelVm
            //{
            //    Id = d.Id,
            //    TableName = d.TableName,
            //    LastReadedId = d.LastReadedId,
            //    //EnttityName = d.EnttityName,
            //})
            //.ToList();
    }

    public List<dynamic> WriteDataToExcel(DataToExcelVm dataToExcel)
    {
        using var dbContext = new ErpDbContext();

        var tableClassName = $"Cafe.EF.Models.{dataToExcel.EnttityName}";

        Type dynamicTableType = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(t => t.GetTypes())
            .First(t => string.Equals(t.Name, tableClassName, StringComparison.Ordinal));

        var dynamicTable = dbContext.Set(dynamicTableType);     // DbSet

        var records = dynamicTable
              .AsQueryable()
              .ToDynamicList()
              .ToList();

        return records;
    }

    //************************************************//

    public string NumberWithDigit(decimal number)
    {
        string num_str = number.ToString();
        string right_number = num_str.Split('.')[0];
        string right_number_with_digit = "";
        for (int i = right_number.Length - 1, j = 1; i >= 0; i--, j++)
        {
            right_number_with_digit = right_number[i] + right_number_with_digit;
            if ((j % 3) == 0 && (i - 1) >= 0)
            {
                right_number_with_digit = "," + right_number_with_digit;
            }

        }
        if (num_str.Contains(".") && double.Parse(num_str.Split('.')[1]) > 0)

            return right_number_with_digit + "." + num_str.Split('.')[1];
        else
            return right_number_with_digit;
    }
}