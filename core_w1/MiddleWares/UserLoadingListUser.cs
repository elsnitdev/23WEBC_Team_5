
using core_w1.Models;
using core_w1.Services;

namespace core_w1.MiddleWares
{
    public class UserLoadingListUser
    {
        private readonly RequestDelegate _next;

        public UserLoadingListUser(RequestDelegate next)
        {
            _next = next; // store the next middleware in the pipeline
        }

        public async Task InvokeAsync(HttpContext context, IUserService userService)
        {
            if (!userService.GetAllUsers().Any()) // check the list user and just runs once 
            {
                var users = new List<User>();
                var lines = File.ReadAllLines("users.txt");// read file

                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        users.Add(new User
                        {
                            UserName = parts[0],
                            Password = parts[1],
                            RoleId = int.Parse(parts[2])
                        });
                    }
                }

                userService.setUsers(users);// call setUser method to set the list of users
            }

            await _next(context);
        }
    }

    public static class UserLoadingMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserLoading(this IApplicationBuilder builder) // add the middleware to the pipeline, by using UseUserloading in program.cs
        {
            return builder.UseMiddleware<UserLoadingListUser>();
        }
    }
}
