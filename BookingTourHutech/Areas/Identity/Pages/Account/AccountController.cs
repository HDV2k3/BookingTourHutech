using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]
public class AccountController : Controller
{
    [HttpGet]
    public IActionResult CheckLoginStatus()
    {
        if (User.Identity.IsAuthenticated)
        {
            return Json(new { loggedIn = true });
        }
        else
        {
            return Json(new { loggedIn = false });
        }
    }
}