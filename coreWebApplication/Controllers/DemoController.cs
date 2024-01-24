using coreWebApplication.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;
using System;
using System.Diagnostics;

namespace coreWebApplication.Controllers
{
    public class DemoController : Controller
    {
        public static List<Customer> customers = new List<Customer>()
        {
            new Customer() { Id = 101, Name = "Nah", Amount = 12000},
            new Customer() { Id = 102, Name = "King", Amount = 12000},
        };
        public IActionResult Index()
        {
            ViewBag.Message = "Customer Management System";
            ViewBag.CustomerCount = customers.Count();
            ViewBag.CustomerList = customers;

            return View();
        }

        // Details action method,we will be using ViewData to transfer data to view
        public IActionResult Details()
        {
            ViewData["Message"] = "Customer Management System";
            ViewData["CustomerCount"] = customers.Count();
            ViewData["CustomerList"] = customers;

            return View();
        }
        public IActionResult Method1()
        {
            TempData["Message"] = "Customer Management System";
            TempData["CustomerCount"] = customers.Count();
            TempData["CustomerList"] = customers;

            return View();
        }
      
        public IActionResult Method2()
        {
            if (TempData["Message"] == null)
            // Test with a break here!
            {
                // Return to the above Index() Action Method
                return RedirectToAction("Index");

                // Store the data to ViewBag or TempData
                TempData["Message"] = TempData["Message"].ToString();

            }
            // Return Method2
            return View();
        }

        public IActionResult Login()
        {
            HttpContext.Session.SetString("username", "JNah");

           // travel to another action method or another view. Using redirect to another action method of the same method itself, display below.
            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            ViewBag.Username = HttpContext.Session.GetString("username");
            return View();
        }

        public  IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }

        // Working with QueryString
        // Request URL:http://localhost:5081/demo/QueryTest
        // Request URL:http://localhost:5081/demo/QueryTest?name=Shallu%20Nah
        public IActionResult QueryTest()
        {
            string name = "Jefferson Nah Jr";
            // checking that if string dot is null or empty, there is an Httpcontext.Request.Query.
            // Any query string can be passed with the variable name 
            // <!--Reassigned query string to the name property, QueryTest?name=Shallu Nah-->
            if (!String.IsNullOrEmpty(HttpContext.Request.Query["name"]))
                name = HttpContext.Request.Query["name"];
            ViewBag.Name = name;

            return View();
        }


    }
}
