using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice1.Models;
using Practice1;

namespace Practice1.Controllers;

public class HomeController : Controller
{
    //private readonly ILogger<HomeController> _logger;
    private readonly PracticeCrossfitContext _context;

    public HomeController(PracticeCrossfitContext context)//ILogger<HomeController> logger, 
    {
        //_logger = logger;
        _context = context;
    }

    public async void Profile()
    {
        var u = new UserController(_context);
        await u.Details(Practice1.User.CurrentUserIdGet());
    }
    
    public IActionResult Denied()
    {
        return View();
    }
    
    // GET
    public IActionResult Login()
    {
        return View();
    }

    //post
    [HttpPost]
    public async Task<IActionResult> Login(string Email, string Password)
    {
        if (ModelState.IsValid)
        {
            var userIn = _context.Users
                .FirstOrDefault(u => u.Email == Email);
            if (userIn != null)
            {
                var res = userIn.VerifyPassword(Password);
                if (res)
                {
                    Practice1.User.CurrentUserIdSet(userIn.Id);
                    await _context.SaveChangesAsync();
                    var t = new TimetableController(_context);
                    t.Index();
                    
                }
                else
                {
                    RedirectToAction(nameof(Denied));
                }
            }
        }
        return View();
    }
    
    public async Task<IActionResult> Logout()
    {
        Practice1.User.CurrentUserIdSet(-1);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Login));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}