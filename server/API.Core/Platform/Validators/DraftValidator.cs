using System.Text.RegularExpressions;

namespace API.Core.Platform.Validators
{
    public class DraftValidator : IDraftValidator
    {
        private const string EMAIL_REGEX =
            "[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";

        public bool IsValidEmail(string email)
        {
            var regex = new Regex(EMAIL_REGEX);

            return regex.IsMatch(email);
        }
    }
}