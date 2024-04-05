using FactoryDI.Library;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDI;

public class ServiceManager
{
    protected ServiceManager() { }
    public static IServiceProvider CreateServiceProvider()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(ConfigureService)
            .Build();

        return host.Services;
    }

    private static void ConfigureService(IServiceCollection services)
    {
        services.AddOptions<LabelGenOptions>().BindConfiguration("LabelGenOptions");

        services.AddSingleton(serviceProvider => 
        {
            var options = serviceProvider.GetService<IOptions<LabelGenOptions>>()!.Value;

            return new LabelGenService(options.Prefix, options.Suffix);
        });
    }
}
