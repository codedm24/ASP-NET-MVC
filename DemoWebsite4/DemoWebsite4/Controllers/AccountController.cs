using DemoWebsite4.Data;
using DemoWebsite4.Migrations.ApplicationDb;
using DemoWebsite4.Models;
using DemoWebsite4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;

namespace DemoWebsite4.Controllers
{
    [Authorize] 
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;

        private static bool _databaseCheccked;

        public AccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IEmailSender emailSender, ISmsSender smsSender,
            ApplicationDbContext applicationDbContext,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _applicationDbContext = applicationDbContext;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }
    
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl= null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if(!await EnsureDatabaseCreated(_applicationDbContext))
                return View(model);

            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            { 
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                { 
                    return RedirectToLocal(returnUrl);        
                }

                if (result.RequiresTwoFactor)
                {
                    //return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }

                if (result.IsLockedOut)
                {
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            RegisterViewModel registerViewModel = new RegisterViewModel();
            return View(registerViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!await EnsureDatabaseCreated(_applicationDbContext))
                return View(registerViewModel);

            if (ModelState.IsValid)
            {
                try
                {
                    var user = new ApplicationUser
                    {
                        UserName = registerViewModel.Email,
                        Email = registerViewModel.Email,
                    };
                    var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation(3, "User created a new account with password.");
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }

                    AddErrors(result);
                }
                catch (Exception ex) {
                    int tt = 0;
                }

            }

            return View(registerViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        { 
            return View(); 
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);

                if (user == null )//|| !await _userManager.IsEmailConfirmedAsync(user))
                {
                    return View("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                //ResetPasswordViewModel resetModel = new ResetPasswordViewModel() { UserId = user.Id, Email = user.Email??string.Empty, Code = code };
                ViewBag.CallbackUrl = callbackUrl;
                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword()
        {
            string code = Request.Query["code"].ToString();
            string userId = Request.Query["userId"].ToString();
            ViewBag.Code = code;
            ViewBag.UserId = userId;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user,model.Code,model.Password);
            if (result.Succeeded) {
                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }

            ViewBag.Code = model.Code;
            ViewBag.UserId = model.UserId;

            AddErrors(result);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        { 
            return View(); 
        }


        private void AddErrors(IdentityResult result) {
            foreach (var error in result.Errors)
            { 
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public async Task<bool> EnsureDatabaseCreated(ApplicationDbContext _applicationDbContext)
        {
            var init = new ApplicationDbInitializer(_applicationDbContext);
            try
            {
                await init.CreateDatabaseAsync();
            }
            catch (Exception ex)
            {
                int tt = 0;
                return false;
            }
            return true;
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            { 
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
