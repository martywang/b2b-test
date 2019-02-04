using Audatex.B2B.SDK.FNOL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Audatex.B2B.SDK.FNOL.Controllers
{
	public class FnolController : Controller
    {
		// POST api/values
		[HttpPost("/assignments")]
		public void AddAssignment([FromBody] Assignment assignment)
		{
		}
	}
}