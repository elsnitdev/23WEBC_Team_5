using Microsoft.AspNetCore.Mvc;

namespace core_w2.MiddleWares
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
