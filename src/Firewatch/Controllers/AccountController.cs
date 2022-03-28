using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Firewatch.Data;
using Scrypt;

namespace Firewatch.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult SignIn(string username, string password)
        {
            var cookieOptions = new CookieOptions()
            {
                Path = "/",
                HttpOnly = false,
                IsEssential = true,
                Expires = DateTime.Now.AddDays(7)
            };

            ScryptEncoder scryptEncoder = new ScryptEncoder();

            var user = UserDAO.Instance.Read().FirstOrDefault(usr => usr.Username == username);

            if (user is null)
            {
                ViewBag.ErrorMessage = "<div class=\"alert alert-danger\" role=\"alert\">Пользователя с таким именем не существует</div>";
                return View();
            }
            else
            {
                if (!scryptEncoder.Compare(password, user.Password))
                {
                    ViewBag.ErrorMessage = "<div class=\"alert alert-danger\" role=\"alert\">Неверный пароль</div>";
                    return View();
                }
                else
                {
                    Response.Cookies.Append("Username", username, cookieOptions);
                    return Redirect("/");
                }
            }
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult SignUp(string username, string password)
        {
            ScryptEncoder scryptEncoder = new ScryptEncoder();

            var user = new User(-1, username, scryptEncoder.Encode(password), 2);

            UserDAO.Instance.Create(user);

            ViewBag.SuccessMessage = "<div class=\"alert alert-success\" role=\"alert\">Учетная запись была успешно создана. Используйте свой логин и пароль для входа на сайт</div>";

            return Redirect("/Account/SignIn");
        }

        public IActionResult SignOut()
        {
            Response.Cookies.Delete("Username");

            return Redirect("/");
        }

        public IActionResult Page()
        {
            string username = Request.Cookies["Username"];

            if (!string.IsNullOrWhiteSpace(username))
            {
                ViewBag.Username = username;
                return View();
            }
            else
            {
                return Redirect("/Forbidden");
            }
        }

        public IActionResult ChangeUsername()
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
        public IActionResult ChangeUsername(string username, string password)
        {
            User user = UserDAO.Instance.Read().First(x => x.Username.Equals(Request.Cookies["Username"]));

            ScryptEncoder scryptEncoder = new ScryptEncoder();

            if (scryptEncoder.Compare(password, user.Password))
            {
                User replacer = new User(-1, username, user.Password, user.UserTypeId);

                UserDAO.Instance.Update(user, replacer);

                var cookieOptions = new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = false,
                    IsEssential = true,
                    Expires = DateTime.Now.AddDays(7)
                };

                Response.Cookies.Delete("Username");
                Response.Cookies.Append("Username", username, cookieOptions);

                return Redirect("/Account/Page");
            }
            else
            {
                ViewBag.ErrorMessage = "<div class=\"alert alert-danger\" role=\"alert\">Неверный пароль</div>";
                return View();
            }
        }

        public IActionResult ChangePassword()
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
        public IActionResult ChangePassword(string currentPassword, string freshPassword, string submitPassword)
        {
            User user = UserDAO.Instance.Read().First(x => x.Username.Equals(Request.Cookies["Username"]));

            ScryptEncoder scryptEncoder = new ScryptEncoder();

            if (scryptEncoder.Compare(currentPassword, user.Password))
            {
                if (freshPassword.Equals(submitPassword))
                {
                    User entity = new User(-1, user.Username, scryptEncoder.Encode(freshPassword), user.UserTypeId);

                    UserDAO.Instance.Update(user, entity);

                    return Redirect("/Account/Page");
                }
                else
                {
                    ViewBag.ErrorMessage = "<div class=\"alert alert-danger\" role=\"alert\">Новый пароль не совпадает с паролем подтверждения</div>";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMessage = "<div class=\"alert alert-danger\" role=\"alert\">Неверно введен текущий пароль</div>";
                return View();
            }
        }

        public IActionResult Restore()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Restore(string username, string email)
        {
            if (!string.IsNullOrWhiteSpace(username) & !string.IsNullOrWhiteSpace(email))
            {
                User user = UserDAO.Instance.Read().First(x => x.Username.Equals(username));

                if (user != null)
                {
                    string password = GenerateTemporaryPassword();

                    ScryptEncoder scryptEncoder = new ScryptEncoder();

                    User replacer = new User(-1, user.Username, scryptEncoder.Encode(password), user.UserTypeId);

                    UserDAO.Instance.Update(user, replacer);

                    // TODO: Generate and send email message

                    ViewBag.Password = password;
                    ViewBag.Message = "<div class=\"alert alert-success\" role=\"alert\">Доступ к вашей учетной записи восстановлен</div>";
                }
                else
                {
                    ViewBag.Message = "<div class=\"alert alert-danger\" role=\"alert\">Пользователя с таким именем не существует</div>";
                }
            }
            else
            {
                ViewBag.Message = "<div class=\"alert alert-danger\" role=\"alert\">Пожалуйста, введите данные в поля, расположенные ниже</div>";
            }

            return View();
        }

        private string GenerateTemporaryPassword()
        {
            Random generator = new Random();

            int n1 = generator.Next(1, 10);
            int n2 = generator.Next(1, 10);
            int n3 = generator.Next(1, 10);
            int n4 = generator.Next(1, 10);
            int n5 = generator.Next(1, 10);
            int n6 = generator.Next(1, 10);
            int n7 = generator.Next(1, 10);
            int n8 = generator.Next(1, 10);

            return $"{n1}{n2}{n3}{n4}{n5}{n6}{n7}{n8}";
        }

        [Route("Forbidden")]
        public IActionResult Rejection()
        {
            return View("Rejection");
        }
    }
}