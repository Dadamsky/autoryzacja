using Microsoft.AspNetCore.Mvc;
using autoryzacja.Models; // Replace with your actual namespace
using autoryzacja.Areas.Identity.Data;  // Replace with your actual namespace

namespace autoryzacja.Controllers
{
    public class RezerwacjeController : Controller
    {
        private readonly ApplicationDBContext _context;

        public RezerwacjeController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Display reservations
        public IActionResult Index()
        {
            var reservations = _context.CarReservations.ToList(); // Assuming CarReservations is your DbSet
            return View(reservations);
        }

        // POST: Create a new reservation
        [HttpPost]
        public IActionResult Create(CarReservation reservation) // Assuming CarReservation is your model
        {
            if (ModelState.IsValid)
            {
                _context.CarReservations.Add(reservation);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        // GET: Display details of a specific reservation
        public IActionResult Details(int id)
        {
            var reservation = _context.CarReservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        // POST: Update a reservation
        [HttpPost]
        public IActionResult Edit(int id, CarReservation reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Update(reservation);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        // POST: Delete a reservation
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var reservation = _context.CarReservations.Find(id);
            if (reservation != null)
            {
                _context.CarReservations.Remove(reservation);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
