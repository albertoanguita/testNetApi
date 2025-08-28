using System.Text.Json;

namespace TestWebAPI1.users;


public class UserManager
{
    private static UserManager? _instance;
    
    private readonly UserStore _userStore;

    private UserManager()
    {
        // parse users
        // todo handle exceptions
        _userStore = JsonSerializer.Deserialize<UserStore>(File.ReadAllText("users.json"))!;
    }

    public static UserManager GetInstance()
    {
        _instance ??= new UserManager();

        return _instance;
    }

    public List<User> GetUsers()
    {
        return _userStore.Users.ToList();
    }

    public List<Admin> GetAdmins()
    {
        return _userStore.Admins.ToList();
    }
}