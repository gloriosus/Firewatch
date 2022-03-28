using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Firewatch.Data;

namespace Firewatch.Controllers
{
    public class StationsController : Controller
    {
        [Route("Data/WeatherStations")]
        public IActionResult Index()
        {
            string username = Request.Cookies["Username"];

            if (!string.IsNullOrWhiteSpace(username))
            {
                int userId = UserDAO.Instance.Read().First(user => user.Username == username).Id;

                ViewBag.UserId = userId;

                return View();
            }
            else
            {
                return Redirect("/Forbidden");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(string title, string latitude, string longitude)
        {
            string coordinates = $"[{latitude}, {longitude}]";

            string username = Request.Cookies["Username"];
            int userId = UserDAO.Instance.Read().First(user => user.Username == username).Id;

            var station = new WeatherStation(-1, title, coordinates, "", userId);

            WeatherStationDAO.Instance.Create(station);

            return Redirect("/Data/WeatherStations");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(string id, string title, string latitude, string longitude)
        {
            string coordinates = $"[{latitude}, {longitude}]";

            string username = Request.Cookies["Username"];
            int userId = UserDAO.Instance.Read().First(user => user.Username == username).Id;

            var replaceable = WeatherStationDAO.Instance.Read().First(st => st.Id == Convert.ToInt32(id));
            var replacer = new WeatherStation(-1, title, coordinates, "", userId);

            WeatherStationDAO.Instance.Update(replaceable, replacer);

            return Redirect("/Data/WeatherStations");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            var removable = WeatherStationDAO.Instance.Read().First(st => st.Id == Convert.ToInt32(id));

            WeatherStationDAO.Instance.Delete(removable);

            return Redirect("/Data/WeatherStations");
        }
    }
}