using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyAuthWork.Constants;
using MyAuthWork.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyAuthWork.Controllers
{
    [AllowAnonymous]
    public class MvcLoginController : Controller
    {
        private TokenManager _tokenManager;

        public MvcLoginController(TokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserLogin user)
        {
            //var currentUser = UserConstants.Users.FirstOrDefault(o => o.UserName.ToLower() == user.UserName.ToLower() && o.Password == user.Password);
            //if (currentUser != null)
            //{
            //    return RedirectToAction("Index", "Deneme");
            //}
            //return View();
            var user1 = _tokenManager.Authenticate(user);
            if (user1 != null && ModelState.IsValid)
            {

                var token = _tokenManager.Generate(user1);
                HttpContext.Session.SetString("Token", token);
                return RedirectToAction("Index", "Test");

            }
            return View();

        }
     


    }
}
