using AutoMapper;
using Cafe.EF.Models;
using Cafe.VM;
using Cafe.VM.ProceduresVm;
using Cafe.VM.ViewModels;

namespace Cafe.Repository.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Add as many of these lines as you need to map your objects
        CreateMap<PR_User, PrUserVm>().ReverseMap();
        CreateMap<OrderType, OrderTypeVm>().ReverseMap();
        CreateMap<EmployeeType, EmployeeTypeVm>().ReverseMap();
        CreateMap<Employee, EmployeeCafeVm>().ReverseMap();



        //srored result classes
        CreateMap<GetAllEmployeesResult, GetAllEmployeesResultVm>().ReverseMap();
        CreateMap<GetAllTrMoveOutResult, GetAllTrMoveOutResultVm>().ReverseMap();
        CreateMap<GetAllTreasuryMovesResult, GetAllTreasuryMovesResultVm>().ReverseMap();
        CreateMap<GetAllShipmentBillsResult, GetAllShipmentBillsResultVm>().ReverseMap();
        CreateMap<GetAllCostsReportResult, GetAllCostsReportResultVm>().ReverseMap();

        CreateMap<DrawMenuResul, DrawMenuResulVm>().ReverseMap();
    }
}