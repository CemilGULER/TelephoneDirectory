using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneDirectory.Common.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;


        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            //TODO: Şimdilik hata detayı direkt olarak client'a gönderilmesi sağlandı ileride sadece hata kodu dönmesi sağlanacak

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var json = new
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message
            };
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(json));
            
        }
    }


}

