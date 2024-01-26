namespace Project.Domain.UserAggregate;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private User(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string password,
        DateTime createdAt)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        CreatedAt = createdAt;
    }

    public static User CreateUser(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string password,
        DateTime createdAt)
    {
        // buss logic
        return new User(
            id,
            firstName,
            lastName,
            email,
            password,
            createdAt);
    }

    public void SetUserFirstName(string firstName)
    {
        this.FirstName = firstName;
    }

    public void SetUserLastName(string lastName)
    {
        this.LastName = lastName;
    }
}