using System;
using System.Threading.Tasks;
using API.Core.Models;
using API.Core.Platform.Entities;
using API.Core.Platform.Validators;

namespace API.Core.Orchestrators
{
    public class DraftOrchestrator : IDraftOrchestrator
    {
        private readonly IContractValidator _contractValidator;

        public DraftOrchestrator(IContractValidator contractValidator)
        {
            _contractValidator = contractValidator;
        }

        public async Task<OrchestratorResult<Draft>> CreateDraft(string buyerEmail, string sellerEmail,
            string verifierEmail, DateTime validUntil, string description)
        {
            var result = new OrchestratorResult<Draft>();
            
            if (string.IsNullOrEmpty(buyerEmail) || _contractValidator.IsValidEmail(buyerEmail))
            {
                return result.Error("Buyer's email was empty or wrong format");
            }

            if (string.IsNullOrEmpty(sellerEmail) || _contractValidator.IsValidEmail(buyerEmail))
            {
                return result.Error("Seller's email was empty or wrong format");
            }

            if (string.IsNullOrEmpty(verifierEmail) || _contractValidator.IsValidEmail(buyerEmail))
            {
                return result.Error("Verifier's email was empty or wrong format");
            }


            //TODO: Treba vyhladat podla emailu asi alebo niecoho 
            var draft = new Draft
            {
                BuyerAccount = new Guid(),
                SellerAccount = new Guid(),
                VerifierAccount = new Guid(),
                CreatedOn = DateTime.UtcNow
            };

            result.Model = draft;
            return result;
        }
    }
}