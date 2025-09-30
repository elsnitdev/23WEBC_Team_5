using Microsoft.AspNetCore.Mvc;

namespace core_w2.MiddleWares
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
