using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {//ServiceTool vasıtasıyla .net Core un kendi servislerine erişiyorum
        public static IServiceProvider ServiceProvider { get; set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services; 
        }

    }
}
