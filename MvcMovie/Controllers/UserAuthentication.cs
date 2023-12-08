using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers
{
    public class UserAuthentication : Controller
    {
        private IUserAuthenticationService _userAuthenticationService;
        public UserAuthentication(IUserAuthenticationService userAuthenticationService)
        {

           this._userAuthenticationService = userAuthenticationService;

        }
        public async Task<IActionResult> Register()
        {
            var model = new RegistrationModel
            {
                Email = "admin@gmail.com",
                Username = "admin",
                Name = "Ravindra",
                Password = "Admin@123",
                ConfirmPassword = "Admin@123",
                Role = "Admin"
            };
            // to register with user, Chnage Role = "User"

            var result = await _userAuthenticationService.RegisterAsync(model);
            return Ok(result.Message);
        }
    }
}
