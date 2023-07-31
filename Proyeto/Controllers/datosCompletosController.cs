using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyeto.Models;

namespace Proyeto.Controllers
{
    public class datosCompletosController : Controller
    {
        private readonly SisCadticContext _context;

        public datosCompletosController(SisCadticContext context)
        {
            _context = context;
        }

        // GET: datosCompletos
        public async Task<IActionResult> Index()
        {
            string userName = HttpContext.Session.GetString("usuario");
            int autorId  = Convert.ToInt32(HttpContext.Session.GetString("autor"));

            var sisCadticContext = _context.Autors.Include(a => a.IdNivelEstudios1Navigation).Where(x=>x.IdAutor== autorId);
            return View(sisCadticContext.FirstOrDefault());
        }

        // GET: datosCompletos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Autors == null)
            {
                return NotFound();
            }

            var autor = await _context.Autors
                .Include(a => a.IdNivelEstudios1Navigation)
                .FirstOrDefaultAsync(m => m.IdAutor == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // GET: datosCompletos/Create
        public IActionResult Create()
        {
            ViewData["IdNivelEstudios1"] = new SelectList(_context.NivelEstudios, "NivelEstudiosId", "NivelEstudiosId");
            return View();
        }

        // POST: datosCompletos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAutor,Nombre,ApePaterno,ApeMaterno,Matricula,NumEmpleado,TipoCuenta,NumTelefono,FechaNaci,CuerpoAcademico,AreaEstudios,IdNivelEstudios1")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdNivelEstudios1"] = new SelectList(_context.NivelEstudios, "NivelEstudiosId", "NivelEstudiosId", autor.IdNivelEstudios1);
            return View(autor);
        }

        // GET: datosCompletos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Autors == null)
            {
                return NotFound();
            }

            var autor = await _context.Autors.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            ViewData["IdNivelEstudios1"] = new SelectList(_context.NivelEstudios, "NivelEstudiosId", "NivelEstudiosId", autor.IdNivelEstudios1);
            return View(autor);
        }

        // POST: datosCompletos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAutor,Nombre,ApePaterno,ApeMaterno,Matricula,NumEmpleado,TipoCuenta,NumTelefono,FechaNaci,CuerpoAcademico,AreaEstudios,IdNivelEstudios1")] Autor autor)
        {
            if (id != autor.IdAutor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorExists(autor.IdAutor))
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
            ViewData["IdNivelEstudios1"] = new SelectList(_context.NivelEstudios, "NivelEstudiosId", "NivelEstudiosId", autor.IdNivelEstudios1);
            return View(autor);
        }

        // GET: datosCompletos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Autors == null)
            {
                return NotFound();
            }

            var autor = await _context.Autors
                .Include(a => a.IdNivelEstudios1Navigation)
                .FirstOrDefaultAsync(m => m.IdAutor == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // POST: datosCompletos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Autors == null)
            {
                return Problem("Entity set 'SisCadticContext.Autors'  is null.");
            }
            var autor = await _context.Autors.FindAsync(id);
            if (autor != null)
            {
                _context.Autors.Remove(autor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutorExists(int id)
        {
          return (_context.Autors?.Any(e => e.IdAutor == id)).GetValueOrDefault();
        }
      
    }
}
