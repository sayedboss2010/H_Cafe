using AutoMapper;
using ERP.EF.Models;
using ERP.VM;
using ERP.VM.ProceduresVm;
using ERP.VM.ViewModels;

namespace ERP.Repository.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Add as many of these lines as you need to map your objects
        CreateMap<PR_User, PrUserVm>().ReverseMap();



        //srored result classes
        CreateMap<GetAllEmployeesResult, GetAllEmployeesResultVm>().ReverseMap();
        CreateMap<GetAllTrMoveOutResult, GetAllTrMoveOutResultVm>().ReverseMap();
        CreateMap<GetAllTreasuryMovesResult, GetAllTreasuryMovesResultVm>().ReverseMap();
        CreateMap<GetAllShipmentBillsResult, GetAllShipmentBillsResultVm>().ReverseMap();
        CreateMap<GetAllCostsReportResult, GetAllCostsReportResultVm>().ReverseMap();

        CreateMap<DrawMenuResul, DrawMenuResulVm>().ReverseMap();
    }
}