using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Errors.Model;

namespace GameDashboard.Application.Exceptions
{
    public class ExceptionMiddelware : IExceptionHandler
    {
        private readonly ILogger<ExceptionMiddelware> _logger;

        public ExceptionMiddelware(ILogger<ExceptionMiddelware> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"Exception occurred:{exception.Message}");

            var status = GetStatsusCode(exception);

            httpContext.Response.StatusCode = status;
            var problemDetails = new ProblemDetails();
            problemDetails.Status = status;
            if (exception.GetType() == typeof(ValidationException))
            {
                var errors = ((ValidationException)exception).Errors.Select(x => x.ErrorMessage).ToList();
                problemDetails.Title = string.Join(", ", errors);
            }
            else
                problemDetails.Title = $"{exception.Message}";


            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
        private static int GetStatsusCode(Exception exception) =>
           exception switch
           {
               BadRequestException => StatusCodes.Status400BadRequest,
               NotFoundException => StatusCodes.Status404NotFound,
               ValidationException => StatusCodes.Status422UnprocessableEntity,
               _ => StatusCodes.Status500InternalServerError
           };
    }


}
