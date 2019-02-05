using Audatex.B2B.SDK.FNOL.Models;
using System.Threading.Tasks;

namespace Audatex.B2B.SDK.FNOL.Services
{
	public interface IAssignmentService
    {
		Task<string> AddAssignmentAsync(Assignment assignment);
	}
}
