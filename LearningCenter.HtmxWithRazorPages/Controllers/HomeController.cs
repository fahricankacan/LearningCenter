using LearningCenter.HtmxWithRazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LearningCenter.HtmxWithRazorPages.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static int mouse_enteredCounter = 0;
        private static string infiniteTd = string.Empty;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
        [HttpPost("clicked")]
        public IActionResult Clicked()
        {
            string helloWorld = "<h1>Hello World!<h1/>";


            return Ok(helloWorld);
        }

        [HttpPost("mouse_entered")]
        public IActionResult mouse_entered()
        {
            mouse_enteredCounter++;

            string mouseEnteredTriggered = "Trigerred - " + mouse_enteredCounter.ToString();
            return Ok(mouseEnteredTriggered);
        }

        [HttpGet("trigger_delay")]
        public IActionResult trigger_delay(trigger_delay_req trigger_Delay_Req)
        {
            return Ok(trigger_Delay_Req.q ?? "sonuç bulunamadı!");
        }

        [HttpGet("delayed_task")]
        public IActionResult delayed_task()
        {
            Thread.Sleep(5000);
            return Ok("complated");
        }

        [HttpGet("RandomNumber")]
        public IActionResult RandomNumber()
        {
            Random random = new Random();
            int number = random.Next(0, 100);
            return Ok($"New random number is {number}");
        }

        [HttpGet("randomlist")]
        public IActionResult RandomList()
        {
            string row = "<td>Fahrican</td>" +
                "<td>Fahrican@gmail.com</td>" +
                "<td>" + Guid.NewGuid().ToString() + "</td>";


            for (int i = 0; i < 2; i++)
            {
                infiniteTd += row;
            }

            return Ok(infiniteTd);
        }

        public class trigger_delay_req
        {
            public string q { get; set; }
        }




    }
}