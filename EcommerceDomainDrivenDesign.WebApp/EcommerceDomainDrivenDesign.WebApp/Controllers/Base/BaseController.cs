using System;
using System.Linq;
using EcommerceDomainDrivenDesign.Application.Base.Commands;
using EcommerceDomainDrivenDesign.Infrastructure.Identity.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceDomainDrivenDesign.WebApp.Controllers.Base
{
    public class BaseController : Controller
    {
        private readonly IUserProvider _userProvider;

        protected Guid UserId
        {
            get { return _userProvider.GetUserId(); }
        }

        protected BaseController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        protected new IActionResult Response(CommandHandlerResult result)
        {
            if (!result.ValidationResult.Errors.Any())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = result.ValidationResult.Errors
            });
        }

        protected new IActionResult Response(object result)
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }
    }
}
