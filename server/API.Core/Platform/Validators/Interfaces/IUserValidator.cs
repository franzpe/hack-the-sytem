namespace API.Core.Platform.Validators
{
    public interface IUserValidator
    {
        bool IsValidEmail(string email);
    }
}