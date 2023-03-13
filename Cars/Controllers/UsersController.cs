namespace CarRentingSystem.ControllersIdentityUser
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class UsersController : Controller
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager1;
        public UsersController(UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager1 = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        // Example nezavarshen
        //[HttpPost]
        //public async Task<IActionResult> Register(string email, string password)
        //{
        //    var user = new IdentityUser
        //    {
        //        Email = email
        //    };
        //    await this.userManager.CreateAsync(user, password);

        //    return Redirect();
        //}

        //public IActionResult Login(string email, string password)
        //{
        //    var result = await this.signInManager1.SignInAsync(SignInResult res);
        //}
    }
}
