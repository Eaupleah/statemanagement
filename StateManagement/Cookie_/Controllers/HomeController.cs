using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cookie_.Models;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace Cookie_.Controllers;

public class HomeController : Controller
{
    private readonly string _cookieName = "Survey";


    public IActionResult Index()
    {
        var cookie = Request.Cookies[_cookieName];

        return View(model: cookie, viewName: nameof(Index));
    }

    [HttpPost]
    public IActionResult Index(string survey)
    {
        if (survey is null)
            return RedirectToAction(nameof(Index));
        CookieOptions options = new()
        {
            Expires = DateTime.Now.AddSeconds(10)
        };
        Response.Cookies.Append(_cookieName, survey, options);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Remove()
    {
        Response.Cookies.Delete(_cookieName);
        return RedirectToAction(nameof(Index));
    }
}