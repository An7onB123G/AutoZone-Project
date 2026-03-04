using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoZone.Data;
using AutoZone.Models;

namespace AutoZone.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vehicles.Include(v => v.BrandModels).Include(v => v.CarTypes).Include(v => v.EngineTypes);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.BrandModels)
                .Include(v => v.CarTypes)
                .Include(v => v.EngineTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            ViewData["BrandModelId"] = new SelectList(_context.BrandModels, "Id", "Id");
            ViewData["CarTypeId"] = new SelectList(_context.CarTypes, "Id", "Id");
            ViewData["EngineTypeId"] = new SelectList(_context.EngineTypes, "Id", "Id");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CatalogNumber,BrandModelId,CarTypeId,EngineTypeId,Description,Country,Year,IsAutomatic,Mileage,Power,ImageURL,Price,RegisterOn")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandModelId"] = new SelectList(_context.BrandModels, "Id", "Id", vehicle.BrandModelId);
            ViewData["CarTypeId"] = new SelectList(_context.CarTypes, "Id", "Id", vehicle.CarTypeId);
            ViewData["EngineTypeId"] = new SelectList(_context.EngineTypes, "Id", "Id", vehicle.EngineTypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["BrandModelId"] = new SelectList(_context.BrandModels, "Id", "Id", vehicle.BrandModelId);
            ViewData["CarTypeId"] = new SelectList(_context.CarTypes, "Id", "Id", vehicle.CarTypeId);
            ViewData["EngineTypeId"] = new SelectList(_context.EngineTypes, "Id", "Id", vehicle.EngineTypeId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CatalogNumber,BrandModelId,CarTypeId,EngineTypeId,Description,Country,Year,IsAutomatic,Mileage,Power,ImageURL,Price,RegisterOn")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
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
            ViewData["BrandModelId"] = new SelectList(_context.BrandModels, "Id", "Id", vehicle.BrandModelId);
            ViewData["CarTypeId"] = new SelectList(_context.CarTypes, "Id", "Id", vehicle.CarTypeId);
            ViewData["EngineTypeId"] = new SelectList(_context.EngineTypes, "Id", "Id", vehicle.EngineTypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.BrandModels)
                .Include(v => v.CarTypes)
                .Include(v => v.EngineTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
    }
}
