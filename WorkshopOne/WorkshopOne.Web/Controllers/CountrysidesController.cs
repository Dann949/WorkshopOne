using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkshopOne.Common.Entities;
using WorkshopOne.Web.Data;

namespace WorkshopOne.Web.Controllers
{
    public class CountrysidesController : Controller
    {
        private readonly DataContext _context;

        public CountrysidesController(DataContext context)
        {
            _context = context;
        }

        // GET: Countrysides
        public async Task<IActionResult> Index()
        {
            return View(await _context.countrysides
                .Include(D=>D.Districts)
                .ToListAsync());
        }

        // GET: Countrysides/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
                Countryside  countryside  = await _context.countrysides
                .Include(D=>D.Districts)
                .ThenInclude(C=>C.Churches)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (countryside == null)
            {
                return NotFound();
            }

            return View(countryside);
        }

  
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Countryside countryside)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(countryside);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }


            }
            return View(countryside);
        }

        // GET: Countrysides/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryside = await _context.countrysides.FindAsync(id);
            if (countryside == null)
            {
                return NotFound();
            }
            return View(countryside);
        }

        // POST: Countrysides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Countryside countryside)
        {
            if (id != countryside.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(countryside);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(countryside);
        }



        // GET: Countrysides/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

               Countryside countryside = await _context.countrysides
                 .Include(d => d.Districts)
                 .ThenInclude(c => c.Churches)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (countryside == null)
            {
                return NotFound();
            }

          
           _context.countrysides.Remove(countryside);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountrysideExists(int id)
        {
            return _context.countrysides.Any(e => e.Id == id);
        }


        public async Task<IActionResult> AddDistrict(int? id)
        {
            if(id== null)
            {
                return NotFound();
            }

            Countryside countryside = await _context.countrysides.FindAsync(id);
                if(countryside == null)
            {
                return NotFound();
            }

            District model = new District { IdCountryside = countryside.Id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDistrict( District district)
        {
            if (ModelState.IsValid)
            {
                Countryside countryside = await _context.countrysides
                    .Include(c => c.Districts)
                    .FirstOrDefaultAsync(c => c.Id == district.Id);
                if (countryside == null)
                {
                    return NotFound();
                }

                try
                {
                    district.Id = 0;
                    countryside.Districts.Add(district);
                    _context.Update(countryside);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(Details)}/{countryside.Id}");

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(district);
        }


        public async Task<IActionResult> EditDistrict(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           District district = await _context.districts.FindAsync(id);
            if (district == null)
            {
                return NotFound();
            }

         Countryside countryside = await _context.countrysides.FirstOrDefaultAsync(c => c.Districts.FirstOrDefault(d => d.Id == district.Id) != null);
            district.IdCountryside = countryside.Id;
            return View(district);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDistrict(District  district)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(district);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(Details)}/{district.IdCountryside}");

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(district);
        }

        public async Task<IActionResult> DeleteDistrict(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

          District district = await _context.districts
                .Include(d => d.Churches)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (district == null)
            {
                return NotFound();
            }

          Countryside countryside = await _context.countrysides.FirstOrDefaultAsync(c => c.Districts.FirstOrDefault(d => d.Id == district.Id) != null);
            _context.districts.Remove(district);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(Details)}/{countryside.Id}");
        }


        public async Task<IActionResult> DetailsDistrict(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           District district = await _context.districts
                .Include(c =>c.Churches )
                .FirstOrDefaultAsync(m => m.Id == id);
            if (district == null)
            {
                return NotFound();
            }

          Countryside countryside = await _context.countrysides.FirstOrDefaultAsync(d => d.Districts.FirstOrDefault(c => c.Id == district.Id) != null);
          district.IdCountryside = countryside.Id;
            return View(district);
        }

        public async Task<IActionResult> AddChurch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           District district = await _context.districts.FindAsync(id);
            if (district == null)
            {
                return NotFound();
            }

            church model = new church { IdDistrict = district.Id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddChurch(church church)
        {
            if (ModelState.IsValid)
            {
               District district = await _context.districts
                    .Include(d => d.Churches)
                    .FirstOrDefaultAsync(c => c.Id == church.IdDistrict);
                if (district == null)
                {
                    return NotFound();
                }

                try
                {
                   church.Id = 0;
                    district.Churches.Add(church);
                    _context.Update(district);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(DetailsDistrict)}/{district.Id}");

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(church);
        }

        public async Task<IActionResult> EditChurch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            church church = await _context.churches.FindAsync(id);
            if ( church == null)
            {
                return NotFound();
            }

            District district = await _context.districts.FirstOrDefaultAsync(d => d.Churches.FirstOrDefault(c => c.Id == church.Id) != null);
            church.IdDistrict = district.Id;
            return View(church);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditChurch(church church)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(church);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(DetailsDistrict)}/{church.IdDistrict}");

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(church);
        }

        public async Task<IActionResult> DeleteChurch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            church church = await _context.churches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (church == null)
            {
                return NotFound();
            }

            District district = await _context.districts.FirstOrDefaultAsync(d => d.Churches.FirstOrDefault(c => c.Id == church.Id) != null);
            _context.churches.Remove(church);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(DetailsDistrict)}/{district.Id}");
        }



    }
}
