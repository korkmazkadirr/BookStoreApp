using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;
using BookStoreApp.Data;
using BookStoreApp.Models;

namespace BookStoreApp.Controllers
{
    public class BookCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var bookNames = _context.Books.Select(b => b.BookName).Distinct().ToList();
            ViewBag.BookNames = bookNames;

            return View();

        }
        [HttpPost]
        public IActionResult FilterByName(string bookName)
        {
            var filteredBookCategories = _context.BookCategories
                .Include(bc => bc.BookName)
                .Include(bc => bc.CategoryName)
                .Include(bc => bc.SubCategoryName)
                .Where(bc => bc.BookName.BookName == bookName)
                .ToList();

            return View("Index", filteredBookCategories);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookCategories == null)
            {
                return NotFound();
            }

            var bookCategory = await _context.BookCategories
                .Include(b => b.BookName)
                .Include(b => b.CategoryName)
                .Include(b => b.SubCategoryName)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (bookCategory == null)
            {
                return NotFound();
            }

            return View(bookCategory);
        }

        public IActionResult CreateCategory()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookName");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory([Bind("BookId,CategoryId")] BookCategory bookCategory)
        {
            if (ModelState.IsValid)
            {
                ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookName", bookCategory.BookId);
                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", bookCategory.CategoryId);
                return View(bookCategory);

            }
            var existingBookCategory = _context.BookCategories.FirstOrDefault(bc => bc.BookId == bookCategory.BookId
                                                                                && bc.CategoryId == bookCategory.CategoryId);

            if (existingBookCategory != null)
            {
                ModelState.AddModelError("", "This category of book already exists.");
                ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookName", bookCategory.BookId);
                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", bookCategory.CategoryId);
                return View(bookCategory);
            }

            _context.Add(bookCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult CreateSubCategory()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookName");
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategories, "SubCategoryId", "SubCategoryName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubCategory([Bind("BookId,SubCategoryId")] BookCategory bookCategory)
        {
            if (ModelState.IsValid)
            {
                ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookName", bookCategory.BookId);
                ViewData["SubCategoryId"] = new SelectList(_context.SubCategories, "SubCategoryId", "SubCategoryName", bookCategory.SubCategoryId);
                return View(bookCategory);

            }
            var existingBookCategory = _context.BookCategories.FirstOrDefault(bc => bc.BookId == bookCategory.BookId
                                                                                && bc.SubCategoryId == bookCategory.SubCategoryId);

            if (existingBookCategory != null)
            {
                ModelState.AddModelError("", "This subcategory of book already exists.");
                ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookName", bookCategory.BookId);
                ViewData["SubCategoryId"] = new SelectList(_context.SubCategories, "SubCategoryId", "SubCategoryName", bookCategory.SubCategoryId);
                return View(bookCategory);
            }

            _context.Add(bookCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookCategories == null)
            {
                return NotFound();
            }

            var bookCategory = await _context.BookCategories.FindAsync(id);
            if (bookCategory == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", bookCategory.BookId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", bookCategory.CategoryId);
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategories, "SubCategoryId", "SubCategoryId", bookCategory.SubCategoryId);
            return View(bookCategory);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,CategoryId,SubCategoryId")] BookCategory bookCategory)
        {
            if (id != bookCategory.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookCategoryExists(bookCategory.BookId))
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
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", bookCategory.BookId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", bookCategory.CategoryId);
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategories, "SubCategoryId", "SubCategoryId", bookCategory.SubCategoryId);
            return View(bookCategory);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookCategories == null)
            {
                return NotFound();
            }

            var bookCategory = await _context.BookCategories
                .Include(b => b.BookName)
                .Include(b => b.CategoryName)
                .Include(b => b.SubCategoryName)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (bookCategory == null)
            {
                return NotFound();
            }

            return View(bookCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookCategories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BookCategories'  is null.");
            }
            var bookCategory = await _context.BookCategories.FindAsync(id);
            if (bookCategory != null)
            {
                _context.BookCategories.Remove(bookCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookCategoryExists(int id)
        {
            return (_context.BookCategories?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
