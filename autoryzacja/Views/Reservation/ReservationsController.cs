using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using autoryzacja.Areas.Identity.Data;
using autoryzacja.Models;

namespace autoryzacja.Views.Reservation
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ReservationsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarReservations.ToListAsync());
        }

        // GET: Reservations/Details/5
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

        // GET: Reservations/Create
        public IActionResult Create()
        {
            // Utwórz listę samochodów
            var carList = new List<SelectListItem>
        {
           
            // Dodaj więcej samochodów zgodnie z potrzebami
        };

            // Przekaż listę do widoku
            ViewBag.CarList = carList;

            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarId,PickupDate,ReturnDate")] CarReservation carReservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carReservation);
        }

        // GET: Reservations/Edit/5
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

        // POST: Reservations/Edit/5
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

        // GET: Reservations/Delete/5
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

        // POST: Reservations/Delete/5
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
