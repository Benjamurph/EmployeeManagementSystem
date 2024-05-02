using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace EmployeeManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index(DashboardViewModel dashboard)
        {

            dashboard.Announcements = await _context.Announcements.ToListAsync();
            var users = await _context.Users.ToListAsync();
            dashboard.Users = users.Count();
            var employess = await _context.Employees.ToListAsync();
            dashboard.Employees = employess.Count();
            var pending = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Pending").First();
            var leaveApplications = _context.LeaveApplications.Where(l => l.StatusId == pending!.Id);
            var jobApplications = _context.JobApplications.Where(l => l.StatusId == pending!.Id);
            dashboard.LeaveApplications = leaveApplications.Count();
            dashboard.JobApplications = jobApplications.Count();
            return !User.Identity.IsAuthenticated ? this.Redirect("~/identity/account/login") : View(dashboard);
        }

        public async Task<IActionResult> SideBar()
        {
            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users.Where(x => x.Id == Userid).FirstOrDefaultAsync();
            var userName = user.FullName;
            ViewData["UserFullName"] = userName;
            ViewData["UserProfilePicture"] = user.Photo + user.FileName;

            return View(userName);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
