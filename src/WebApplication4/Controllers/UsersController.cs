using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BWSC.Models;
using BWSC.Models.ManageViewModels;
using BWSC.Services;
using Microsoft.EntityFrameworkCore;
using BWSC.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BWSC.Controllers
{
    
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext ApplicationDbContext;

        public UsersController(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailSender emailSender,
        ISmsSender smsSender,
        ILoggerFactory loggerFactory)
        {
            ApplicationDbContext = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<UsersController>();
        }
        //
        // GET: /Users/Index
        [HttpGet]
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        // GET: /Users/Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(Id);
            //populateRolesDropDownList(user);
            return View(user);
        
        }
        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Id, [Bind("Id,Email")] ApplicationUser updatedUser)
        {
            //var name = RoleName;
            if (Id != updatedUser.Id)
            {
                return NotFound();
            }

            var SavedUser = await _userManager.FindByIdAsync(updatedUser.Id);
            SavedUser.Email = updatedUser.Email;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _userManager.UpdateAsync(SavedUser);
                    if (!result.Succeeded)
                        {
                        throw new Exception();
                    }
                    //ApplicationDbContext.Update(SavedUser);
                    //await ApplicationDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }

            return View(updatedUser);
        }
        //
        // GET: /Users/ChangePassword
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        private void populateRolesDropDownList(ApplicationUser user)
        {
            var list = ApplicationDbContext.Roles.OrderBy(r => r.Name).ToList().Select(rr =>
            new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            //var roles = user.Roles;
            //ViewBag.RoleID = roles.ToList();
        }
       
       
        //
        // POST: /Users/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Action("LinkLoginCallback", "Manage");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));
            return Challenge(properties, provider);
        }

        
    }
}
