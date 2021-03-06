using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.Dapper;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolves.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<MusteriManager>().As<IMusteriService>();//Eğer birisi constructer'ında(yapıcı metod) IMusteriService isterse ona MusteriManager vericez
            builder.RegisterType<EfMusteriDal>().As<IMusteriDal>();


            builder.RegisterType<KanalManager>().As<IKanalService>();
            builder.RegisterType<EfKanalDal>().As<IKanalDal>();

            builder.RegisterType<AdresManager>().As<IAdresService>();
            builder.RegisterType<EfAdresDal>().As<IAdresDal>();

            builder.RegisterType<TuzelMusteriManager>().As<ITuzelMusteriService>();
            builder.RegisterType<DapperTuzelMusteriDal>().As<ITuzelMusteriDal>();

            builder.RegisterType<GercekMusteriManager>().As<IGercekMusteriService>();
            builder.RegisterType<DapperGercekMusteriDal>().As<IGercekMusteriDal>();

            builder.RegisterType<TicariFaliyetManager>().As<ITicariFaliyetService>();
            builder.RegisterType<DapperTicariFaliyetDal>().As<ITicariFaleyetDal>();

            builder.RegisterType<SendikaManager>().As<ISendikaService>();
            builder.RegisterType<EfSendikaDal>().As<ISendikaDal>();




            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            //Yukarıdaki nesneler için bir tane interceptor çalıştırcam
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();//Mevcut assembly'e ulaştım

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(
                new ProxyGenerationOptions()
                {
                    // Araya girecek olan nesneyi belirt
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance(); //Tek bir Instance oluştursun
        }
    }
}
