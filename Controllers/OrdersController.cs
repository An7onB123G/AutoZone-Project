using AutoZone.Data;
using AutoZone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoZone.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Client> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<Client> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders
                .Include(o => o.Clients)
                .Include(o => o.Vehicle)
                    .ThenInclude(v => v.BrandModels)
                .AsQueryable();

            if (!User.IsInRole("Admin"))
            {
                var userId = _userManager.GetUserId(User);
                applicationDbContext = applicationDbContext.Where(o => o.ClientId == userId);
            }

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Clients)
                .Include(o => o.Vehicle)
                    .ThenInclude(v => v.BrandModels)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            if (!User.IsInRole("Admin") && order.ClientId != _userManager.GetUserId(User))
            {
                return Forbid();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create(int? vehicleId)
        {
            //ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id");

            PopulateVehicleSelectList(vehicleId);
            return View(new Order { VehicleId = vehicleId ?? 0 });
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleId")] Order order)
        {

            order.ClientId = _userManager.GetUserId(User)!;
            order.OrderDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id", order.ClientId);
            PopulateVehicleSelectList(order.VehicleId);
            return View(order);
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id", order.ClientId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id", order.VehicleId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,VehicleId,OrderDate")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id", order.ClientId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id", order.VehicleId);
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Clients)
                .Include(o => o.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        private void PopulateVehicleSelectList(int? selectedVehicleId = null)
        {
            var vehicles = _context.Vehicles
                .Include(v => v.BrandModels)
                .OrderBy(v => v.BrandModels.Brand)
                .ThenBy(v => v.BrandModels.Model)
                .Select(v => new
                {
                    v.Id,
                    Name = v.BrandModels.Brand + " " + v.BrandModels.Model + " - " + v.Year
                })
                .ToList();

            ViewData["VehicleId"] = new SelectList(vehicles, "Id", "Name", selectedVehicleId);
        }
    }
}
