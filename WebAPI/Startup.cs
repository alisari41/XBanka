using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DependencyResolves;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encyption;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //Bizim API mize izin verdi�im yerin d���nda istek gelmesi problemi i�in g�venlik
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:3000"));//girdi�im siteden talet gelirse buna izin ver demek
            });


            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();//Bizim kendi 


            //token i�in 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters//configuresyon bilgilerini verriyorum
                    {
                        ValidateIssuer = true,//Bu Adama tok verdi�imiz zaten appsetting.jsonda "www.alisari.com" verid�im bilgiyi veriyorum
                        ValidateAudience = true,
                        ValidateLifetime = true,//Verdi�im Ya�am s�resini kortrol et
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,//Anahtar�da kotrol edeyim mi
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });

            //Kendi olu�turdu�um services
            services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule(),//Benim ba�ka CoreModule olursa orada ekleyebilirim 
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            //Kendi ekledi�im app. lar� tek seferde buradan s�n�f� ekleyerek olu�turuyorum
            app.ConfigureCustomExceptionMiddleware();

            //Cors(g�nvenlik) ��in Kulland�m s�ralamas� �nemli
            app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader());//Burdan gelen b�t�n isteklere izin ver


            app.UseHttpsRedirection();

            app.UseRouting();

            //S�ralamalar� �nemli giri� yapmadan i�lem yap�lmaz gibi d���n
            //Eklenmesi gerekir
            app.UseAuthentication();//Giri� izni gibi d���n giri� yapabilir

            //Bu yoksa bunuda ekle
            app.UseAuthorization();//Yetkidir ne yapabilir.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
