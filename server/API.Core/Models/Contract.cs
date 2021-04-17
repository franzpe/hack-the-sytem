using System;
using System.Collections.Generic;
using API.Core.Platform.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Core.Models
{
    public class Contract
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }
        public string Description { get; set; }
        
        public string TargetedSum { get; set; }
        
        public DateTime ValidUntil { get; set; }

        public Guid BuyerId { get; set; }

        public Guid SellerId { get; set; }

        public Guid VerifierId { get; set; }

        public List<string> FileNames { get; set; }

        public string ManipulationFee { get; set; }

        public ContractStatus Status { get; set; }

        public DateTime UpdatedOn { get; set; }

        public bool PartnerAccepted { get; set; }

        public bool VerifierAccepted { get; set; }
    }
}
