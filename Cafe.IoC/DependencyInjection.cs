
using Cafe.Repository.Repositories.kitchen;
using Cafe.Repository.Repositories.Menu;
using Cafe.Repository.Repositories.MenuCafe;
using Cafe.Services.kitchen;
using Cafe.Services.Menu;
using Cafe.Services.MenuCafe;
using Cafe.VM.ViewModels;

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