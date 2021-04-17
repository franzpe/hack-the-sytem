namespace API.Core.Platform.Enums
{
    public enum ContractStatus
    {
        Created,
        NotAccepted,
        Accepted,
        WaitingForVerification,
        VerifierRejected,
        VerifierAccepted,
        Finished,
        Canceled
    }
}