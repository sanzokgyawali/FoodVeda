using FoodVeda.Dto;
using FoodVeda.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace FoodVeda.Controllers
{
    public class NeutrionistController:Controller
    {
        public readonly ApplicationDbContext applicationDb;
        public NeutrionistController(ApplicationDbContext applicationDbContext)
        {
            applicationDb = applicationDbContext;
        }
        [HttpGet]
        [ViewLayout("_null")]
        public IActionResult Index(bool status = false)
        {
            if (status == true)
            {
                ViewBag.Message = "Failed to login";
            }
            return View();
        }
        [HttpPost]
        [ViewLayout("_null")]
        public IActionResult Index(AdminSignInDto adminSignIn)
        {
            var adminSignDto = applicationDb.Users.Where(x => x.UserName == adminSignIn.UserName && x.Password == adminSignIn.Password && x.UserTypeId==(int)UserTypeEnum.Nutrionist).FirstOrDefault();
            if (adminSignDto != null)
            {
                HttpContext.Session.SetString("UserId", adminSignDto.ID.ToString());
                var userId = Int32.Parse(HttpContext.Session.GetString("UserId"));
                var Message = applicationDb.MessageToNutrionists.Where(x => x.UserId == userId).ToList();

                return View(nameof(Message));
            }
            else
            {
                return View(nameof(Index));
            }
        }
        
        [HttpGet]
        [ViewLayout("_null")]
        public IActionResult Message()
        {
            var userId = Int32.Parse(HttpContext.Session.GetString("UserId"));
            if(userId==null)
            {
                return View(nameof(Index));
            }
            var Message= applicationDb.MessageToNutrionists.Where(x => x.UserId == userId).ToList();
            return View(Message);
        }
        [HttpGet]
        public bool Message(string Message, string Email)
        {
            string to = Email; //To address    
            string from = "sanzokgyawali123@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = "In this article you will learn how to send a email using Asp.Net & C#";
            message.Subject = "Sending Email Using Asp.Net & C#";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("sanzokgyawali123@gmail.com", "KopilaRam12@");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            var data = applicationDb.MessageToNutrionists.Where(x => x.Email == Email).FirstOrDefault();
            data.Status = "delivered";
            applicationDb.MessageToNutrionists.Update(data);
            applicationDb.SaveChanges();
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                return true;
            }
            return true;
        }
    }
}
