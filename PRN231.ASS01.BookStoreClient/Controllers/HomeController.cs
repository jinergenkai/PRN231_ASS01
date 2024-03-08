using Microsoft.AspNetCore.Mvc;
using PRN231.ASS01.BookStoreClient.Client;
using PRN231.ASS01.Repository.Models;
using PRN231.ASS01.BookStoreClient.Models;
using System.Diagnostics;

namespace PRN231.ASS01.BookStoreClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserClient _userClient;

        public HomeController(ILogger<HomeController> logger, UserClient userClient)
        {
            _logger = logger;
            _userClient = userClient;
        }

        public IActionResult Index()
        {
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
        public async Task<IActionResult> Login(string email, string password)
        {
            User user = await _userClient.GetByEmailAndPassword(email, password);
            if (user != null)
            {
                return RedirectToAction("Index", "Users");
            }
            else
            {
                ViewData["LoginError"] = "Wrong email or password";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}