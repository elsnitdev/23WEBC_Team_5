using Microsoft.AspNetCore.Mvc;

namespace core_w2.Areas.Users.Models
{
    public class User
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public int RoleId { get; set; }

    }
}
