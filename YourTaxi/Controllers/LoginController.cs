using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourTaxi.Data;
using YourTaxi.Models;

namespace YourTaxi.Controllers
{
    public class LoginController : Controller
    {
        private readonly YourTaxiContext _context;

        public LoginController(YourTaxiContext context)
        {
            _context = context;
        }

        // GET: Login
        public async Task<IActionResult> Index()
        {
            return View(await _context.Login.ToListAsync());
        }

        public IActionResult Erorr()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin([Bind("Id,Name,Password")] Login login)
        {
            //admin Login details 
            HttpContext.Session.Clear();
           
            if ((login.Name.Equals("Yourtaxi@admin.com") && login.Password.Equals("yourtaxi")))
            {
                    HttpContext.Session.SetString("Name", login.Name.ToString());
                    return RedirectToAction("Index", "Home");
            }
            else 
            {
                return RedirectToAction(nameof(AdminLogin));
            }            
        }
        
        public IActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserLogin([Bind("Id,Name,Password")] Login login)
        {
            // admin Login details
            HttpContext.Session.Clear();
            var loginData = _context.Login.Where(m => m.Name == login.Name && m.Password == login.Password).FirstOrDefault();

            if ((login.Name.Equals("Yourtaxi@admin.com") && login.Password.Equals("admin")) || loginData != null)
            {
                if (loginData != null)
                {
                    HttpContext.Session.SetString("Id", loginData.Id.ToString());
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    HttpContext.Session.SetString("Name", login.Name.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            else 
            {
                return RedirectToAction(nameof(UserLogin));
            }            
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Password")] Login login)
        {
            if (ModelState.IsValid)
            {
                _context.Add(login);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("Id", login.Id.ToString());
                return RedirectToAction("Index", "Home");
            }
            return View(login);
        }

        private bool LoginExists(int id)
        {
            return _context.Login.Any(e => e.Id == id);
        }
    }
}
