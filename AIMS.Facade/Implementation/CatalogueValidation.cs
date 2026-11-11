using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    internal static class CatalogueValidation
    {
        internal static string RequiredText(string? value, int maxLength, string parameterName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, parameterName);
            var trimmed = value.Trim();
            if (trimmed.Length > maxLength)
                throw new ArgumentException($"The value must not exceed {maxLength} characters.", parameterName);
            return trimmed;
        }

        internal static string? OptionalText(string? value, int maxLength, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            return RequiredText(value, maxLength, parameterName);
        }

        internal static string? OptionalEmail(string? value, string parameterName)
        {
            var email = OptionalText(value, 254, parameterName);
            if (email is not null && (email.Any(char.IsWhiteSpace)
                || !System.Net.Mail.MailAddress.TryCreate(email, out var address)
                || !string.Equals(address.Address, email, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("Supply a single valid email address without a display name.", parameterName);
            return email;
        }

        internal static void NonNegativeDecimal(decimal value, int scale, string parameterName)
        {
            var maximum = scale switch
            {
                2 => 9999999999999999.99m,
                3 => 999999999999999.999m,
                6 => 999999999999.999999m,
                _ => throw new ArgumentOutOfRangeException(nameof(scale))
            };
            if (value < 0m || value > maximum || decimal.Round(value, scale) != value)
                throw new ArgumentOutOfRangeException(parameterName, $"The value must be nonnegative and fit decimal(18,{scale}) without rounding.");
        }

        internal static void NewId(long value, string parameterName)
        {
            if (value != 0)
                throw new ArgumentException("Do not supply a database-generated identifier when adding a record.", parameterName);
        }

        internal static void RowVersion(byte[] rowVersion)
        {
            ArgumentNullException.ThrowIfNull(rowVersion);
            if (rowVersion.Length != 8)
                throw new ArgumentException("Supply the original eight-byte RowVersion returned when the record was read.", nameof(rowVersion));
        }
    }
}
