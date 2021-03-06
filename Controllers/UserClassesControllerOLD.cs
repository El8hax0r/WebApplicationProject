﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using WebApplication1;
using System.ComponentModel.DataAnnotations;

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
            var currentUser = HttpContext.User.Identity.Name; //this is current user Email
            //var currentId = _context.AspNetUsers.Include(u => u.Email).Include(u => u.Id)
                //.Where(u => u.Email == currentUser); //this is user GUID
            //var webApplication1Context = _context.UserClass.Include(u => u.Class).Include(u => u.IdNavigation)
                //.Where(u => u.IdNavigation.Email == currentUser); // //taking this out for testing issue //no issue
            var nameQuery = _context.UserClass.Include(u => u.Class).Include(u => u.IdNavigation)
                .Where(u => u.IdNavigation.UserName == User.Identity.Name);
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
                .FirstOrDefaultAsync(m => m.ClassId == id);
            if (userClass == null)
            {
                return NotFound();
            }

            return View(userClass);
        }

        // GET: UserClasses/Create
        public IActionResult Create()
        {
            var currentUser = HttpContext.User.Identity.Name; //this is current user Email
            //var nameQuery = _context.UserClass.Include(u => u.Class).Include(u => u.IdNavigation.UserName)
                //.Where(u => u.IdNavigation.UserName == User.Identity.Name); 
            ViewData["ClassId"] = new SelectList(_context.Class, "ClassId", "ClassName"); 
            ViewData["Id"] = new SelectList(_context.AspNetUsers, "Id", "Id"); //UserName
            return View();
        }

        // POST: UserClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassId,Id")] UserClass userClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Class, "ClassId", "ClassName", userClass.ClassId); //change this back later?
            ViewData["Id"] = new SelectList(_context.AspNetUsers, "Id", "Id", userClass.Id);
            ViewData["UserName"] = new SelectList(_context.AspNetUsers, "UserName", "UserName", userClass.IdNavigation.UserName);
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
            ViewData["ClassId"] = new SelectList(_context.Class, "ClassId", "ClassName", userClass.ClassId); //change....
            ViewData["Id"] = new SelectList(_context.AspNetUsers, "Id", "Id", userClass.Id);
            return View(userClass);
        }

        // POST: UserClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassId,Id")] UserClass userClass)
        {
            if (id != userClass.ClassId)
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
                    if (!UserClassExists(userClass.ClassId))
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
            ViewData["ClassId"] = new SelectList(_context.Class, "ClassId", "ClassName", userClass.ClassId);
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
                .FirstOrDefaultAsync(m => m.ClassId == id);
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
            return _context.UserClass.Any(e => e.ClassId == id);
        }
    }
}
