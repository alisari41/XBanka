using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation :MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(','); // Virgül ile ayır onları rol array'ine at
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {//Başlamadan önce
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            //burada kullanıcın talep ettiği herhangi bir rol var mı onlara bakmak lazım
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))//Role sahipse
                {
                    return;
                }
            }
            //rolü yoksa 
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
