using FoodVeda.Dto;
using FoodVeda.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodVeda.Controllers
{
    public class VendorController:Controller
    {
        public readonly ApplicationDbContext applicationDb;
        public VendorController(ApplicationDbContext applicationDbContext)
        {
            applicationDb = applicationDbContext;
        }
        [ViewLayout("_null")]
        [HttpGet]
        public IActionResult Index(bool status = false)
        {
            if (status == true)
            {
                ViewBag.Message = "Failed to login";
            }
            return View();
        }
        [ViewLayout("_VendorLayout")]
        [HttpPost]
        public IActionResult Index(AdminSignInDto adminSignIn)
        {
            var adminSignDto = applicationDb.Users.Where(x => x.UserName == adminSignIn.UserName && x.Password == adminSignIn.Password && x.UserTypeId==(int)UserTypeEnum.Vendor).FirstOrDefault();
            if (adminSignDto != null)
            {
                HttpContext.Session.SetString("UserId", adminSignDto.ID.ToString());

                return Redirect("Vendor/ViewOrder");
            }
            else
            {
                return Redirect(nameof(VendorController.Index));
            }
        }
        [ViewLayout("_VendorLayout")]
        [HttpGet]
        public IActionResult Dashboard()
        {
            var userId = Int32.Parse(HttpContext.Session.GetString("UserId"));
            if (userId == null)
            {
                return Redirect("Vendor/Index");
            }
            return View();
        }
        [ViewLayout("_VendorLayout")]
        [HttpGet]
        public IActionResult AddProduct()
        {
            var userId = Int32.Parse(HttpContext.Session.GetString("UserId"));
            if (userId == null)
            {
                return Redirect("Vendor/AddProduct");
            }
            return View();
        }
        [ViewLayout("_VendorLayout")]
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            product.UserId = Int32.Parse(HttpContext.Session.GetString("UserId"));
            product.AverageRating = 4;
            applicationDb.Products.Add(product);
            applicationDb.SaveChanges();
            return View();
        }
        [ViewLayout("_VendorLayout")]
        [HttpGet]
        public IActionResult ViewOrder()
        {
            var userId = Int32.Parse(HttpContext.Session.GetString("UserId"));
            if (userId == null)
            {
                return Redirect("Vendor/Index");
            }
            var UserId= Int32.Parse(HttpContext.Session.GetString("UserId"));
            var orders = applicationDb.Orders.Where(x => x.Product.UserId == UserId).ToList();
            return View(orders);
        }
        [HttpGet]
        public void ChangeStatusOrder(int OrderId)
        {
            var data = applicationDb.Orders.Where(x => x.Id == OrderId).FirstOrDefault();
            data.StatusId = 3;
            applicationDb.Orders.Update(data);
            applicationDb.SaveChanges();
        }
        [ViewLayout("_VendorLayout")]
        public IActionResult ViewProduct()
        {
            var userId = Int32.Parse(HttpContext.Session.GetString("UserId"));
            if (userId == null)
            {
                return Redirect("Vendor/Index");
            }
            var UserId = Int32.Parse(HttpContext.Session.GetString("UserId"));
            var products = applicationDb.Products.Where(x => x.UserId == UserId).ToList();
            return View(products);
        }
    }
}
