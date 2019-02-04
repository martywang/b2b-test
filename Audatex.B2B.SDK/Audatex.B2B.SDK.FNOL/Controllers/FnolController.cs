using Audatex.B2B.SDK.FNOL.Entities;
using Audatex.B2B.SDK.FNOL.Models;
using Audatex.B2B.SDK.FNOL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Audatex.B2B.SDK.FNOL.Controllers
{
	public class FnolController : Controller
    {
		// POST api/values
		[HttpPost("/assignments")]
		public void AddAssignment([FromBody] Assignment assignment)
		{
            var service = new AssignmentService("mongodb://localhost:27017");
            var entity = new AssignmentEntity() { FirstName = assignment.FirstName, LastName = assignment.LastName };
            service.Create(entity);
		}
	}
}