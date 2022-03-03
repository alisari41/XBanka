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
            //Bizim API mize izin verdiðim yerin dýþýnda istek gelmesi problemi için güvenlik
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:3000"));//girdiðim siteden talet gelirse buna izin ver demek
            });


            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();//Bizim kendi 


            //token için 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters//configuresyon bilgilerini verriyorum
                    {
                        ValidateIssuer = true,//Bu Adama tok verdiðimiz zaten appsetting.jsonda "www.alisari.com" veridðim bilgiyi veriyorum
                        ValidateAudience = true,
                        ValidateLifetime = true,//Verdiðim Yaþam süresini kortrol et
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,//Anahtarýda kotrol edeyim mi
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });

            //Kendi oluþturduðum services
            services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule(),//Benim baþka CoreModule olursa orada ekleyebilirim 
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

            //Kendi eklediðim app. larý tek seferde buradan sýnýfý ekleyerek oluþturuyorum
            app.ConfigureCustomExceptionMiddleware();

            //Cors(günvenlik) Ýçin Kullandým sýralamasý önemli
            app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader());//Burdan gelen bütün isteklere izin ver


            app.UseHttpsRedirection();

            app.UseRouting();

            //Sýralamalarý önemli giriþ yapmadan iþlem yapýlmaz gibi düþün
            //Eklenmesi gerekir
            app.UseAuthentication();//Giriþ izni gibi düþün giriþ yapabilir

            //Bu yoksa bunuda ekle
            app.UseAuthorization();//Yetkidir ne yapabilir.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
