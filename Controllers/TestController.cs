using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyAuthWork.Controllers
{
    [Authorize(Roles ="Seller")]
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
