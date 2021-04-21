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
        public Guid Id { get; set; }
        public string Description { get; set; }
        
        [Required]
        public string TargetedSum { get; set; }

        [Required]
        public DateTime ValidUntil { get; set; }

        [Required]
        public Guid BuyerId { get; set; }

        [Required]
        public Guid SellerId { get; set; }

        [Required]
        public Guid VerifierId { get; set; }

        public List<string> FileNames { get; set; }

        public string ManipulationFee { get; set; }

        public ContractStatus Status { get; set; }

        public DateTime UpdatedOn { get; set; }

        public bool PartnerAccepted { get; set; }

        public bool VerifierAccepted { get; set; }
    }
}
