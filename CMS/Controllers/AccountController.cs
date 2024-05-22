using CMS.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace CMS
{
    /// <summary>
    /// Role:
    /// ---------------
    /// A role is nothing more than a string that identifies a permission set for an authenticated user
    /// in the system. The role-based authorization model allows, in .NET, the use of the Authorize attribute 
    /// to restrict access to a resource, based on the specified role. The latter is applied to a controller 
    /// or to an action.
    /// Example:
    /// [Authorize(Roles = "Administrator")] //restrict access to the “AccountController” to system administrators only,
    /// public class AccountController : Controller
    /// {
    ///     //Code
    /// }
    /// 
    ///  Multiple roles can access this controller,
    ///  [Authorize(Roles = "Manager,Administrator")]
    ///  public class AccountController : Controller
    ///  {
    ///      //Code
    ///  }
    ///  
    /// 
    /// Policy, Requirements, and Handlers:
    /// ------------------------------------------
    /// A policy is a set of requirements; a requirement is a set of parameters that are used to validate 
    /// the identity of the user, while a handler is used to determine whether a user has access to a 
    /// specific resource using the parameters contained in the requirements.
    /// 
    /// A policy is usually registered at the startup of the application, more precisely in the 
    /// ConfigureServices() method of the class Startup.cs.
    /// services.AddAuthorization(options =>
    /// {
    ///     options.AddPolicy("ShouldBeOnlyEmployee", policy =>
    ///        policy.RequireClaim("EmployeeId")); //Claims to express policy requirements.
    /// });
    /// 
    /// Once registered, the policy is usable through the Authorize attribute on a controller or 
    /// on a specific Action.
    /// [Authorize(Policy = "ShouldBeOnlyEmployee")]
    /// public IActionResult SomeMethod()
    /// {
    ///    //Write your code here
    /// }
    ///  </summary>
    public class AccountController : Controller
    {
        private const string ActionName = "Login";
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private object userId;

        public AccountController(UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user==null)
            {
                ModelState.AddModelError("message", "User does not exist.");
                return View(model);

            }
            if (user != null && !user.EmailConfirmed)
            {
                ModelState.AddModelError("message", "Email not confirmed yet");
                return View(model);

            }
            if (await _userManager.CheckPasswordAsync(user, model.Password) == false)
            {
                ModelState.AddModelError("message", "Invalid credentials");
                return View(model);

            }

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
            if (result.Succeeded)
            {

                //When an identity is created it may be assigned one or more claims issued by a trusted party.
                //A claim is a name value pair that represents what the subject is, not what the subject can 
                //do. For example, driver's license, issued by a local driving license authority. 
                //claim name: DateOfBirth, the claim value would be your date of birth, for example 8th June 1995
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                 };

                var dbClaims = await _userManager.GetClaimsAsync(user);
                if(dbClaims.Any())
                {
                    userClaims.AddRange(dbClaims);
                }




                var grandmeIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                var userPrincipal = new ClaimsPrincipal(new[] { grandmeIdentity });
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties { });


                return RedirectToAction("index", "home");
            }
            else if (result.IsLockedOut)
            {
                return View("AccountLocked");
            }
            else
            {
                ModelState.AddModelError("message", "Invalid login attempt");
                return View(model);
            }
        }

        [HttpGet]
       // [Authorize]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        // [Authorize]
        public async Task<IActionResult> Register(RegisterModel model)
        {                               
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed=true
                };

                string defaultRole = "Admin";
                if(!_roleManager.Roles.Any())
                {
                    var identityRole = new IdentityRole { Name = defaultRole };
                    var addRoleResp = await _roleManager.CreateAsync(identityRole);
                }

         
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var addRoleToUser = await _userManager.AddToRoleAsync(user, defaultRole);
                    var addClaimResult = await _userManager.AddClaimAsync(user, new Claim("Designation", "Branch Manager"));

                    return (IActionResult)RedirectToAction(Login);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
           
            return View(model);

        }

        private async RedirectToAction(Func<IActionResult> login)
        {
            throw new NotImplementedException();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}
