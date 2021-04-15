using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Core.Models
{
    public class Contract
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Description { get; set; }
        
        public string TargetedSum { get; set; }
        
        public DateTime ValidUntil { get; set; }

        public string PartnerEmail { get; set; }

        public string VerifierEmail { get; set; }

        public List<string> FileNames { get; set; }

        public string ManipulationFee { get; set; }
    }
}
