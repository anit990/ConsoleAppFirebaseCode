using Microsoft.AspNetCore.Mvc;
using WebAppDemo.Services;

namespace WebAppDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        //https://companieslogo.com/img/orig/PNB.NS_BIG-6ba17682.png
        private readonly string _tokenFilePath = Path.Combine(Directory.GetCurrentDirectory(), "fcm_token.txt");
        private readonly FcmService _fcmService;

        public NotificationController(FcmService fcmService)
        {
            _fcmService = fcmService;
        }
        [HttpPost("send")]
        public async Task<IActionResult> Send()
        {
            string deviceToken = GetFcmToken(); 
            await _fcmService.SendMessageAsync(deviceToken);
            return Content("Notification sent.");
        }
        public string GetFcmToken()
        {
            if (!System.IO.File.Exists(_tokenFilePath))
            {
                throw new FileNotFoundException("FCM token file not found.");
            }
            return System.IO.File.ReadAllText(_tokenFilePath).Trim();
        }
    }
}
