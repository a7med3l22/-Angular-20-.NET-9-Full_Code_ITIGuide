using Core_Layer.Models.ErrorsHandle;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReTypeAllByMe.Controllers
{
	[Route("error")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi =true)]
	public class ErrorsController : ControllerBase
	{

		[HttpGet("{ErrorCode}")]
		public IActionResult HandleError(int ErrorCode)
		{
			return new ObjectResult(new HandleErrors(ErrorCode)) { StatusCode=ErrorCode};
		}
	}
}
