
namespace core_w1.MiddleWares
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
