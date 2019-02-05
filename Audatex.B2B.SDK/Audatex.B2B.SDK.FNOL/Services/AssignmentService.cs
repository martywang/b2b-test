using Audatex.B2B.SDK.FNOL.Entities;
using Audatex.B2B.SDK.FNOL.Models;
using Audatex.B2B.SDK.FNOL.Mongo;
using Audatex.B2B.SDK.FNOL.RabbitMQ;
using Rebus.Bus;
using System.Threading.Tasks;

namespace Audatex.B2B.SDK.FNOL.Services
{
	public class AssignmentService : IAssignmentService
	{
		private readonly IRepository<AssignmentEntity> _repository;
        private IBus bus;

		public AssignmentService(IRepository<AssignmentEntity> repository, IBus bus)
		{
			_repository = repository;
            this.bus = bus;
		}

		public async Task<string> AddAssignmentAsync(Assignment assignment)
		{   
            await bus.Publish(new AssignmentMessage()
            {
                FirstName = assignment.FirstName,
                LastName = assignment.LastName
            });

			var result = await _repository.AddAsync(new AssignmentEntity()
			{
				FirstName = assignment.FirstName,
				LastName = assignment.LastName
			});
			return result.Id.ToString();
		}
	}
}