#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc.Repository;
using mvc.Repository.Models;

namespace mvc.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly gcasppDbContext _context;

        public FuncionariosController(gcasppDbContext context)
        {
            _context = context;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index(string searchString)
        {
            var funcionarios = from m in _context.Funcionarios
                               select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                funcionarios = funcionarios.Where(s => s.FuncionarioNome!.Contains(searchString));
                return View(await funcionarios.ToListAsync());
            }
            else
            {
                return View(await _context.Funcionarios.ToListAsync());
            }
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.FuncionarioId == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FuncionarioId,FuncionarioNome,FuncionarioDatadenascimento,FuncionarioSalario")] Funcionario funcionario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(funcionario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(funcionario);
            }
            catch (Exception er)
            { 
                return RedirectToAction(actionName: "Index");
            }
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FuncionarioId,FuncionarioNome,FuncionarioDatadenascimento,FuncionarioSalario")] Funcionario funcionario)
        {
            if (id != funcionario.FuncionarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.FuncionarioId))
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
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.FuncionarioId == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionarios.Any(e => e.FuncionarioId == id);
        }
    }
}
