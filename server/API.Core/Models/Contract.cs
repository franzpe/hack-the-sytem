using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using API.Core.Platform.Enums;
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
        
        [Required]
        [BsonElement("TargetedSum")]
        public string TargetedSum { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [BsonElement("ValidUntil")]
        public DateTime ValidUntil { get; set; }

        [Required]
        [BsonElement("BuyerId")]
        public string BuyerId { get; set; }

        [Required]
        [BsonElement("SellerId")]
        public string SellerId { get; set; }

        [Required]
        [BsonElement("VerifierId")]
        public string VerifierId { get; set; }

        [BsonElement("FileNames")]
        public List<string> FileNames { get; set; }

        [BsonElement("ManipulationFee")]
        public string ManipulationFee { get; set; }

        [BsonElement("Status")]
        public ContractStatus Status { get; set; }

        [BsonElement("UpdatedOn")]
        public DateTime UpdatedOn { get; set; }

        [BsonElement("PartnerAccepted")]
        public bool PartnerAccepted { get; set; }

        [BsonElement("VerifierAccepted")]
        public bool VerifierAccepted { get; set; }
    }
}
