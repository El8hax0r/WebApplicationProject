using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class UserClassesController : Controller
    {
        private readonly WebApplication1Context _context;

        public UserClassesController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: UserClasses
        public async Task<IActionResult> Index()
        {
            var name = HttpContext.User.Identity.Name;
            //var webApplication1Context = _context.UserClass.Include(u => u.Class).Include(u => u.IdNavigation);
            var nameQuery = _context.UserClass.Include(u => u.Class).Include(u => u.IdNavigation)
                .Where(u => u.IdNavigation.Email == name);
            return View(await nameQuery.ToListAsync());
        }

        // GET: UserClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userClass = await _context.UserClass
                .Include(u => u.Class)
                .Include(u => u.IdNavigation)
                .FirstOrDefaultAsync(m => m.UserClassId == id);
            if (userClass == null)
            {
                return NotFound();
            }

            return View(userClass);
        }

        // GET: UserClasses/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Class, "ClassId", "ClassName");
            ViewData["Id"] = new SelectList(_context.AspNetUsers.Where(u => u.UserName == HttpContext.User.Identity.Name), "Id", "Id");
            return View();
        }

        // POST: UserClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserClassId,ClassId,Id")] UserClass userClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Class, "ClassId", "ClassId", userClass.ClassId);
            ViewData["Id"] = new SelectList(_context.AspNetUsers, "Id", "Id", userClass.Id);
            return View(userClass);
        }

        // GET: UserClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userClass = await _context.UserClass.FindAsync(id);
            if (userClass == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Class, "ClassId", "ClassDescription", userClass.ClassId);
            ViewData["Id"] = new SelectList(_context.AspNetUsers, "Id", "Id", userClass.Id);
            return View(userClass);
        }

        // POST: UserClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserClassId,ClassId,Id")] UserClass userClass)
        {
            if (id != userClass.UserClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserClassExists(userClass.UserClassId))
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
            ViewData["ClassId"] = new SelectList(_context.Class, "ClassId", "ClassDescription", userClass.ClassId);
            ViewData["Id"] = new SelectList(_context.AspNetUsers, "Id", "Id", userClass.Id);
            return View(userClass);
        }

        // GET: UserClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userClass = await _context.UserClass
                .Include(u => u.Class)
                .Include(u => u.IdNavigation)
                .FirstOrDefaultAsync(m => m.UserClassId == id);
            if (userClass == null)
            {
                return NotFound();
            }

            return View(userClass);
        }

        // POST: UserClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userClass = await _context.UserClass.FindAsync(id);
            _context.UserClass.Remove(userClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserClassExists(int id)
        {
            return _context.UserClass.Any(e => e.UserClassId == id);
        }
    }
}
