using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeeManagementSystem.Controllers
{
    public class PaymentRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PaymentRecords
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PaymentRecords.Include(p => p.Employee).Include(p => p.TaxYear);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PaymentRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentRecord = await _context.PaymentRecords
                .Include(p => p.Employee)
                .Include(p => p.TaxYear)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentRecord == null)
            {
                return NotFound();
            }

            return View(paymentRecord);
        }

        // GET: PaymentRecords/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["TaxYearId"] = new SelectList(_context.TaxYears, "Id", "Year");
            return View();
        }

        // POST: PaymentRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentRecord paymentRecord)
        {

            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employee = await _context.Employees.Where(x => x.Id == paymentRecord.EmployeeId).FirstOrDefaultAsync();
            var taxYear = await _context.TaxYears.Where(x => x.Id == paymentRecord.TaxYearId).FirstOrDefaultAsync();
            if (employee.AnnualSalary < taxYear.PersonalAllowance)
            {
                paymentRecord.TaxRate = 0;
            }
            else if (employee.AnnualSalary >= taxYear.BasicRateLowerThreshold && employee.AnnualSalary <= taxYear.BasicRateUpperThreshold)
            {
                paymentRecord.TaxRate = taxYear.BasicRateTaxPercentage;
            }
            else if (employee.AnnualSalary >= taxYear.HigherRateLowerThreshold && employee.AnnualSalary <= taxYear.HigherRateUpperThreshold)
            {
                paymentRecord.TaxRate = taxYear.HigherRateTaxPercentage;
            }
            else if (employee.AnnualSalary > taxYear.AdditionalRateThreshold)
            {
                paymentRecord.TaxRate = taxYear.AdditionalRateTaxPercentage;
            }
            paymentRecord.ContractualHours = employee.ContractualHours;
            paymentRecord.TotalHoursWorked = employee.ContractualHours + paymentRecord.OvertimeHours;
            paymentRecord.ContractualEarnings = employee.ContractualHours * employee.HourlyRate;
            paymentRecord.OvertimeEarnings = paymentRecord.OvertimeHours * employee.HourlyRate;
            paymentRecord.TotalEarnings = paymentRecord.OvertimeEarnings + paymentRecord.ContractualEarnings;
            if (employee.AnnualSalary < taxYear.PersonalAllowance)
            {
                paymentRecord.TotalDeduction = 0;
            }
            else
            {
                paymentRecord.TotalDeduction = paymentRecord.TotalEarnings * (paymentRecord.TaxRate / 100);
            }
            paymentRecord.NetPayment = paymentRecord.TotalEarnings - paymentRecord.TotalDeduction;
            paymentRecord.CreatedById = Userid;
            paymentRecord.CreatedOn = DateTime.Now;
            paymentRecord.ModifiedByID = Userid;
            paymentRecord.ModifiedOn = DateTime.Now;
            _context.Add(paymentRecord);
            await _context.SaveChangesAsync(Userid);
            return RedirectToAction(nameof(Index));

            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", paymentRecord.EmployeeId);
            ViewData["TaxYearId"] = new SelectList(_context.TaxYears, "Id", "Year", paymentRecord.TaxYearId);
            return View(paymentRecord);
        }

        // GET: PaymentRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentRecord = await _context.PaymentRecords.FindAsync(id);
            if (paymentRecord == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", paymentRecord.EmployeeId);
            ViewData["TaxYearId"] = new SelectList(_context.TaxYears, "Id", "Year", paymentRecord.TaxYearId);
            return View(paymentRecord);
        }

        // POST: PaymentRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PaymentRecord paymentRecord)
        {
            if (id != paymentRecord.Id)
            {
                return NotFound();
            }

            try
            {
                var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var oldPaymentRecord = await _context.PaymentRecords.FindAsync(id);
                var employee = await _context.Employees.Where(x => x.Id == paymentRecord.EmployeeId).FirstOrDefaultAsync();
                var taxYear = await _context.TaxYears.Where(x => x.Id == paymentRecord.TaxYearId).FirstOrDefaultAsync();
                if (employee.AnnualSalary < taxYear.PersonalAllowance)
                {
                    paymentRecord.TaxRate = 0;
                }
                else if (employee.AnnualSalary >= taxYear.BasicRateLowerThreshold && employee.AnnualSalary <= taxYear.BasicRateUpperThreshold)
                {
                    paymentRecord.TaxRate = taxYear.BasicRateTaxPercentage;
                }
                else if (employee.AnnualSalary >= taxYear.HigherRateLowerThreshold && employee.AnnualSalary <= taxYear.HigherRateUpperThreshold)
                {
                    paymentRecord.TaxRate = taxYear.HigherRateTaxPercentage;
                }
                else if (employee.AnnualSalary > taxYear.AdditionalRateThreshold)
                {
                    paymentRecord.TaxRate = taxYear.AdditionalRateTaxPercentage;
                }
                paymentRecord.ContractualHours = employee.ContractualHours;
                paymentRecord.TotalHoursWorked = employee.ContractualHours + paymentRecord.OvertimeHours;
                paymentRecord.ContractualEarnings = employee.ContractualHours * employee.HourlyRate;
                paymentRecord.OvertimeEarnings = paymentRecord.OvertimeHours * employee.HourlyRate;
                paymentRecord.TotalEarnings = paymentRecord.OvertimeEarnings + paymentRecord.ContractualEarnings;
                if (employee.AnnualSalary < taxYear.PersonalAllowance)
                {
                    paymentRecord.TotalDeduction = 0;
                }
                else
                {
                    paymentRecord.TotalDeduction = paymentRecord.TotalEarnings * (paymentRecord.TaxRate / 100);
                }
                paymentRecord.NetPayment = paymentRecord.TotalEarnings - paymentRecord.TotalDeduction;
                paymentRecord.ModifiedByID = Userid;
                paymentRecord.ModifiedOn = DateTime.Now;
                _context.Entry(oldPaymentRecord).CurrentValues.SetValues(paymentRecord);
                await _context.SaveChangesAsync(Userid);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentRecordExists(paymentRecord.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", paymentRecord.EmployeeId);
            ViewData["TaxYearId"] = new SelectList(_context.TaxYears, "Id", "Year", paymentRecord.TaxYearId);
            return View(paymentRecord);
        }

        // GET: PaymentRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentRecord = await _context.PaymentRecords
                .Include(p => p.Employee)
                .Include(p => p.TaxYear)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentRecord == null)
            {
                return NotFound();
            }

            return View(paymentRecord);
        }

        // POST: PaymentRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentRecord = await _context.PaymentRecords.FindAsync(id);
            if (paymentRecord != null)
            {
                _context.PaymentRecords.Remove(paymentRecord);
            }

            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _context.SaveChangesAsync(Userid);
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentRecordExists(int id)
        {
            return _context.PaymentRecords.Any(e => e.Id == id);
        }
    }
}
