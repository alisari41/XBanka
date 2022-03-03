using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services, ICoreModule[] modules)
        {//En son bunu Startup.cs den ConfigureServices metodunda en altta çağır services.AddDependencyResolvers(); diye
            foreach (var module in modules)
            {
                module.Load(services);//modullerdeki tüm modülleri services'e yükle
            }

            return ServiceTool.Create(services); 
        }
    }
}
