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
        // KhoaTr - 22/09/2025: Bỏ tham số users khỏi GetAllUsers vì IUserService định nghĩa không có tham số
        public List<User> GetAllUsers()
        {
            return _users;
        }
        // KhoaTr - 22/09/2025: Thêm phương thức hỗ trợ phân trang
        private List<User> Paginate(int skip, int limit)
        {
          Console.WriteLine($"skip: {skip}, To: {limit}");
          return _users.Skip(skip).Take(limit).ToList();
        }
        // KhoaTr - 22/09/2025: Thêm phương thức overload GetAllUsers với tham số phân trang
        public UserResponse GetAllUsers(int skip, int limit)
        {
          var totalUsers = _users.Count;
          var totalPages = (int)Math.Ceiling((double)totalUsers / limit);
          var response = new UserResponse
          {
            Users = Paginate(skip, limit),
            Skip = skip,
            Total = totalPages,
            Limit = limit,
          };
      Console.WriteLine(_users.Count);
          return response;
        }

        public void setUsers(List<User> users)
            {
                throw new NotImplementedException();
            }
        }
}
