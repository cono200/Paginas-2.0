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
    public class RegistroController : Controller
    {
        private readonly SisCadticContext _context;

        public RegistroController(SisCadticContext context)
        {
            _context = context;
        }

        // GET: Registro
        public async Task<IActionResult> Index()
        {
            var sisCadticContext = _context.Usuarios.Include(u => u.IdAutor1Navigation);
            return View(await sisCadticContext.ToListAsync());
        }

        // GET: Registro/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdAutor1Navigation)
                .FirstOrDefaultAsync(m => m.NombreUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Registro/Create
        public IActionResult Create()
        {
            ViewData["IdAutor1"] = new SelectList(_context.Autors, "IdAutor", "IdAutor");
            return View();
        }

        // POST: Registro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreUsuario,Correo,Contrasena")] Usuario usuario, [Bind("TipoCuenta, Nombre, ApePaterno, ApeMaterno, FechaNaci, IdNivelEstudios1, AreaEstudios, NumTelefono")] Autor autor, string confcontrasena)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autor);
                _context.SaveChanges();
                usuario.IdAutor1 = autor.IdAutor;
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAutor1"] = new SelectList(_context.Autors, "IdAutor", "IdAutor", usuario.IdAutor1);
            return View(usuario);
        }

        // GET: Registro/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["IdAutor1"] = new SelectList(_context.Autors, "IdAutor", "IdAutor", usuario.IdAutor1);
            return View(usuario);
        }

        // POST: Registro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NombreUsuario,Correo,Contrasena,IdAutor1")] Usuario usuario)
        {
            if (id != usuario.NombreUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.NombreUsuario))
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
            ViewData["IdAutor1"] = new SelectList(_context.Autors, "IdAutor", "IdAutor", usuario.IdAutor1);
            return View(usuario);
        }

        // GET: Registro/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdAutor1Navigation)
                .FirstOrDefaultAsync(m => m.NombreUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Registro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'SisCadticContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(string id)
        {
          return (_context.Usuarios?.Any(e => e.NombreUsuario == id)).GetValueOrDefault();
        }
    }
}
