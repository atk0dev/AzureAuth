namespace Domain.Users;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    
    Task<User?> GetByIdAsync(UserId id);

    void Add(User user);

    void Update(User user);

    void Remove(User user);
}