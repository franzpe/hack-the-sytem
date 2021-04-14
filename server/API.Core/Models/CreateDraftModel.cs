using System;
using System.Collections.Generic;

namespace API.Core.Models
{
    public class CreateDraftModel
    {
        public string Description { get; set; }
        
        public string TargetedSum { get; set; }
        
        public DateTime ValidUntil { get; set; }

        public string PartnerEmail { get; set; }

        public string VerifierEmail { get; set; }

        public List<string> FileNames { get; set; }

        public string ManipulationFee { get; set; }
    }
}
