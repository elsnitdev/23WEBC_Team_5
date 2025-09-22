namespace core_w1.Models
{
  public class UserResponse
  {
    public List<User> Users { get; set; } = new List<User>();
    public int Total { get; set; }
    public int Skip { get; set; }
    public int Limit { get; set; }
  }
}
