using ForApplication.BusinecLogic.Services;
using ForApplication.Data;
using ForApplication.DataAccess;
using ForApplication.PresentationLogic.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForApplication;

public class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath("C:\\Users\\azama\\OneDrive\\Desktop\\Projects\\ForApplication")
            .AddJsonFile("AppSetting.json");

        IConfigurationRoot configurationRoot = configuration.Build();

        var section = configurationRoot.GetSection("ConnectionStrings");

        var connectionString = section["MyConnectionString"];

        var serviceProvider = new ServiceCollection()
            .AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            })
            .AddScoped<IProductDataAccess, ProductDataAccess>()
            .AddScoped<IProductServic, ProductServic>()
            .AddScoped<ProductView>()
            .BuildServiceProvider();

        var productView = serviceProvider.GetService<ProductView>();
        await productView.Menu();
    }
}