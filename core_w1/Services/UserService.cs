using core_w1.Models;

namespace core_w1.Services
{
    public class UserService : IUserService //implement the interface for user service
    {
        private readonly List<User> _users= new List<User>(); // remember assigning an intial value to avoid null expception
        public UserService()
        {
            if (File.Exists("listUser.txt"))
            {
                var lines = File.ReadAllLines("listUser.txt");
                foreach (var line in lines)
                {
                    var parts = line.Split(','); // format username,password,roleId
                    if (parts.Length == 3)
                    {
                        _users.Add(new User
                        { // take the index of each part username,password,roleId
                            UserName = parts[0],
                            Password = parts[1],
                            RoleId = int.Parse(parts[2])
                        });
                    }
                }
            }
        }
        public List<User> GetAllUsers(List<User>users)
        {
            return _users;
        }

        public void setUsers(List<User> users)
        {
            throw new NotImplementedException();
        }
    }
}
