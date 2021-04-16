using System;
using System.Threading.Tasks;
using API.Core.Models;
using API.Core.Platform.Entities;

namespace API.Core.Orchestrators
{
    public interface IDraftOrchestrator
    {
        Task<OrchestratorResult<Draft>> CreateDraft(string buyerEmail, string sellerEmail,
            string verifierEmail, DateTime validUntil, string description);
    }
}