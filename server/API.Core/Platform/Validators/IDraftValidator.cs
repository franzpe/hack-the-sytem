namespace API.Core.Platform.Validators
{
    public interface IDraftValidator
    {
        bool IsValidEmail(string email);
    }
}