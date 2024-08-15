namespace GameDashboard.Application.Constants
{
    public static class ValidationMessages
    {

        public static readonly string EmailAddressInvalid = "Invalid email address.";
        public static readonly string PasswordsNotMatched = "Passwords do not matched.";
        public static readonly string PasswordMinimumLengthEight = "Password must be at least 8 characters long.";
        public static readonly string PasswordRegexError = "Password must contain uppercase letters, lowercase letters, and punctuation marks.";

        public static readonly string UsernameMinLengthError = "Username must be greater than 4 characters.";

        public static readonly string ConstructionTimeNotRange = "Building construction time must be between 30 and 1800 seconds.";
        public static readonly string BuildingCostNotLessThenZero = "Building cost cannot be less than 0.";

    }
}
