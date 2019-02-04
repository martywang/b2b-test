using System.Collections.Generic;
using System.Linq;
using Audatex.B2B.SDK.FNOL.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Audatex.B2B.SDK.FNOL.Services
{
    public class AssignmentService
    {
        private readonly IMongoCollection<AssignmentEntity> _assignmentCollection;

        public AssignmentService(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("FNOLDb");
            _assignmentCollection = database.GetCollection<AssignmentEntity>("Assignments");
        }

        public List<AssignmentEntity> Get()
        {
            return _assignmentCollection.Find(assignment => true).ToList();
        }

        public AssignmentEntity Get(string id)
        {
            return _assignmentCollection.Find<AssignmentEntity>(assignment => assignment.Id == id).FirstOrDefault();
        }

        public AssignmentEntity Create(AssignmentEntity assignment)
        {
            _assignmentCollection.InsertOne(assignment);
            return assignment;
        }

        public void Update(string id, AssignmentEntity assignmentIn)
        {
            _assignmentCollection.ReplaceOne(assignment => assignment.Id == id, assignmentIn);
        }

        public void Remove(AssignmentEntity assignmentIn)
        {
            _assignmentCollection.DeleteOne(assignment => assignment.Id == assignmentIn.Id);
        }

        public void Remove(string id)
        {
            _assignmentCollection.DeleteOne(assignment => assignment.Id == id);
        }
    }
}