using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BWSC.Data;
using BWSC.Models;

namespace WebApplication4.Controllers
{
    public class CoachesController : Controller
    {
        private readonly SwimmingClubContext _context;

        public CoachesController(SwimmingClubContext context)
        {
            _context = context;    
        }

        // GET: Coaches
        public async Task<IActionResult> Index()
        {
            var swimmingClubContext = _context.Coaches
                .Include(c => c.Squad)
                .AsNoTracking();
            return View(await swimmingClubContext.ToListAsync());
        }

        // GET: Coaches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coaches
                .Include(sq => sq.Squad)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (coach == null)
            {
                return NotFound();
            }

            return View(coach);
        }

        // GET: Coaches/Create
        public IActionResult Create()
        {
            PopulateSquadsDropDownList();
            return View();
        }

        // POST: Coaches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,SquadID,StartDate,Surname")] Coach coach)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coach);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            PopulateSquadsDropDownList(coach.SquadID);
            return View(coach);
        }

        // GET: Coaches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coaches
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (coach == null)
            {
                return NotFound();
            }
            PopulateSquadsDropDownList(coach.SquadID);
            return View(coach);
        }

        // POST: Coaches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,SquadID,StartDate,Surname")] Coach coach)
        {
            if (id != coach.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoachExists(coach.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            PopulateSquadsDropDownList(coach.SquadID);
            return View(coach);
        }

        private void PopulateSquadsDropDownList(object selectedSquad = null)
        {
            var squadsQuery = from sq in _context.Squads
                              orderby sq.Name
                              select sq;
            ViewBag.SquadID = new SelectList(squadsQuery.AsNoTracking(), "ID", "Name", selectedSquad);
        }

        // GET: Coaches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coaches.SingleOrDefaultAsync(m => m.ID == id);
            if (coach == null)
            {
                return NotFound();
            }

            return View(coach);
        }

        // POST: Coaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coach = await _context.Coaches.SingleOrDefaultAsync(m => m.ID == id);
            _context.Coaches.Remove(coach);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CoachExists(int id)
        {
            return _context.Coaches.Any(e => e.ID == id);
        }
    }
}
