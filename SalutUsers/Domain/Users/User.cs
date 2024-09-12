namespace Domain.Users;

public class User
{
    public User(UserId id, Name name, Email email, Youtube youtubeHandle, Linkedin linkedinHandle)
    {
        Id = id;
        Name = name;
        Email = email;
        Youtube = youtubeHandle;
        Linkedin = linkedinHandle;
    }

    private User()
    {
    }

    public UserId Id { get; private set; }

    public Name Name { get; private set; }

    public Email Email { get; private set; }

    public Youtube Youtube { get; set; }
    
    public Linkedin Linkedin { get; set; }

    public void Update(Name name, Email email, Youtube youtubeHandle, Linkedin linkedinHandle)
    {
        Name = name;
        Email = email;
        Youtube = youtubeHandle;
        Linkedin = linkedinHandle;
    }
}
