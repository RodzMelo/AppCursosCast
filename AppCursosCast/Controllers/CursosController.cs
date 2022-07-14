using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppCursosCast.Models;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace AppCursosCast.Controllers
{
    public class CursosController : Controller
    {
        private readonly CursosContext _context;
        private readonly INotyfService _toastNotification;

        public CursosController(CursosContext context, INotyfService toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }

        // GET: Cursos
        public async Task<IActionResult> Index()
        {
            var cursosContext = _context.Curso.Include(c => c.Categoria);
            return View(await cursosContext.ToListAsync());
        }

        // GET: Cursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Curso == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso
                .Include(c => c.Categoria)
                .FirstOrDefaultAsync(m => m.CursoId == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // GET: Cursos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Nome");
            return View();
        }

        // POST: Cursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CursoId,Descricao,DataInicio,DataFim,QuantidadeEstudantes,Ativo,CategoriaId")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(curso);
                
                await _context.SaveChangesAsync();
                _context.Log.Add(new Log { Usuario = "Admin", Acao = "Inserir", CursoId = curso.CursoId, DataInclusao = DateTime.Now });
                await _context.SaveChangesAsync();
                _toastNotification.Success("Curso cadastrado com sucesso!");
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Nome", curso.CategoriaId);
            return View(curso);
        }
        
        // GET: Cursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Curso == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Nome", curso.CategoriaId);
            return View(curso);
        }

        // POST: Cursos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CursoId,Descricao,DataInicio,DataFim,QuantidadeEstudantes,Ativo,CategoriaId")] Curso curso)
        {
            if (id != curso.CursoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curso);
                    await _context.SaveChangesAsync();

                    _context.Log.Where(l => l.CursoId == curso.CursoId).ToList().ForEach(l =>
                    {
                        l.DataAtualizacao = DateTime.Now;
                    });
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists(curso.CursoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _toastNotification.Information("Curso Editado com sucesso");
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Nome", curso.CategoriaId);
            return View(curso);
        }

        // GET: Cursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Curso == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso
                .Include(c => c.Categoria)
                .FirstOrDefaultAsync(m => m.CursoId == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // POST: Cursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Curso == null)
            {
                return Problem("Entity set 'CursosContext.Curso'  is null.");
            }
            var curso = await _context.Curso.FindAsync(id);
            if (curso != null && DateTime.Now < curso.DataFim)
            {
                _context.Curso.Remove(curso);
                _toastNotification.Error("Curso deletado com sucesso!");
                await _context.SaveChangesAsync();
            }
            else
                _toastNotification.Warning("Esse curso já foi realizado. Impossivel excluir!");
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursoExists(int id)
        {
          return (_context.Curso?.Any(e => e.CursoId == id)).GetValueOrDefault();
        }
    }
}
