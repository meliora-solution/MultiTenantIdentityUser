using Asp.Versioning;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using SharedServices.Models.Account;
using System.Security.Claims;

namespace WebApiSample.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signInManager,
                                HttpClient httpClient)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpClient = httpClient;
        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] LoginUser obj)
        {
            /*
                 returning with claims
             https://stackoverflow.com/questions/45100238/net-core-cookie-authentication-signinasync-not-working
         
             Login Api EndPoint with cookies
            'https://localhost:7029/api/auth-default/login?useCookies=false&useSessionCookies=true'

             string uri = "https://localhost:44305/api/auth-default/login?useCookies=true";
           
             */

            try
            {
 

                string uri = "https://localhost:44305/api/auth-default/login";
                var loginResponse = await _httpClient.PostAsJsonAsync(uri, obj);
                //   var result = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();

                if (loginResponse.IsSuccessStatusCode)
                {
                    return Ok(await loginResponse.Content.ReadFromJsonAsync<LoginResponse>());
                }
                //var contentTemp = await loginResponse.Content.ReadAsStringAsync();
                //var result = await _signInManager.PasswordSignInAsync(obj.Email, obj.Password, true, false);
                //if (result.Succeeded)
                //{

                //    return Ok();
                //}
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }




            return BadRequest();

        }

        [HttpPost]
        [Route("SignUp")]
        //[Authorize]
        public async Task<IActionResult> SignUp([FromBody] RegisterUser obj)
        {
            IdentityUser user;
            string Email = obj.Email;
            string Password = obj.Password;

            user = new IdentityUser
            {
                UserName = Email,
                Email = Email

            };


            //ServiceResponseDto<bool> response = new();
            var response = await _userManager.CreateAsync(user, Password);


            if (response.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}
