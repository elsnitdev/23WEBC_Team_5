using core_w1.Models;
namespace core_w1.Services
{
    public interface IUserService // create the interface for user service
    {
        List<User> GetAllUsers();   
        void setUsers(List<User> users);

    }
}
