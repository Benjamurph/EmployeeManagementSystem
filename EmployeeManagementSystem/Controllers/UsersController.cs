using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace EmployeeManagementSystem.Controllers
{

    [Authorize(Roles = "Super Administrator,Administrator")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> rolemanager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment hostEnvironment)
        {
            _rolemanager = rolemanager;

            _signInManager = signInManager;

            _userManager = userManager;
            _context = context;
            _webHostEnvironment = hostEnvironment;
        }

        public async Task<ActionResult> Index()
        {
            var users = await _context.Users.Include(x => x.Role).ToListAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel model, IFormFile file)
        {

            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser user = new ApplicationUser();
            user.UserName = model.UserName;
            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.NormalizedUserName = model.UserName;
            user.Email = model.Email;
            user.EmailConfirmed = true;
            user.CreatedOn = DateTime.Now;
            user.CreatedById = Userid;
            user.RoleId = model.RoleId;
            user.ImageFile = file;
            string uniqueFileName = null;
            if (file != null)
            {
                string ImageUploadedFolder = Path.Combine(_webHostEnvironment.WebRootPath, "UploadedImages");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filepath = Path.Combine(ImageUploadedFolder, uniqueFileName);
                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    user.ImageFile.CopyTo(fileStream);
                }

                user.Photo = "/UploadedImages/";
                user.FileName = uniqueFileName;
            }
            else
            {
                user.Photo = "/UploadedImages/";
                user.FileName = "default-profile-icon-6.jpg";
            }
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var roleName = await _context.Roles.Where(x => x.Id == user.RoleId).FirstOrDefaultAsync();
                await _userManager.AddToRoleAsync(user, roleName.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", model.RoleId);

        }

        [HttpGet]
        public async Task<IActionResult> UserProfile(string? id)
        {
            var user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            ViewData["UserFullName"] = user.FullName;
            ViewData["UserProfilePicture"] = user.Photo + user.FileName;

            return View();
        }

        [Authorize(Roles = "Super Administrator")]
        [HttpGet]
        public async Task<ActionResult> AssignUserRole(string? id)
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AssignUserRole(string? id, UserViewModel newRole)
        {
            var user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            var newRoleName = await _context.Roles.Where(x => x.Id == newRole.RoleId).FirstOrDefaultAsync();
            var oldRoleName = await _context.Roles.Where(x => x.Id == user.RoleId).FirstOrDefaultAsync();
            user.RoleId = newRole.RoleId;

            await _userManager.RemoveFromRoleAsync(user, oldRoleName.Name);
            await _userManager.AddToRoleAsync(user, newRoleName.Name);
            await _context.SaveChangesAsync(id);
            return RedirectToAction("Index");
            return View();
        }
        [Authorize(Roles = "Super Administrator,Administrator")]
        [HttpGet]
        public async Task<ActionResult> EmployeeLink(string? id)
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EmployeeLink(string? id, UserViewModel employee)
        {
            var user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            user.EmployeeId = employee.EmployeeId;
            await _context.SaveChangesAsync(id);
            return RedirectToAction("Index");
            return View();
        }
    }



}