namespace Domain.Users;

public sealed class UserNotFoundException : Exception
{
    public UserNotFoundException(UserId id)
        : base($"The user with the ID = {id.Value} was not found")
    {
    }
}
