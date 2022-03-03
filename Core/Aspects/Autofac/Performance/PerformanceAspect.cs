using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceAspect : MethodInterception
    {//Bir metodun başından sonuna ne kadar süre geçiyor onu hesaplamamız gerekiyor
        private int _interval;// geçen süreyi tutucam
        private Stopwatch _stopwatch; //klinometre görevi görecek

        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }


        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds>_interval)//Saniye cinsinden geçen süre _interval dan büyük mü
            {
                Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");//geçen süreyi mail attırabiliriz
            }
            _stopwatch.Reset();
        }
    }
}
