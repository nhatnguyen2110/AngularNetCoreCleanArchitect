using System.Diagnostics;
using System.Security.Claims;
using BackgroundJobUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundJobUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HangfireSettings _hangfireSettings;
    public HomeController(ILogger<HomeController> logger
        , HangfireSettings hangfireSettings
        )
    {
        _logger = logger;
        _hangfireSettings = hangfireSettings;
    }
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login([FromForm] AuthenticateRequestModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Username != _hangfireSettings.UserName)
            {
                ModelState.AddModelError("Username", "Username is incorrect");
                return View("Login");
            }
            else if (model.Password != _hangfireSettings.Password)
            {
                ModelState.AddModelError("Password", "Password is incorrect");
                return View("Login");
            }
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "admin"),
            new Claim("FullName", "administrator"),
            new Claim(ClaimTypes.Role, "Administrator"),
        };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(principal),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                });

            return Redirect("/hangfire");
        }

        return View("Login");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        // Clear the existing external cookie
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

}
