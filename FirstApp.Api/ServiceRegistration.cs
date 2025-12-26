using FirstApp.Api.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FirstApp.Api.Profiles;

namespace FirstApp.Api;

public static class ServiceRegistration
{
    public static void AddApiServices(this IServiceCollection services,IConfiguration config)
    {
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"))
        );
        services.AddAutoMapper(opt =>
        {
            opt.AddProfile<MapProfile>();
        }
        );
    }
}
