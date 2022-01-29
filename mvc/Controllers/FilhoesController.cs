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
    public class FilhoesController : Controller
    {
        private readonly gcasppDbContext _context;

        public FilhoesController(gcasppDbContext context)
        {
            _context = context;
        }

        // GET: Filhoes
        public async Task<IActionResult> Index(string searchString)
        {
            var filhos = from m in _context.Filhos
                         select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                filhos = filhos.Where(s => s.FilhoNome!.Contains(searchString));
                return View(await filhos.ToListAsync());
            }
            else
            {
                var gcasppDbContext = _context.Filhos.Include(f => f.FilhoFuncionarioMaeNavigation).Include(f => f.FilhoFuncionarioPaiNavigation);
                return View(await gcasppDbContext.ToListAsync());
            }
        }

        // GET: Filhoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filho = await _context.Filhos
                .Include(f => f.FilhoFuncionarioMaeNavigation)
                .Include(f => f.FilhoFuncionarioPaiNavigation)
                .FirstOrDefaultAsync(m => m.FilhoId == id);
            if (filho == null)
            {
                return NotFound();
            }

            return View(filho);
        }

        // GET: Filhoes/Create
        public IActionResult Create()
        {
            ViewData["FilhoFuncionarioMae"] = new SelectList(_context.Funcionarios, "FuncionarioId", "FuncionarioNome");
            ViewData["FilhoFuncionarioPai"] = new SelectList(_context.Funcionarios, "FuncionarioId", "FuncionarioNome");
            return View();
        }

        // POST: Filhoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FilhoId,FilhoNome,FilhoDatadenascimento,FilhoFuncionarioPai,FilhoFuncionarioMae")] Filho filho)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filho);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilhoFuncionarioMae"] = new SelectList(_context.Funcionarios, "FuncionarioId", "FuncionarioId", filho.FilhoFuncionarioMae);
            ViewData["FilhoFuncionarioPai"] = new SelectList(_context.Funcionarios, "FuncionarioId", "FuncionarioId", filho.FilhoFuncionarioPai);
            return View(filho);
        }

        // GET: Filhoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filho = await _context.Filhos.FindAsync(id);
            if (filho == null)
            {
                return NotFound();
            }
            ViewData["FilhoFuncionarioMae"] = new SelectList(_context.Funcionarios, "FuncionarioId", "FuncionarioId", filho.FilhoFuncionarioMae);
            ViewData["FilhoFuncionarioPai"] = new SelectList(_context.Funcionarios, "FuncionarioId", "FuncionarioId", filho.FilhoFuncionarioPai);
            return View(filho);
        }

        // POST: Filhoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FilhoId,FilhoNome,FilhoDatadenascimento,FilhoFuncionarioPai,FilhoFuncionarioMae")] Filho filho)
        {
            if (id != filho.FilhoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filho);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilhoExists(filho.FilhoId))
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
            ViewData["FilhoFuncionarioMae"] = new SelectList(_context.Funcionarios, "FuncionarioId", "FuncionarioId", filho.FilhoFuncionarioMae);
            ViewData["FilhoFuncionarioPai"] = new SelectList(_context.Funcionarios, "FuncionarioId", "FuncionarioId", filho.FilhoFuncionarioPai);
            return View(filho);
        }

        // GET: Filhoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filho = await _context.Filhos
                .Include(f => f.FilhoFuncionarioMaeNavigation)
                .Include(f => f.FilhoFuncionarioPaiNavigation)
                .FirstOrDefaultAsync(m => m.FilhoId == id);
            if (filho == null)
            {
                return NotFound();
            }

            return View(filho);
        }

        // POST: Filhoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filho = await _context.Filhos.FindAsync(id);
            _context.Filhos.Remove(filho);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilhoExists(int id)
        {
            return _context.Filhos.Any(e => e.FilhoId == id);
        }


    }
}
