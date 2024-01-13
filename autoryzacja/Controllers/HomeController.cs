using autoryzacja.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using autoryzacja.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace autoryzacja.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        //return View("~/Views/CarReservations/Create.cshtml");

        [Authorize]
        public IActionResult Create()
        {
                return View("~/Views/CarReservations/Create.cshtml");
           
        }



        public IActionResult Images()
        {
            return View();
        }
        [Authorize]
        public IActionResult Offer()
        {
            if (User.Identity.IsAuthenticated && User.Identity.Name == "admin@admin.pl")
            {
                return View();
            }
            else
            {
                return Unauthorized(); // Or redirect to a different view or action
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}