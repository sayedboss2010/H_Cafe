
using Cafe.Repository.Repositories.Employee;
using Cafe.Repository.Repositories.EmployeeType;
using Cafe.Repository.Repositories.kitchen;
using Cafe.Repository.Repositories.Location;
using Cafe.Repository.Repositories.Menu;
using Cafe.Repository.Repositories.MenuCafe;
using Cafe.Repository.Repositories.OrderType;
using Cafe.Repository.Repositories.Table;
using Cafe.Services.Employee;
using Cafe.Services.EmployeeType;
using Cafe.Services.kitchen;
using Cafe.Services.Location;
using Cafe.Services.Menu;
using Cafe.Services.MenuCafe;
using Cafe.Services.OrderType;
using Cafe.Services.Table;
using Cafe.VM.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cafe.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
      IConfiguration configuration)
    {



        services.AddDbContext<ErpDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DBConnection")));

        services.AddScoped<IHelperRepo, HelperRepo>();
        services.AddScoped<IHelperService, HelperService>();

        services.AddScoped<IAccountRepo, AccountRepo>();
        services.AddScoped<IAccountService, AccountService>();

        services.AddScoped<IMenuRepo, MenuRepo>();
        services.AddScoped<IMenuService, MenuService>();

        services.AddScoped<MenuCafeRepo, MenuCafeRepo>();
        services.AddScoped<MenuCafeService, MenuCafeService>();

        services.AddScoped<IGenericRepo<kitchenVM>, kitchenRepo>();
        services.AddScoped<IGenericService<kitchenVM>, kitchenService>();
        services.AddScoped<IOrderTypeRepo, OrderTypeRepo>();
        services.AddScoped<IOrderTypeService, OrderTypeService>();


        services.AddScoped<IEmployeeTypeRepo, EmployeeTypeRepo>();
        services.AddScoped<IEmployeeTypeService, EmployeeTypeService>();


        services.AddScoped<IEmployeeRepo, EmployeeRepo>();
        services.AddScoped<IEmployeeService, EmployeeService>();


        services.AddScoped<ITableRepo, TableRepo>();
        services.AddScoped<ITableService, TableService>();

        services.AddScoped<ILocationRepo, LocationRepo>();
        services.AddScoped<ILocationSerivce, LocationSerivce>();
        //Auto Mapper Configurations
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}