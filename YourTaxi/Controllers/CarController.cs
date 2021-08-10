using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourTaxi.Data;
using YourTaxi.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace YourTaxi.Controllers
{
    public class CarController : Controller
    {
        private readonly YourTaxiContext _context;

        public CarController(YourTaxiContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
          return View(await _context.Cars.ToListAsync());
        }
        public async Task<IActionResult> AddBook(int? id)
        {
            if (HttpContext.Session.GetString("Id") != null)
            {
                var value = HttpContext.Session.GetString("Id");
                // car booking model
                CarBook carbook = new CarBook();
                var book = _context.Cars.Where(x => x.Id == id).FirstOrDefault();
                carbook.CarName = book.CarName;
                carbook.CarModel = book.CarModel;
                carbook.From = book.From;
                carbook.To = book.To;
                carbook.Price = book.Price;
                carbook.UserId = Convert.ToInt32(value);
                _context.Add(carbook);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "UserCars");
            }
            else
                return RedirectToAction("UserLogin", "Login");
        }

    }
}
