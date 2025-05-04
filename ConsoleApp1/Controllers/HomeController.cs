using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAppDemo.Models;

namespace WebAppDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FirebaseSettings _firebaseSettings;
        public HomeController(ILogger<HomeController> logger, IOptions<FirebaseSettings> firebaseSettings)
        {
            _logger = logger;
            _firebaseSettings = firebaseSettings.Value;
        }

        public IActionResult Index()
        {
            ViewBag.FirebaseConfig = _firebaseSettings;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult SaveToken([FromBody] TokenModel model)
        {
            // Save the token in your DB (or just log it)
            System.IO.File.WriteAllText("fcm_token.txt", model.Token);
            return Ok(new { message = "Token saved" });
        }
    }
    public class TokenModel
    {
        public string Token { get; set; }
    }
}
