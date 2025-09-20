using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantWebApi.DataAccess.Context;
using RestaurantWebApi.Model;
using RestaurantWebSite.Helper;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace RestaurantWebSite.Controllers
{
    public class LoginController : Controller
    {
        private readonly DataContext _dbContext;
        public LoginController(DataContext dataContext)
        {
            _dbContext = dataContext;
            
        }
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Register(UserInputModel userInputModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User();


                var sb = new StringBuilder();
                using (SHA256 sha256 = SHA256.Create())
                {
                    var bytes = Encoding.UTF8.GetBytes(userInputModel.Password);
                    var hash = sha256.ComputeHash(bytes);
                    foreach (var b in hash)
                    {
                        sb.Append(b.ToString("x2"));
                    }
                    sb.ToString();
                }
                user.Username=userInputModel.Username;
                user.FirstName=userInputModel.FirstName;
                user.LastName=userInputModel.LastName;
                user.Email=userInputModel.Email;

                user.Password = sb.ToString();
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            ;

            return View(userInputModel);

        }



        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {

                var sb = new StringBuilder();
                using (SHA256 sha256 = SHA256.Create())
                {
                    var bytes = Encoding.UTF8.GetBytes(password);
                    var hash = sha256.ComputeHash(bytes);
                    foreach (var b in hash)
                    {
                        sb.Append(b.ToString("x2"));
                    }
                    sb.ToString();
                }

                var ss = sb.ToString();
                var user = await _dbContext.Users.Where(u => u.Username == username && u.Password == ss).FirstOrDefaultAsync();

                if (user != null)
                {
                    return RedirectToAction("Welcome");
                }

                ViewBag.Error = "Invalid username or password";
                return View();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult Welcome()
        {
            return Content("Welcome to Culinary World! 🎉");
        }

    }
}
