using Microsoft.AspNetCore.Mvc;

namespace core_w2.Areas.Users.Models
{
    public class UserResponse
  {
    public List<User> Users { get; set; } = new List<User>();
    public int Total { get; set; }
    public int Skip { get; set; }
    public int Limit { get; set; }
  }
}
