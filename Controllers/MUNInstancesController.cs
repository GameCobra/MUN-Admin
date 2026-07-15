using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MUNAdmin.Data;
using MUNAdmin.Models;

namespace MUNAdmin.Controllers
{
    public class MUNInstancesController : Controller
    {
        private readonly MUNAdminContext _context;

        public MUNInstancesController(MUNAdminContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.MUNInstance.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mUNInstance = await _context.MUNInstance
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mUNInstance == null)
            {
                return NotFound();
            }

            return View(mUNInstance);
        }

        // GET: MUNInstances/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AdminUsername,AdminPassword,MUNTitle,MUNAccessCode")] MUNInstance mUNInstance)
        {
            Debug.WriteLine(ModelState.ToList()[0]);
            if (ModelState.IsValid)
            {
                _context.Add(mUNInstance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mUNInstance);
        }

        // GET: MUNInstances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mUNInstance = await _context.MUNInstance.FindAsync(id);
            if (mUNInstance == null)
            {
                return NotFound();
            }
            return View(mUNInstance);
        }

        // POST: MUNInstances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AdminUsername,AdminPassword,MUNTitle,MUNAccessCode")] MUNInstance mUNInstance)
        {
            if (id != mUNInstance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mUNInstance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MUNInstanceExists(mUNInstance.Id))
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
            return View(mUNInstance);
        }

        // GET: MUNInstances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mUNInstance = await _context.MUNInstance
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mUNInstance == null)
            {
                return NotFound();
            }

            return View(mUNInstance);
        }

        // POST: MUNInstances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mUNInstance = await _context.MUNInstance.FindAsync(id);
            if (mUNInstance != null)
            {
                _context.MUNInstance.Remove(mUNInstance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MUNInstanceExists(int id)
        {
            return _context.MUNInstance.Any(e => e.Id == id);
        }
    }
}
