using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplicationBasic.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            HttpContext.Session = null;
            await HttpContext.Authentication.SignOutAsync("MyAuthenticationScheme");
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Login()
        {
            const string Issuer = "https://example.com";
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, "Andrew", ClaimValueTypes.String, Issuer),
                new Claim(ClaimTypes.Surname, "Lock", ClaimValueTypes.String, Issuer),
                new Claim(ClaimTypes.Country, "UK", ClaimValueTypes.String, Issuer),
                new Claim("ChildhoodHero", "Ronnie James Dio", ClaimValueTypes.String)
            };
            var userIdentity = new ClaimsIdentity(claims, "SessionToken");
            var userPrincipal = new ClaimsPrincipal(userIdentity);
            IIdentity identity = new GenericIdentity("MyIdenity");
            await HttpContext.Authentication.SignInAsync("MyAuthenticationScheme", userPrincipal,
             new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                IsPersistent = false,
                AllowRefresh = false
            });
            HttpContext.User = HttpContext.Authentication.HttpContext.User;
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Unathorized()
        {
            return View();
        }
    }

}
