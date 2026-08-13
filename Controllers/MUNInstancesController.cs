using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MUNAdmin.Data;
using MUNAdmin.Models;
using MUNAdmin.Models.LoginModels;
using MUNAdmin.Services;
using NuGet.Protocol;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MUNAdmin.Controllers
{
    public class MUNInstancesController : Controller
    {
        private readonly MUNAdminContext _context;
        private readonly UserServices _userServices;

        public MUNInstancesController(MUNAdminContext context, UserServices userServices)
        {
            _context = context;
            _userServices = userServices;
        }

        //[Authorize(Policy = "IsAdminOfMUN")]
        [Authorize]
        public async Task<IActionResult> Index()
        {

            if (!_context.MUNInstance.Any())
            {
                CouncilInformation council = new CouncilInformation
                {
                    CouncilName = "Security",
                    PrimaryColor = "4d92b3",
                    SecondaryColor = "bdeaff"
                };

                _context.MUNInstance.Add(new MUNInstance
                {
                    AdminUsername = "ADMIN",
                    AdminPassword = "ADMINPASSWORD",
                    MUNTitle = "TEST MUN",
                    CouncilInformationList = new List<CouncilInformation> { council },
                    DelegationList = new List<DelegationInstance> { new DelegationInstance
                    {
                        DelegationCountry = "Canada",
                        CouncilList = new List<DelegationCouncil> { new DelegationCouncil
                        {
                            Council = council,
                            RequestedRebuttal = false
                        } }
                    } }
                });
                await _context.SaveChangesAsync();
            }
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
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,AdminUsername,AdminPassword,MUNTitle,MUNAccessCode")] MUNInstance mUNInstance)
        {
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
