using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Firewatch.Data;
using System.Globalization;

namespace Firewatch.Controllers
{
    public class MonthsController : Controller
    {
        [Route("Data/Months")]
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

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(string month, string station, string temperature, string dew, string precipitation, string date)
        {
            int weatherStationId = WeatherStationDAO.Instance.GetId(station);
            float temperatureValue = float.Parse(temperature, CultureInfo.InvariantCulture);
            float dewValue = float.Parse(dew, CultureInfo.InvariantCulture);
            float precipitationValue = float.Parse(precipitation, CultureInfo.InvariantCulture);
            DateTime dateValue = DateTime.Parse(date);

            string username = Request.Cookies["Username"];

            int userId = UserDAO.Instance.Read().First(user => user.Username == username).Id;

            Day day = new Day(-1, weatherStationId, temperatureValue, dewValue, precipitationValue, dateValue, userId);

            MonthDAO.Instance.Create(day, month);

            return Redirect($"/Data/Months/{month}");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(string id, string month, string station, string temperature, string dew, string precipitation, string date)
        {
            int weatherStationId = WeatherStationDAO.Instance.GetId(station);
            float temperatureValue = float.Parse(temperature, CultureInfo.InvariantCulture);
            float dewValue = float.Parse(dew, CultureInfo.InvariantCulture);
            float precipitationValue = float.Parse(precipitation, CultureInfo.InvariantCulture);
            DateTime dateValue = DateTime.Parse(date);

            string username = Request.Cookies["Username"];

            int userId = UserDAO.Instance.Read().First(user => user.Username == username).Id;

            Day replaceable = MonthDAO.Instance.Read(month).First(day => day.Id == Convert.ToInt32(id));
            Day replacer = new Day(-1, weatherStationId, temperatureValue, dewValue, precipitationValue, dateValue, userId);

            MonthDAO.Instance.Update(replaceable, replacer, month);

            return Redirect($"/Data/Months/{month}");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(string id, string month)
        {
            Day removable = MonthDAO.Instance.Read(month).First(day => day.Id == Convert.ToInt32(id));

            MonthDAO.Instance.Delete(removable, month);

            return Redirect($"/Data/Months/{month}");
        }

        [Route("Data/Months/{name}")]
        public IActionResult Month(string name)
        {
            string username = Request.Cookies["Username"];

            if (!string.IsNullOrWhiteSpace(username))
            {
                ViewBag.Month = name;

                int userId = UserDAO.Instance.Read().First(user => user.Username == username).Id;

                ViewBag.UserId = userId;

                return View("Month");
            }
            else
            {
                return Redirect("/Forbidden");
            }
        }
    }
}