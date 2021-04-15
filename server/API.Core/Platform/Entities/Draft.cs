using System;

namespace API.Core.Platform.Entities
{
    public class Draft
    {
        public Guid BuyerAccount { get; set; }

        public Guid SellerAccount { get; set; }

        public Guid VerifierAccount { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
