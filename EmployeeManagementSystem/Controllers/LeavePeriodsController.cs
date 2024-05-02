using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeeManagementSystem.Controllers
{
    [Authorize(Roles = "Super Administrator,Administrator")]
    public class LeavePeriodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeavePeriodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeavePeriods
        public async Task<IActionResult> Index()
        {
            return View(await _context.leavePeriods.ToListAsync());
        }

        // GET: LeavePeriods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leavePeriod = await _context.leavePeriods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leavePeriod == null)
            {
                return NotFound();
            }

            return View(leavePeriod);
        }

        // GET: LeavePeriods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeavePeriods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeavePeriod leavePeriod)
        {
            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            leavePeriod.CreatedById = Userid;
            leavePeriod.CreatedOn = DateTime.Now;
            leavePeriod.ModifiedByID = Userid;
            leavePeriod.ModifiedOn = DateTime.Now;
            _context.Add(leavePeriod);
            await _context.SaveChangesAsync(Userid);
            return RedirectToAction(nameof(Index));

            return View(leavePeriod);
        }

        // GET: LeavePeriods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leavePeriod = await _context.leavePeriods.FindAsync(id);
            if (leavePeriod == null)
            {
                return NotFound();
            }
            return View(leavePeriod);
        }

        // POST: LeavePeriods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeavePeriod leavePeriod)
        {
            if (id != leavePeriod.Id)
            {
                return NotFound();
            }


            try
            {
                var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var oldleaveperiod = await _context.leavePeriods.FindAsync(id);
                leavePeriod.ModifiedByID = Userid;
                leavePeriod.ModifiedOn = DateTime.Now;
                _context.Entry(oldleaveperiod).CurrentValues.SetValues(leavePeriod);
                await _context.SaveChangesAsync(Userid);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeavePeriodExists(leavePeriod.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            return View(leavePeriod);
        }

        // GET: LeavePeriods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leavePeriod = await _context.leavePeriods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leavePeriod == null)
            {
                return NotFound();
            }

            return View(leavePeriod);
        }

        // POST: LeavePeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leavePeriod = await _context.leavePeriods.FindAsync(id);
            if (leavePeriod != null)
            {
                _context.leavePeriods.Remove(leavePeriod);
            }
            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _context.SaveChangesAsync(Userid);
            return RedirectToAction(nameof(Index));
        }

        private bool LeavePeriodExists(int id)
        {
            return _context.leavePeriods.Any(e => e.Id == id);
        }
    }
}
