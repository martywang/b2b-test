using Audatex.B2B.SDK.FNOL.Entities;
using Audatex.B2B.SDK.FNOL.Models;
using Audatex.B2B.SDK.FNOL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Audatex.B2B.SDK.FNOL.Controllers
{
	public class FnolController : Controller
    {
		private readonly IAssignmentService _assignmentService;

		public FnolController(IAssignmentService assignmentService)
		{
			_assignmentService = assignmentService;
		}

		[HttpPost("/assignments")]
		public Task AddAssignment([FromBody] Assignment assignment)
		{
			return _assignmentService.AddAssignmentAsync(assignment);
		}
	}
}