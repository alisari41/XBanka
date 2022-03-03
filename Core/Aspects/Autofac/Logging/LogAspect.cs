using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using Core.Utilities.Results;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {//genelde metod çağırıldığında kullanılır. İş ihtiyaçlarına göre değişir
        private LoggerServiceBase _loggerServiceBase;

        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))//LoggerServiceBase türünde değilse
            {
                throw new System.Exception(AspectMessages.WrongLoggerType);
            }
            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
        }

        //protected override void OnBefore(IInvocation invocation)
        //{
        //    _loggerServiceBase.Info(GetLogDetail(invocation));//istediğim log bilgisini geçebilirim
        //}

        public override void Intercept(IInvocation invocation)
        {//Eğer Add metodu ekleme yapmazsa log dosyasına ekleme
            invocation.Proceed();

            var returnValue = (IResult)invocation.ReturnValue;

            if (returnValue.Success)
            {
                _loggerServiceBase.Info(GetLogDetail(invocation));
            }
        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {

            var logParameteres = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameteres.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }
            var logDetail = new LogDetail
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameteres
            };
            return logDetail;
        }
    }
}
