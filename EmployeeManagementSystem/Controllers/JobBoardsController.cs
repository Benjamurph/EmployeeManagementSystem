using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeeManagementSystem.Controllers
{
    public class JobBoardsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public JobBoardsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _webHostEnvironment = hostEnvironment;
        }

        // GET: JobBoards
        public async Task<IActionResult> Index(JobsBoardViewModel jobs)
        {
            //var departments = _context.Departments.ToList<Department>();
            jobs.JobBoards = await _context.JobBoards.ToListAsync();
            jobs.Departments = await _context.Departments.ToListAsync();
            jobs.Designations = await _context.Designations.ToListAsync();
            jobs.Cities = await _context.Cities.ToListAsync();


            return View(jobs);

        }

        // GET: JobBoards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobBoard = await _context.JobBoards
                .Include(j => j.City)
                .Include(j => j.Department)
                .Include(j => j.Designation)
                .Include(j => j.Expired)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobBoard == null)
            {
                return NotFound();
            }

            return View(jobBoard);
        }

        [Authorize(Roles = "Super Administrator,Administrator,Human Resource Manager")]
        // GET: JobBoards/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["DesignationId"] = new SelectList(_context.Designations, "Id", "Name");
            ViewData["ExpiredId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "ExpirationStatus"), "Id", "Description");
            return View();
        }

        // POST: JobBoards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobBoard jobBoard)
        {
            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            jobBoard.CreatedById = Userid;
            jobBoard.CreatedOn = DateTime.Now;
            jobBoard.ModifiedByID = Userid;
            jobBoard.ModifiedOn = DateTime.Now;
            _context.Add(jobBoard);
            await _context.SaveChangesAsync(Userid);
            return RedirectToAction(nameof(Index));

            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["DesignationId"] = new SelectList(_context.Designations, "Id", "Name");
            ViewData["ExpiredId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "ExpirationStatus"), "Id", "Description");
            return View(jobBoard);
        }

        [Authorize(Roles = "Super Administrator,Administrator,Human Resource Manager")]
        // GET: JobBoards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobBoard = await _context.JobBoards.FindAsync(id);
            if (jobBoard == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["DesignationId"] = new SelectList(_context.Designations, "Id", "Name");
            ViewData["ExpiredId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "ExpirationStatus"), "Id", "Description");
            return View(jobBoard);
        }

        // POST: JobBoards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JobBoard jobBoard)
        {
            if (id != jobBoard.Id)
            {
                return NotFound();
            }


            try
            {
                var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                jobBoard.ModifiedByID = Userid;
                jobBoard.ModifiedOn = DateTime.Now;
                _context.Update(jobBoard);
                await _context.SaveChangesAsync(Userid);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobBoardExists(jobBoard.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Id", jobBoard.CityId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", jobBoard.DepartmentId);
            ViewData["DesignationId"] = new SelectList(_context.Designations, "Id", "Id", jobBoard.DesignationId);
            ViewData["ExpiredId"] = new SelectList(_context.SystemCodeDetails, "Id", "Id", jobBoard.ExpiredId);
            return View(jobBoard);
        }

        [Authorize(Roles = "Super Administrator,Administrator,Human Resource Manager")]
        // GET: JobBoards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobBoard = await _context.JobBoards
                .Include(j => j.City)
                .Include(j => j.Department)
                .Include(j => j.Designation)
                .Include(j => j.Expired)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobBoard == null)
            {
                return NotFound();
            }

            return View(jobBoard);
        }

        // POST: JobBoards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobBoard = await _context.JobBoards.FindAsync(id);
            if (jobBoard != null)
            {
                _context.JobBoards.Remove(jobBoard);
            }
            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _context.SaveChangesAsync(Userid);
            return RedirectToAction(nameof(Index));
        }

        private bool JobBoardExists(int id)
        {
            return _context.JobBoards.Any(e => e.Id == id);
        }

        // GET: JobBoards/Create
        public IActionResult CreateApplication()
        {
            ViewData["GenderId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "Gender"), "Id", "Description");
            ViewData["DisabilityId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "Disability"), "Id", "Description");
            return View();
        }


        // POST: JobBoards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateApplication(int id, JobApplication jobApplication, IFormFile file)
        {
            JobApplication application = new JobApplication();
            application.FirstName = jobApplication.FirstName;
            application.LastName = jobApplication.LastName;
            application.JobPostId = jobApplication.Id;
            application.PhoneNumber = jobApplication.PhoneNumber;
            application.Email = jobApplication.Email;
            application.Address = jobApplication.Address;
            application.PostCode = jobApplication.PostCode;
            application.GenderId = jobApplication.GenderId;
            application.DisabilityId = jobApplication.DisabilityId;
            application.CVFile = file;
            var pendingStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.Code == "Pending" && y.SystemCode.Code == "LeaveApprovalStatus").First();
            application.StatusId = pendingStatus.Id;
            string uniqueFileName = null;
            if (file != null)
            {
                string CVFolder = Path.Combine(_webHostEnvironment.WebRootPath, "CVs");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filepath = Path.Combine(CVFolder, uniqueFileName);
                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    application.CVFile.CopyTo(fileStream);
                }

                application.CV = "/CVs/";
                application.FileName = uniqueFileName;
            }
            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            application.CreatedById = Userid;
            application.CreatedOn = DateTime.Now;
            application.ModifiedByID = Userid;
            application.ModifiedOn = DateTime.Now;
            _context.Add(application);
            await _context.SaveChangesAsync(Userid);
            return RedirectToAction(nameof(Index));
            ViewData["GenderId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "Gender"), "Id", "Description");
            ViewData["DisabilityId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "Disability"), "Id", "Description");
            return View(jobApplication);

        }

        [Authorize(Roles = "Super Administrator,Administrator,Human Resource Manager")]
        public async Task<IActionResult> JobIndex()
        {
            var pending = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Pending").First();
            var applicationDbContext = _context.JobApplications
                .Include(l => l.Status)
                .Where(l => l.StatusId == pending!.Id);

            ViewData["GenderId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "Gender"), "Id", "Description");
            ViewData["DisabilityId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "Disability"), "Id", "Description");
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "Super Administrator,Administrator,Human Resource Manager")]
        public async Task<IActionResult> ApprovedJobApplications()
        {
            var approvedStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Approved").First();
            var applicationDbContext = _context.JobApplications
                .Include(l => l.Status)
                .Where(l => l.StatusId == approvedStatus!.Id);
            ViewData["GenderId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "Gender"), "Id", "Description");
            ViewData["DisabilityId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "Disability"), "Id", "Description");
            return View(await applicationDbContext.ToListAsync());
        }


        [Authorize(Roles = "Super Administrator,Administrator,Human Resource Manager")]
        public async Task<IActionResult> RejectedJobApplications()
        {
            var rejectedStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Rejected").First();
            var applicationDbContext = _context.JobApplications
                .Include(l => l.Status)
                .Where(l => l.StatusId == rejectedStatus!.Id);
            ViewData["GenderId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "Gender"), "Id", "Description");
            ViewData["DisabilityId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "Disability"), "Id", "Description");
            return View(await applicationDbContext.ToListAsync());
        }


        [Authorize(Roles = "Super Administrator,Administrator,Human Resource Manager")]
        [HttpGet]

        public async Task<IActionResult> ApproveJob(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplications
                .Include(l => l.Gender)
                .Include(l => l.Disability)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            ViewData["GenderId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "Gender"), "Id", "Description");
            ViewData["DisabilityId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "Disability"), "Id", "Description");

            return View(jobApplication);
        }

        [HttpPost]

        public async Task<IActionResult> ApproveJob(int? id, JobApplication application)
        {
            var approvedstatus = _context.SystemCodeDetails
                .Include(x => x.SystemCode)
                .Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Approved")
                .FirstOrDefault();

            var jobApplication = await _context.JobApplications
                .Include(l => l.Gender)
                .Include(l => l.Disability)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            jobApplication.StatusId = approvedstatus!.Id;
            _context.Update(jobApplication);
            await _context.SaveChangesAsync(Userid);

            ViewData["GenderId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "Gender"), "Id", "Description");
            ViewData["DisabilityId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "Disability"), "Id", "Description");
            return RedirectToAction(nameof(JobIndex));
            return View(jobApplication);
        }

        [Authorize(Roles = "Super Administrator,Administrator,Human Resource Manager")]
        [HttpGet]

        public async Task<IActionResult> RejectJob(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplications
                .Include(l => l.Gender)
                .Include(l => l.Disability)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            ViewData["GenderId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "Gender"), "Id", "Description");
            ViewData["DisabilityId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "Disability"), "Id", "Description");

            return View(jobApplication);
        }

        [HttpPost]

        public async Task<IActionResult> RejectJob(int? id, JobApplication application)
        {
            var rejectedstatus = _context.SystemCodeDetails
                .Include(x => x.SystemCode)
                .Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Rejected")
                .FirstOrDefault();

            var jobApplication = await _context.JobApplications
                .Include(l => l.Gender)
                .Include(l => l.Disability)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            jobApplication.StatusId = rejectedstatus!.Id;
            _context.Update(jobApplication);
            await _context.SaveChangesAsync(Userid);

            ViewData["GenderId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "Gender"), "Id", "Description");
            ViewData["DisabilityId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "Disability"), "Id", "Description");
            return RedirectToAction(nameof(JobIndex));
            return View(jobApplication);
        }
    }
}
