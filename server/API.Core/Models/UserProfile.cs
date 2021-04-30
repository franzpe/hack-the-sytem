using System;
using System.ComponentModel.DataAnnotations;
using API.Core.Platform.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Core.Models
{
    public class UserProfile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("IsVerifier")]
        public bool IsVerifier { get; set; }

        [BsonElement("UpdatedOn")]
        [DataType(DataType.DateTime)]
        public DateTime UpdatedOn { get; set; }

        [BsonElement("Status")]
        [DataType(DataType.DateTime)]
        public UserStatus Status { get; set; }
    }
}
