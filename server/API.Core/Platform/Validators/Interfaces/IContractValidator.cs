namespace API.Core.Platform.Validators
{
    public interface IContractValidator
    {
        bool IsValidEmail(string email);
    }
}