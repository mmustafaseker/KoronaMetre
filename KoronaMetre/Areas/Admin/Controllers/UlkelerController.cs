﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoronaMetre.Models;

namespace KoronaMetre.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UlkelerController : Controller
    {
        private readonly KoronaContextDb _context;

        public UlkelerController(KoronaContextDb context)
        {
            _context = context;
        }

        // GET: Admin/Ulkeler
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ulkeler.ToListAsync());
        }

        // GET: Admin/Ulkeler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ulke = await _context.Ulkeler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ulke == null)
            {
                return NotFound();
            }

            return View(ulke);
        }

        // GET: Admin/Ulkeler/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Ulkeler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad,Nufus")] Ulke ulke)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ulke);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ulke);
        }

        // GET: Admin/Ulkeler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ulke = await _context.Ulkeler.FindAsync(id);
            if (ulke == null)
            {
                return NotFound();
            }
            return View(ulke);
        }

        // POST: Admin/Ulkeler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Nufus")] Ulke ulke)
        {
            if (id != ulke.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ulke);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UlkeExists(ulke.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ulke);
        }

        // GET: Admin/Ulkeler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ulke = await _context.Ulkeler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ulke == null)
            {
                return NotFound();
            }

            return View(ulke);
        }

        // POST: Admin/Ulkeler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ulke = await _context.Ulkeler.FindAsync(id);
            _context.Ulkeler.Remove(ulke);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UlkeExists(int id)
        {
            return _context.Ulkeler.Any(e => e.Id == id);
        }
    }
}
