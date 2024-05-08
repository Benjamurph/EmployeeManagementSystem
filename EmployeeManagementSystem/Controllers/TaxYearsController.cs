using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeeManagementSystem.Controllers
{
    public class TaxYearsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaxYearsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaxYears
        public async Task<IActionResult> Index()
        {
            return View(await _context.TaxYears.ToListAsync());
        }

        // GET: TaxYears/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxYear = await _context.TaxYears
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxYear == null)
            {
                return NotFound();
            }

            return View(taxYear);
        }

        // GET: TaxYears/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaxYears/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaxYear taxYear)
        {
            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            taxYear.CreatedById = Userid;
            taxYear.CreatedOn = DateTime.Now;
            taxYear.ModifiedByID = Userid;
            taxYear.ModifiedOn = DateTime.Now;

            _context.Add(taxYear);
            await _context.SaveChangesAsync(Userid);
            return RedirectToAction(nameof(Index));

            return View(taxYear);
        }

        // GET: TaxYears/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxYear = await _context.TaxYears.FindAsync(id);
            if (taxYear == null)
            {
                return NotFound();
            }
            return View(taxYear);
        }

        // POST: TaxYears/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaxYear taxYear)
        {
            if (id != taxYear.Id)
            {
                return NotFound();
            }


            try
            {
                var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var oldtaxyear = await _context.Employees.FindAsync(id);
                taxYear.ModifiedByID = Userid;
                taxYear.ModifiedOn = DateTime.Now;
                _context.Entry(oldtaxyear).CurrentValues.SetValues(taxYear);
                await _context.SaveChangesAsync(Userid);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaxYearExists(taxYear.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            return View(taxYear);
        }

        // GET: TaxYears/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxYear = await _context.TaxYears
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxYear == null)
            {
                return NotFound();
            }

            return View(taxYear);
        }

        // POST: TaxYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taxYear = await _context.TaxYears.FindAsync(id);
            if (taxYear != null)
            {
                _context.TaxYears.Remove(taxYear);
            }
            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _context.SaveChangesAsync(Userid);
            return RedirectToAction(nameof(Index));
        }

        private bool TaxYearExists(int id)
        {
            return _context.TaxYears.Any(e => e.Id == id);
        }
    }
}
