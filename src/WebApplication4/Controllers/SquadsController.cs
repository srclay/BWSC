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
    public class SquadsController : Controller
    {
        private readonly SwimmingClubContext _context;

        public SquadsController(SwimmingClubContext context)
        {
            _context = context;    
        }

        // GET: Squads
        public async Task<IActionResult> Index()
        {
            return View(await _context.Squads.ToListAsync());
        }

        // GET: Squads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var squad = await _context.Squads.SingleOrDefaultAsync(m => m.ID == id);
            if (squad == null)
            {
                return NotFound();
            }

            return View(squad);
        }

        // GET: Squads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Squads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Squad squad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(squad);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(squad);
        }

        // GET: Squads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var squad = await _context.Squads.SingleOrDefaultAsync(m => m.ID == id);
            if (squad == null)
            {
                return NotFound();
            }
            return View(squad);
        }

        // POST: Squads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] Squad squad)
        {
            if (id != squad.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(squad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SquadExists(squad.ID))
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
            return View(squad);
        }

        // GET: Squads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var squad = await _context.Squads.SingleOrDefaultAsync(m => m.ID == id);
            if (squad == null)
            {
                return NotFound();
            }

            return View(squad);
        }

        // POST: Squads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var squad = await _context.Squads.SingleOrDefaultAsync(m => m.ID == id);
            _context.Squads.Remove(squad);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SquadExists(int id)
        {
            return _context.Squads.Any(e => e.ID == id);
        }
    }
}
