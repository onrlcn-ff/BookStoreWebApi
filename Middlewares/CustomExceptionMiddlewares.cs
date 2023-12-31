using System.Xml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BookStoreWebApi.Services;

namespace BookStoreWebApi.Middlewares
{
    
    public class CustomExceptionMiddlewares
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _logger;

        public CustomExceptionMiddlewares(RequestDelegate next, ILoggerService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                string message = $"[Request]  HTTP {context.Request.Method} - {context.Request.Path}";
                _logger.Write(message);
                await _next.Invoke(context);
                watch.Stop();
                message = $"[Response] HTTP {context.Request.Method} - {context.Request.Path} Responded {context.Response.StatusCode} in {watch.Elapsed.TotalMilliseconds} ms ";
                _logger.Write(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleExeption(context, ex, watch);
                
            }
        }

        private  Task HandleExeption(HttpContext context, Exception ex, Stopwatch watch)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = $"[Error]  HTTP {context.Request.Method} - Responded {context.Response.StatusCode} Eror Message {ex.Message} in {watch.Elapsed.TotalMilliseconds} ms ";
            _logger.Write(message);
            
            var result = JsonConvert.SerializeObject(new {error = ex.Message}, Newtonsoft.Json.Formatting.None);
            return context.Response.WriteAsync(result);

        }
    }

    public static class CustomExceptionMiddlewaresExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddlewares(this IApplicationBuilder builder){
            return builder.UseMiddleware<CustomExceptionMiddlewares>();
        }
    }
}