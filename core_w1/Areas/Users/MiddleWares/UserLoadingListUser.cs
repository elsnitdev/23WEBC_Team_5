using Microsoft.AspNetCore.Mvc;

namespace core_w2.Areas.Users.MiddleWares
{
    public class UserLoadingListUser
    {
        private readonly RequestDelegate _next;
        public UserLoadingListUser(RequestDelegate next)
        {
            _next = next;
        }

    }
}
