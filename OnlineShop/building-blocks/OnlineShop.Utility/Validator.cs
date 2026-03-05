namespace OnlineShop.Utility;

public static class Validator
{
    public static bool IsValidEmail(this string email)
    {
        try
        {
            if (string.IsNullOrEmpty(email))
                return false;

            var tr = email.Trim();
            var addr = new System.Net.Mail.MailAddress(tr);
            return addr.Address == tr;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsValid(this Guid value)
    {
        return value == Guid.Empty;
    }

    public static bool IsValid(this Guid? value)
    {
        return value.HasValue && value.Value != Guid.Empty;
    }

    public static bool IsValid(this DateTime value)
    {
        return value != DateTime.MinValue;
    }

    public static bool IsValid(this DateTime? value)
    {
        return value.HasValue && value != DateTime.MinValue;
    }
}
