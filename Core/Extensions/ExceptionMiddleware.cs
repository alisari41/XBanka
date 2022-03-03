using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;//sıradaki middleware

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        //startup.cs de eklenen service'ler hepsi middleware dir bizde kendi alanımızı arasına eklicez app.UseHttpsRedirection(); bunlar gibi
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext,e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {//çalıştırmaya çalıştırılan operasyon olurda bir hata verirse

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            string message = "Internal Server Error";//Bu şekilde kullanırsam magic string oluşur . Bir classtan almam daha mantıklı
            if (e.GetType()==typeof(ValidationException))
            {
                message = e.Message;
            }
            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
