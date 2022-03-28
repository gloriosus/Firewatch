using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Firewatch.Data;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace Firewatch.Controllers
{
    public class DataController : Controller
    {
        public IActionResult Index()
        {
            string username = Request.Cookies["Username"];

            if (!string.IsNullOrWhiteSpace(username))
            {
                return View();
            }
            else
            {
                return Redirect("/Forbidden");
            }
        }
    }
}