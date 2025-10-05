// KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website
namespace core_website.MiddleWares
// KhoaTr - END
{

  public interface IRequestLogger
  {
    void Log(string message) { }
  }
  public class RequestLogger : IRequestLogger
  {
    public void Log(string message)
    {
      File.AppendAllText("Request_log.txt", message); // File.AppendAllText is used to append
                                                      // the text to file ,if file does not exist it'll create the file
      Console.WriteLine($"Request Log: {message}");
    }

  }
}
