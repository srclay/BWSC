using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BWSC.Data;
using BWSC.Models;
using Microsoft.AspNetCore.Authorization;

namespace BWSC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SwimmersController : Controller 
    {
        private readonly SwimmingClubContext _context;

        public SwimmersController(SwimmingClubContext context)
        {
            _context = context;    
        }

        // GET: Swimmers
        public async Task<IActionResult> Index()
        {
            var swimmers = _context.Swimmers
                .Include(s => s.Squad)
                .AsNoTracking();
            return View(await swimmers.ToListAsync());
        }

        // GET: Swimmers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swimmer = await _context.Swimmers
                .Include(sq => sq.Squad)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (swimmer == null)
            {
                return NotFound();
            }

            return View(swimmer);
        }

        // GET: Swimmers/Create
        public IActionResult Create()
        {
            PopulateSquadsDropDownList();
            //ViewData["SquadID"] = new SelectList(_context.Squads, "ID", "ID");
            return View();
        }

        // POST: Swimmers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ASANumber,DOB,FirstName,SquadID,StartDate,Surname,photo")] Swimmer swimmer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(swimmer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            PopulateSquadsDropDownList(swimmer.SquadID);
            //ViewData["SquadID"] = new SelectList(_context.Squads, "ID", "ID", swimmer.SquadID);
            return View(swimmer);
        }

        // GET: Swimmers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swimmer = await _context.Swimmers
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (swimmer == null)
            {
                return NotFound();
            }
            PopulateSquadsDropDownList(swimmer.SquadID);
            //ViewData["SquadID"] = new SelectList(_context.Squads, "ID", "ID", swimmer.SquadID);
            return View(swimmer);
        }

        // POST: Swimmers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ASANumber,DOB,FirstName,SquadID,StartDate,Surname,photo")] Swimmer swimmer)
        {
            if (id != swimmer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(swimmer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SwimmerExists(swimmer.ID))
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
            PopulateSquadsDropDownList(swimmer.SquadID);
            //ViewData["SquadID"] = new SelectList(_context.Squads, "ID", "ID", swimmer.SquadID);
            return View(swimmer);
        }

        private void PopulateSquadsDropDownList(object selectedSquad = null)
        {
            var squadsQuery = from sq in _context.Squads
                              orderby sq.Name
                              select sq;
            ViewBag.SquadID = new SelectList(squadsQuery.AsNoTracking(), "ID", "Name", selectedSquad);
        }

        // GET: Swimmers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swimmer = await _context.Swimmers.SingleOrDefaultAsync(m => m.ID == id);
            if (swimmer == null)
            {
                return NotFound();
            }

            return View(swimmer);
        }

        // POST: Swimmers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var swimmer = await _context.Swimmers.SingleOrDefaultAsync(m => m.ID == id);
            _context.Swimmers.Remove(swimmer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SwimmerExists(int id)
        {
            return _context.Swimmers
                .Include(sq => sq.Squad)
                .AsNoTracking()
                .Any(e => e.ID == id);
        }
    }
}
