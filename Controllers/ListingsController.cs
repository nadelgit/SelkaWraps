using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SelkaWraps.Data;
using SelkaWraps.Models.Listings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SelkaWraps.Controllers
{
    public class ListingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ListingsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Listings
        public async Task<IActionResult> Index()
        {
            var data = await _context.Listings.ToListAsync();
            //var viewdata = data.Select(q => new IndexVM
            //{
            //    Id = q.Id,
            //    Category = q.Category,
            //    CreatedAt = q.CreatedAt,
            //    Description = q.Description,
            //    Title = q.Title,
            //    IsActive = q.IsActive,
            //    Price = q.Price,
            //    Quantity = q.Quantity,
            //    Material = q.Material,
            //    ImageUrl = q.ImageUrl,
            //    UpdatedAt = q.UpdatedAt,
            //    Orders = q.Orders


            //});

            var viewdata = _mapper.Map<List<ListingsReadOnlyVM>>(data);
            return View(viewdata);
        }

        // GET: Listings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = await _context.Listings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listing == null)
            {
                return NotFound();
            }

            var viewdata = _mapper.Map<ListingsReadOnlyVM>(listing);

            return View(viewdata);
        }

        // GET: Listings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Listings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ListingsCreateVM listingCreateVM)
        {
            if (ModelState.IsValid)
            {
                var listing = _mapper.Map<Listing>(listingCreateVM);
                _context.Add(listing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(listingCreateVM);
        }

        // GET: Listings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = await _context.Listings.FindAsync(id);
            var viewdata = _mapper.Map<ListingsEditVM>(listing);
            if (listing == null)
            {
                return NotFound();
            }
            return View(viewdata);
        }

        // POST: Listings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ListingsEditVM listingsEdit)
        {
            if (id != listingsEdit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var listing = _mapper.Map<Listing>(listingsEdit);
                    _context.Update(listing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListingExists(listingsEdit.Id))
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
            return View(listingsEdit);
        }

        // GET: Listings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = await _context.Listings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listing == null)
            {
                return NotFound();
            }
            var viewdata = _mapper.Map<ListingsReadOnlyVM>(listing);
            return View(viewdata);
        }

        // POST: Listings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listing = await _context.Listings.FindAsync(id);
            if (listing != null)
            {
                _context.Listings.Remove(listing);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListingExists(int id)
        {
            return _context.Listings.Any(e => e.Id == id);
        }
    }
}
