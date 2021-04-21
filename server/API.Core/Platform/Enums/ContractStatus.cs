namespace API.Core.Platform.Enums
{
    public enum ContractStatus
    {
        Created,
        NotAccepted,
        Accepted,
        WaitingForVerification,
        VerifierRejected,
        Verified,
        Finished,
        Canceled
    }
}