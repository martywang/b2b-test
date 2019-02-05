using Audatex.B2B.SDK.FNOL.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Audatex.B2B.SDK.FNOL.Entities
{
	public class AssignmentEntity : Entity
    {
        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }
    }
}
