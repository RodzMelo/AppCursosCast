using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppCursosCast.Models;

namespace AppCursosCast.Controllers
{
    public class LogsController : Controller
    {
        private readonly CursosContext _context;

        public LogsController(CursosContext context)
        {
            _context = context;
        }

        // GET: Logs
        public async Task<IActionResult> Index()
        {
            var cursosContext = _context.Log.Include(l => l.Curso);
            return View(await cursosContext.ToListAsync());
        }

        // GET: Logs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Log == null)
            {
                return NotFound();
            }

            var log = await _context.Log
                .Include(l => l.Curso)
                .FirstOrDefaultAsync(m => m.LogId == id);
            if (log == null)
            {
                return NotFound();
            }

            return View(log);
        }

        // GET: Logs/Create
        public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.Curso, "CursoId", "Descricao");
            return View();
        }

        // POST: Logs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LogId,Usuario,Acao,CursoId,DataInclusao,DataAtualizacao")] Log log)
        {
            if (ModelState.IsValid)
            {
                _context.Add(log);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Curso, "CursoId", "Descricao", log.CursoId);
            return View(log);
        }

        // GET: Logs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Log == null)
            {
                return NotFound();
            }

            var log = await _context.Log.FindAsync(id);
            if (log == null)
            {
                return NotFound();
            }
            ViewData["CursoId"] = new SelectList(_context.Curso, "CursoId", "Descricao", log.CursoId);
            return View(log);
        }

        // POST: Logs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LogId,Usuario,Acao,CursoId,DataInclusao,DataAtualizacao")] Log log)
        {
            if (id != log.LogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(log);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogExists(log.LogId))
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
            ViewData["CursoId"] = new SelectList(_context.Curso, "CursoId", "Descricao", log.CursoId);
            return View(log);
        }

        // GET: Logs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Log == null)
            {
                return NotFound();
            }

            var log = await _context.Log
                .Include(l => l.Curso)
                .FirstOrDefaultAsync(m => m.LogId == id);
            if (log == null)
            {
                return NotFound();
            }

            return View(log);
        }

        // POST: Logs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Log == null)
            {
                return Problem("Entity set 'CursosContext.Log'  is null.");
            }
            var log = await _context.Log.FindAsync(id);
            if (log != null)
            {
                _context.Log.Remove(log);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogExists(int id)
        {
          return (_context.Log?.Any(e => e.LogId == id)).GetValueOrDefault();
        }
    }
}
