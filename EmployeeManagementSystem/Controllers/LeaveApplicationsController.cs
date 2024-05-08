using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeeManagementSystem.Controllers
{
    [Authorize(Roles = "Super Administrator,Administrator,Human Resource Manager,Roleless")]
    public class LeaveApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaveApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaveApplications
        public async Task<IActionResult> Index()
        {
            var pending = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Pending").First();
            var applicationDbContext = _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .Where(l => l.StatusId == pending!.Id);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ApprovedLeaveApplications

        public async Task<IActionResult> ApprovedLeaveApplications()
        {
            var approvedStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Approved").First();
            var applicationDbContext = _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .Where(l => l.StatusId == approvedStatus!.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RejectedLeaveApplications
        public async Task<IActionResult> RejectedLeaveApplications()
        {
            var rejectedStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Rejected").First();
            var applicationDbContext = _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .Where(l => l.StatusId == rejectedStatus!.Id); ;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeaveApplications/ApproveLeave/5
        [Authorize(Roles = "Super Administrator,Administrator,Human Resource Manager")]
        [HttpGet]

        public async Task<IActionResult> ApproveLeave(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");

            return View(leaveApplication);
        }

        // POST: LeaveApplications/ApproveLeave/5
        [HttpPost]

        public async Task<IActionResult> ApproveLeave(int? id, LeaveApplication leave)
        {
            var approvedstatus = _context.SystemCodeDetails
                .Include(x => x.SystemCode)
                .Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Approved")
                .FirstOrDefault();

            var adjustmenttype = _context.SystemCodeDetails
                .Include(x => x.SystemCode)
                .Where(y => y.SystemCode.Code == "LeaveAdjustment" && y.Code == "Negative")
                .FirstOrDefault();

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            leaveApplication.ApprovedOn = DateTime.Now;
            leaveApplication.ApprovedById = Userid;
            leaveApplication.StatusId = approvedstatus!.Id;
            leaveApplication.ApprovalNotes = leave.ApprovalNotes;
            _context.Update(leaveApplication);
            await _context.SaveChangesAsync(Userid);

            var adjustment = new LeaveAdjustmentEntry
            {
                EmployeeId = leaveApplication.EmployeeId,
                NoOfDays = leaveApplication.NumberOfDays,
                LeaveStartDate = leaveApplication.StartDate,
                LeaveEndDate = leaveApplication.EndDate,
                AdjustmentDescription = "Leave Taken - Negative Adjustment",
                LeavePeriodId = 1,
                LeaveAdjustmentDate = DateTime.Now,
                AdjustmentTypeId = adjustmenttype.Id
            };

            _context.Add(adjustment);
            await _context.SaveChangesAsync(Userid);

            var employee = await _context.Employees.FindAsync(leaveApplication.EmployeeId);
            employee.LeaveBalance = (employee.LeaveBalance - leaveApplication.NumberOfDays);
            _context.Update(employee);
            await _context.SaveChangesAsync(Userid);

            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");
            return RedirectToAction(nameof(Index));
            return View(leaveApplication);
        }

        // GET: LeaveApplications/RejectLeave/5
        [Authorize(Roles = "Super Administrator,Administrator,Human Resource Manager")]
        [HttpGet]

        public async Task<IActionResult> RejectLeave(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");
            return View(leaveApplication);
        }

        // POST: LeaveApplications/RejectLeave/5
        [HttpPost]

        public async Task<IActionResult> RejectLeave(int? id, LeaveApplication leave)
        {
            var rejectedStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Rejected").First();

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            leaveApplication.ApprovedOn = DateTime.Now;
            leaveApplication.ApprovedById = Userid;
            leaveApplication.ApprovalNotes = leave.ApprovalNotes;
            leaveApplication.StatusId = rejectedStatus.Id;
            _context.Update(leaveApplication);
            await _context.SaveChangesAsync(Userid);

            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");
            return RedirectToAction(nameof(Index));
            return View(leaveApplication);
        }



        // GET: LeaveApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // GET: LeaveApplications/Create
        public IActionResult Create()
        {

            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");
            return View();
        }

        // POST: LeaveApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveApplication leaveApplication)
        {
            var pendingStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.Code == "Pending" && y.SystemCode.Code == "LeaveApprovalStatus").First();

            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            leaveApplication.CreatedById = Userid;
            leaveApplication.CreatedOn = DateTime.Now;
            leaveApplication.ModifiedByID = Userid;
            leaveApplication.ModifiedOn = DateTime.Now;
            leaveApplication.StatusId = pendingStatus.Id;
            _context.Add(leaveApplication);
            await _context.SaveChangesAsync(Userid);
            return RedirectToAction(nameof(Index));

            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        // GET: LeaveApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplication == null)
            {
                return NotFound();
            }
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        // POST: LeaveApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveApplication leaveApplication)
        {
            if (id != leaveApplication.Id)
            {
                return NotFound();
            }


            try
            {
                var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var oldleaveapplication = await _context.LeaveApplications.FindAsync(id);
                leaveApplication.ModifiedByID = Userid;
                leaveApplication.ModifiedOn = DateTime.Now;
                _context.Entry(oldleaveapplication).CurrentValues.SetValues(leaveApplication);
                await _context.SaveChangesAsync(Userid);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveApplicationExists(leaveApplication.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        // GET: LeaveApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // POST: LeaveApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplication != null)
            {
                _context.LeaveApplications.Remove(leaveApplication);
            }

            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _context.SaveChangesAsync(Userid);
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveApplicationExists(int id)
        {
            return _context.LeaveApplications.Any(e => e.Id == id);
        }
    }
}
