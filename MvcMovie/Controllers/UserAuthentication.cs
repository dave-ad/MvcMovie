using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers;

public class UserAuthentication : Controller
{
    private IUserAuthenticationService _userAuthenticationService;
    public UserAuthentication(IUserAuthenticationService userAuthenticationService)
    {

       this._userAuthenticationService = userAuthenticationService;

    }

    //public async Task<IActionResult> Register()
    //{
    //    var model = new RegistrationModel
    //    {
    //        Email = "admin@gmail.com",
    //        Username = "admin",
    //        Name = "Ravindra",
    //        Password = "Admin@123",
    //        ConfirmPassword = "Admin@123",
    //        Role = "Admin"
    //    };
    //    // to register with user, Change Role = "User"

    //    var result = await _userAuthenticationService.RegisterAsync(model);
    //    return Ok(result.Message);
    //}

    public async Task<IActionResult> Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _userAuthenticationService.LoginAsync(model);
        if (result.StatusCode == 1)
            return RedirectToAction("Index", "Home");
        else
        { 
            TempData["msg"]="Could not log in - Error on server side";
            return RedirectToAction(nameof(Login));
        }
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await _userAuthenticationService.LogoutAsync();
        return RedirectToAction(nameof(Login));
    }


}
