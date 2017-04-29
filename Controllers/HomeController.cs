using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using build17_demo.Models;

namespace build17_demo.Controllers
{
    public class HomeController : Controller
    {
        static HomeController()
        {
            using (DataContext context = new DataContext(Startup.connectionBuilder.ConnectionString))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                string[] texts = new string[]{"Hi", "Hello", "Hola"};
                string[] authors = new string[]{"Travis", "Luis", "Louise"};
                string[] species = new string[]{"whale", "whale", "turtle"};

                for (int i = 0; i < 3; i++)
                {
                    Comment newComment = new Comment { Text = texts[i], Author = authors[i], Species = species[i] };
                    context.Comments.Add(newComment);
                    context.SaveChanges();
                    Console.WriteLine("\nCreated Comment: " + newComment.ToString());
                }

            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        [Route("/api/comments")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Get()
        {
            List<Comment> result;
            using (DataContext context = new DataContext(Startup.connectionBuilder.ConnectionString))
            {
                result = context.Comments.ToList();
            }

            if (result.Count < 1)
                return Json("");

            return Json(result);
        }

        [HttpPost]
        [Route("/api/comments")]
        public IActionResult Post(string author, string text, string species)
        {   
            using (DataContext context = new DataContext(Startup.connectionBuilder.ConnectionString))
            {
                Comment newComment = new Comment { Text = text, Author = author, Species = species };
                context.Comments.Add(newComment);
                context.SaveChanges();
                Console.WriteLine("\nCreated Comment: " + newComment.ToString());
            }
            return Json("");
        }
    }
}
