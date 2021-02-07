using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/statusCode")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "抱歉，你訪問的頁面不存在";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.Query = statusCodeResult.OriginalQueryString;
                    ViewBag.PathBase = statusCodeResult.OriginalPathBase;
                    break;                   
            }
            return View("NotFound");
        }

           
        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
            ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            ViewBag.ExceptionStackTrace = exceptionHandlerPathFeature.Error.StackTrace;

            return View("Error");
        }

    }
}
