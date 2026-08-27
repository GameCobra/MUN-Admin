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
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Index()
        {

            if (!_context.MUNInstance.Any())
            {
                CouncilInformation council = new CouncilInformation
                {
                    CouncilName = "Security",
                    PrimaryColor = "4d92b3",
                    SecondaryColor = "bdeaff",
                    Resolutions = new List<Resolution>
                    {
                        new Resolution {Title = "T1", BodyText = "B1", Code = "C1"},
                        new Resolution {Title = "T2", BodyText = "B2", Code = "C2"},
                        new Resolution {Title = "T3", BodyText = "B3", Code = "C3"},
                        new Resolution {Title = "T4", BodyText = "B4", Code = "C4"}
                    }
                };

                CouncilInformation council2 = new CouncilInformation
                {
                    CouncilName = "ECO",
                    PrimaryColor = "",
                    SecondaryColor = "",
                    Resolutions = new List<Resolution>
                    {
                        new Resolution {Title = "T11", BodyText = "New Resolution", Code = "C"},
                    }
                };

                _context.MUNInstance.Add(new MUNInstance
                {
                    AdminUsername = "ADMIN",
                    AdminPassword = "ADMINPASSWORD",
                    MUNTitle = "TEST MUN",
                    MUNAccessCode = 1,
                    CouncilInformationList = new List<CouncilInformation> { council, council2 },
                    DelegationList = new List<DelegationInstance> 
                    { 
                        new DelegationInstance
                        {
                            DelegationCountry = "Canada",
                            DelegationAccsesCode = 1,
                            CouncilList = new List<DelegationCouncil> 
                            { 
                                new DelegationCouncil { Council = council, RequestedRebuttal = false},
                                new DelegationCouncil { Council = council2, RequestedRebuttal = false}
                            }
                        } 
                    }
                });
                await _context.SaveChangesAsync();
            }
            return View(await _context.MUNInstance.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Could not find the specified ID";
                return RedirectToAction("Error", "Home");

            }

            var mUNInstance = await _context.MUNInstance
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mUNInstance == null)
            {
                TempData["Error"] = "Could not find the specified MUN";
                return RedirectToAction("Error", "Home");
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
                TempData["Error"] = "Could not find the specified ID";
                return RedirectToAction("Error", "Home");
            }

            var mUNInstance = await _context.MUNInstance.FindAsync(id);
            if (mUNInstance == null)
            {
                TempData["Error"] = "Could not find the specified MUN";
                return RedirectToAction("Error", "Home");
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
                TempData["Error"] = "Could not find the specified ID";
                return RedirectToAction("Error", "Home");
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
                        TempData["Error"] = "Could not find the specified MUN";
                        return RedirectToAction("Error", "Home");
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
                TempData["Error"] = "Could not find the specified ID";
                return RedirectToAction("Error", "Home");
            }

            var mUNInstance = await _context.MUNInstance
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mUNInstance == null)
            {
                TempData["Error"] = "Could not find the specified MUN";
                return RedirectToAction("Error", "Home");
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
                for (int i = 0; i < mUNInstance.DelegationList.Count; i++)
                {
                    for (int j = 0; j < mUNInstance.DelegationList[i].CouncilList.Count; j++)
                    {
                        _context.Remove(mUNInstance.DelegationList[i].CouncilList[j]);
                    }
                }

                for (int i = 0; i < mUNInstance.CouncilInformationList.Count; i++)
                {
                    _context.Remove(mUNInstance.CouncilInformationList[i]);
                }

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
