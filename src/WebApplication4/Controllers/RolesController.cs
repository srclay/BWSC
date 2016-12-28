using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BWSC.Data;
using Microsoft.EntityFrameworkCore;

namespace BWSC.Controllers
{
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _RoleManager;

        public RolesController(
            RoleManager<IdentityRole> RoleManager,
            ApplicationDbContext context)
        {
            _context = context;
            _RoleManager = RoleManager;
        }
        public IActionResult Index()
        {
            var roles = _context.Roles.ToList();
            return View(roles);
        }

        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public async Task<ActionResult> Create(Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole role)
        {
            try
            {
                //var role = new IdentityRole(collection["RoleName"]);
                await _RoleManager.CreateAsync(role);
                //_context.Roles.Add(role
                //_context.Roles.Add(new Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole()
                //{
                    //Name = collection["RoleName"]
                //});
                //_context.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string RoleName)
        {
            var thisRole = _context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            _context.Roles.Remove(thisRole);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Roles/Edit/5
        public ActionResult Edit(string roleName)
        {
            var thisRole = _context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(thisRole);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole role)
        {
            try
            {
                _context.Entry(role).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}