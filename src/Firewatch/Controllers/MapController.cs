using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Firewatch.Models;
using Microsoft.AspNetCore.Http;
using Firewatch.Data;
using System.Globalization;

namespace Firewatch.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            var username = Request.Cookies["Username"];

            if (username is null)
            {
                ViewBag.Auth = "<div style=\"width: 50%; height: 100%; text-align: center; padding-top: 10px; float: left;\" ><a href=\"/Account/SignIn\">Вход</a></div><div style=\"width: 50%; height: 100%; text-align: center; padding-top: 10px; float: right;\"><a href=\"/Account/SignUp\">Регистрация</a></div>";
            }
            else
            {
                ViewBag.Auth = "<div style=\"width: 100%; height: 100%; text-align: center; padding-top: 10px; float: left;\"><a href=\"/Account/SignOut\">Выход</a></div>";
                ViewBag.OwnData = "<p><input type=\"checkbox\" name=\"isOwnData\" value=\"False\"> Использовать свои данные?</p><a class=\"btn btn-primary\" href=\"/Data\">Своя сборка данных</a>";
                ViewBag.PersonalPage = $"<div style=\"width: 100%; height: 50px; border-bottom: 1px solid #ABB2B9;\"><p>{username}: <a href=\"/Account/Page\">Персональная страница</a></p></div>";
            }

            return View();
        }

        [HttpPost]
        public IActionResult Index(string date, string isOwnData)
        {
            var username = Request.Cookies["Username"];

            if (username is null)
            {
                ViewBag.Auth = "<div style=\"width: 50%; height: 100%; text-align: center; padding-top: 10px; float: left;\" ><a href=\"/Account/SignIn\">Вход</a></div><div style=\"width: 50%; height: 100%; text-align: center; padding-top: 10px; float: right;\"><a href=\"/Account/SignUp\">Регистрация</a></div>";
            }
            else
            {
                ViewBag.Auth = "<div style=\"width: 100%; height: 100%; text-align: center; padding-top: 10px; float: left;\"><a href=\"/Account/SignOut\">Выход</a></div>";
                ViewBag.OwnData = "<p><input type=\"checkbox\" name=\"isOwnData\"> Использовать свои данные?</p><a class=\"btn btn-primary\" href=\"/Data\">Своя сборка данных</a>";
                ViewBag.PersonalPage = $"<div style=\"width: 100%; height: 50px; border-bottom: 1px solid #ABB2B9;\"><p>{username}: <a href=\"/Account/Page\">Персональная страница</a></p></div>";
            }

            DateTime selectedDate = DateTime.Parse(date);

            string month = selectedDate.ToString("MMMM", CultureInfo.InvariantCulture);

            Map.FillComplexIndicatorTable(month, selectedDate);

            string result = string.Empty;

            foreach (var polygon in PolygonDAO.Instance.Read())
            {
                result += Map.Generate(polygon);
            }

            ViewBag.Date = date;
            ViewBag.Result = result;

            return View();
        }
    }
}
