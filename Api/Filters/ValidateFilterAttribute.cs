using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Core.Utilities.Results;

namespace Api.Filters
{
	public class ValidateFilterAttribute : ActionFilterAttribute
	{

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid)
			{
				var errors = string.Join(",", context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList()) ?? "" ;

				context.Result = new BadRequestObjectResult(new ErrorResult(errors));


			}
		}
	}
}
