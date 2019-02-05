using Audatex.B2B.SDK.FNOL.Entities;
using Audatex.B2B.SDK.FNOL.Models;
using Audatex.B2B.SDK.FNOL.Mongo;
using System.Threading.Tasks;

namespace Audatex.B2B.SDK.FNOL.Services
{
	public class AssignmentService : IAssignmentService
	{
		private readonly IRepository<AssignmentEntity> _repository;

		public AssignmentService(IRepository<AssignmentEntity> repository)
		{
			_repository = repository;
		}

		public async Task<string> AddAssignmentAsync(Assignment assignment)
		{
			var result = await _repository.AddAsync(new AssignmentEntity()
			{
				FirstName = assignment.FirstName,
				LastName = assignment.LastName
			});
			return result.Id.ToString();
		}
	}
}