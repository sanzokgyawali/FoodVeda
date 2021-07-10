using FoodVeda.Dto;
using FoodVeda.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodVeda.Controllers
{
    [ViewLayout("_AdminLayout")]
    public class AdminController:Controller
    {
        public readonly ApplicationDbContext applicationDb;
        public AdminController(ApplicationDbContext applicationDbContext)
        {
            applicationDb = applicationDbContext;
        }
        [HttpGet]
        public IActionResult Index(bool status=false)
        {
            if(status==true)
            {
                ViewBag.Message = "Failed to login";
            }
            return View();
        }
        [HttpPost]
        public IActionResult Index(AdminSignInDto adminSignIn)
        {
            int adminSignDto = applicationDb.Users.Where(x => x.UserName == adminSignIn.UserName && x.Password == adminSignIn.Password).Count();
            if(adminSignDto>=1)
            {
                return View(nameof(Dashboard),true);
            }
            else
            {
                return View(nameof(Index));
            }
        }
        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddVendor(bool status)
        {
            if(status==true)
            {
                ViewBag.Message = "Successfully Added Vendor";
            }
            ViewBag.PrimaryLocation = applicationDb.PrimaryLocations.ToList();
            return View();
            
        }
        [HttpPost]
        public IActionResult AddVendor(User user)
        {
            user.UserTypeId = (int)UserTypeEnum.Vendor;
            applicationDb.Users.Add(user);
            applicationDb.SaveChanges();
            return View(nameof(AddVendor),true);
        }
        [HttpGet]
        public IActionResult ViewVendor()
        {
            var vendorList = applicationDb.Users.Where(x => x.UserTypeId == (int)UserTypeEnum.Vendor).ToList();
            return View(vendorList);
        }
        [HttpGet]
        public IActionResult AddNutrionist(bool status=false)
        {
            if (status == true)
            {
                ViewBag.Message = "Successfully Added Vendor";
            }
            ViewBag.PrimaryLocation = applicationDb.PrimaryLocations.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult AddNutrionist(User user)
        {
            user.UserTypeId = (int)UserTypeEnum.Nutrionist;
            applicationDb.Users.Add(user);
            applicationDb.SaveChanges();
            return View(nameof(AddNutrionist), true);
        }
        [HttpGet]
        public IActionResult NutrionistMessage()
        {
            var message = applicationDb.MessageToNutrionists.OrderByDescending(x => x.Id).ToList();
            return View(message);
        }
        [HttpGet]
        public IActionResult ViewOrder()
        {
            var orders = applicationDb.Orders.OrderByDescending(x => x.Id).ToList();
            return View(orders);
        }

    }
}
