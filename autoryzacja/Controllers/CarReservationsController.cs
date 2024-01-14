using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using autoryzacja.Areas.Identity.Data;
using autoryzacja.Models;
using Microsoft.AspNetCore.Authorization;

namespace autoryzacja.Controllers
{
    public class CarReservationsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public CarReservationsController(ApplicationDBContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: CarReservations
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarReservations.ToListAsync());
        }

        // GET: CarReservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carReservation = await _context.CarReservations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carReservation == null)
            {
                return NotFound();
            }

            return View(carReservation);
        }

        // GET: CarReservations/Create
        [Authorize]
        public IActionResult Create()
        {
            var carList = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Audi RS3" },
            new SelectListItem { Value = "2", Text = "BMW 3" },
            new SelectListItem { Value = "3", Text = "Mercedes-Benz C-Class" }
            // Dodaj więcej samochodów
        };

            ViewBag.CarList = carList;

            return View();
        }

        // POST: CarReservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarId,PickupDate,ReturnDate")] CarReservation carReservation)
        {
            if (ModelState.IsValid)
            {
                
                bool isReserved = _context.CarReservations.Any(r =>
                    r.CarId == carReservation.CarId &&
                    ((carReservation.PickupDate >= r.PickupDate && carReservation.PickupDate <= r.ReturnDate) ||
                     (carReservation.ReturnDate >= r.PickupDate && carReservation.ReturnDate <= r.ReturnDate)));

                if (isReserved)
                {
                    
                    ModelState.AddModelError(string.Empty, "Przepraszamy auto jest już zarezerwowane w tych godzinach");
                    return View(carReservation);
                }


                _context.Add(carReservation);
                await _context.SaveChangesAsync();
                ViewBag.SuccessMessage = "Pomyślnie zarezerwowano samochód!";
                return View(carReservation);
            }

            
            return View(carReservation);
        }




        // GET: CarReservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carReservation = await _context.CarReservations.FindAsync(id);
            if (carReservation == null)
            {
                return NotFound();
            }
            return View(carReservation);
        }

        // POST: CarReservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarId,PickupDate,ReturnDate")] CarReservation carReservation)
        {
            if (id != carReservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carReservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarReservationExists(carReservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(carReservation);
        }

        // GET: CarReservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carReservation = await _context.CarReservations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carReservation == null)
            {
                return NotFound();
            }

            return View(carReservation);
        }

        // POST: CarReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carReservation = await _context.CarReservations.FindAsync(id);
            if (carReservation != null)
            {
                _context.CarReservations.Remove(carReservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarReservationExists(int id)
        {
            return _context.CarReservations.Any(e => e.Id == id);
        }


    }
}
