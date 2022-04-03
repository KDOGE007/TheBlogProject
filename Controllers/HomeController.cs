using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TheBlogProject.Data;
using TheBlogProject.Models;
using TheBlogProject.Services;
using TheBlogProject.ViewModels;
using X.PagedList;

namespace TheBlogProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IBlogEmailSender emailSender, ApplicationDbContext context)
        {
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 6;

            var blogs = _context.Blogs
                .Include(b => b.Author)
                //.Where(b => b.Posts.Any(p => p.ReadyStatus == Enums.ReadyStatus.ProductionReady))
                .OrderByDescending(b => b.Created)
                .ToPagedListAsync(pageNumber, pageSize);

            ViewData["MainText"] = "The Blog Project";
            ViewData["SubText"] = "Building a custom website using.Net 5 MVC and Bootstrap 5";
            //Todo: Fix the path
            ViewData["HeaderImage"] = base.File("/img/home-bg", "image/jpg");

            return View(await blogs);
        }
                
        public IActionResult About()
        {
            ViewData["MainText"] = "The Blog Project";
            ViewData["SubText"] = "Building a custom website using.Net 5 MVC and Bootstrap 5";
            return View();
        }
        public IActionResult Contact()
        {
            ViewData["MainText"] = "The Blog Project";
            ViewData["SubText"] = "Building a custom website using.Net 5 MVC and Bootstrap 5";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactMe model)
        {
            //this is where we will be emailing...
            model.Message = $"{model.Message} <hr> Phone: {model.Phone}";
            await _emailSender.SendContactEmailAsync(model.Email, model.Name, model.Subject, model.Message);
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
