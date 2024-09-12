namespace Domain.Users;

public record Email
{
    private const int DefaultLength = 4;

    private Email(string value) => Value = value;

    public string Value { get; init; }

    public static Email? Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return null;
        }

        if (value.Length <= DefaultLength)
        {
            return null;
        }

        if (!value.Contains("@"))
        {
            return null;
        }

        return new Email(value);
    }
}