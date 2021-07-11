using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FoodVeda.Models;
using Microsoft.EntityFrameworkCore;
using FoodVeda.Dto;

namespace FoodVeda.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext applicationDb;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            applicationDb = applicationDbContext;
        }
        //[ViewLayout("_MyLayout")]
        public IActionResult Index()
        {
            var topProducts =  applicationDb.Products.OrderByDescending(x => x.AverageRating).Take(8).ToList();
            var latestProducts = applicationDb.Products.OrderByDescending(x => x.Id).Take(4).ToList();
            var homeDto = new HomeIndexDto()
            {
                TopProducts = topProducts,
                LatestProducts = latestProducts
            };
            return View(homeDto);
        }
        [HttpGet]
        public IActionResult NutrionistMessage(bool status=false)
        {
            if(status==true)
            {
                ViewBag.Message = "Succcessfully sent the message";
            }
            else
            {
                ViewBag.Message = "";
            }
            ViewBag.Neutrionist = applicationDb.Users.Where(x => x.UserTypeId ==(int)UserTypeEnum.Nutrionist).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult NutrionistMessage(MessageToNutrionist messageN)
        {
            messageN.Status = "pending";
            applicationDb.MessageToNutrionists.Add(messageN);
            applicationDb.SaveChanges();
            ViewBag.Message = "Succcessfully sent the message! You will receive the answer from Nutrionist in mail";
            ViewBag.Neutrionist = applicationDb.Users.Where(x => x.UserTypeId == (int)UserTypeEnum.Nutrionist).ToList();
            return View(nameof(NutrionistMessage));
        }
        [HttpGet]
        public IActionResult FullDetails(int productId)
        {
            var productDetails = applicationDb.Products.Where(x => x.Id == productId).FirstOrDefault();
            return View(productDetails);
        }
        [HttpGet]
        public IActionResult BuyNow(int productId, string OrderIdForTrackingStatus=null)
        {
            ViewBag.OrderForTrackingStatus = "1";
            ViewBag.Location = applicationDb.PrimaryLocations.ToList();
            ViewBag.productDetails = applicationDb.Products.Where(x => x.Id == productId).FirstOrDefault();
            return View();
        }
        [HttpPost]
        public IActionResult BuyNow(Order order)
        {
            Random generator = new Random();
            order.OrderIdForTrackingStatus = "F" + generator.Next(0, 10000).ToString("D6");
            order.StatusId = 1;
            ViewBag.Location = applicationDb.PrimaryLocations.ToList();
            ViewBag.OrderForTrackingStatus = order.OrderIdForTrackingStatus;
            ViewBag.productDetails = applicationDb.Products.Where(x => x.Id == order.ProductId).FirstOrDefault();
            applicationDb.Orders.Add(order);
            applicationDb.SaveChanges();
            return View(nameof(BuyNow));

        }
        [HttpGet]
        public IActionResult TrackOrder()
        {
            return View();
        }
        [HttpGet]
        public string OrderTrackingStatus(string OrderId)
        {
            return applicationDb.Orders.Where(x => x.OrderIdForTrackingStatus == OrderId).Select(x=>x.Status.Name).FirstOrDefault();
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
    }
}
