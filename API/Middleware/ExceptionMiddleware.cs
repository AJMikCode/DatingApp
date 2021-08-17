using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        //  RequestDelegate is whats coming up next in the middleware pipeline, brought in using Microsoft.AspNetCore.Http
        //  ILogger<ExceptionMiddleware>, so we can still log out exception to the middleware. using Microsoft.Extensions.Logging
        //   IHostEnvironment is used to see what environment we're in. using Microsoft.Extensions.Hosting
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
           try
           {
               //   First thing to do is get context and pass onto next piece of middleware.
               await _next(context);
           } 
           catch(Exception ex)
           {
               //   This makes terminal to not be silent and actually work. Will show error in terminal.
               _logger.LogError(ex, ex.Message);
               context.Response.ContentType = "application/json";
               context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

               var response = _env.IsDevelopment()
                    //   Question mark represents what will happen if it if development mode as the repsonse shows.
                        //  Second question mark represents to not throw an exception on stacktrace because it may breka the internet
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    //  Represents if in consctruction mode and not development mode
                    : new ApiException(context.Response.StatusCode, "Internal Server Error");

                // Makes sure response goes back as a normal json formatted response in camelcase 
                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                var json = JsonSerializer.Serialize(response, options);

                //  Acts as a return statement for InvokeAsync() method
                await context.Response.WriteAsync(json);
           }
        }
    }
}