using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolves
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {//Startup.cs de services.Add  ekleyebildiğim şeylerin Merkezi olanlarını buradan eklemeye çalışıyorum
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager,MemoryCacheManager>();// birisi benden ICacheManager isterse ona MemoryCacheManager ver. Yarın öbür gün RedisCacheManager geçersek değiştirmemiz bütün sistemin redis'e geçmesini sağlayacak

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<Stopwatch>();
        }
    }
}
