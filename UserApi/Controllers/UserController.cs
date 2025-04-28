using Microsoft.AspNetCore.Mvc;

namespace UserApi.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
