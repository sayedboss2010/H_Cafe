
using Cafe.Repository.Repositories.Menu;
using Cafe.Services.Menu;
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