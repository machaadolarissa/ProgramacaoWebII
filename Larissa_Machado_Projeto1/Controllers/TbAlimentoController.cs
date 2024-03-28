using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Larissa_Machado_Projeto1.Models;

namespace Larissa_Machado_Projeto1.Controllers
{
    public class TbAlimentoController : Controller
    {
        private readonly db_IFContext _context;

        public TbAlimentoController(db_IFContext context)
        {
            _context = context;
        }

        // GET: TbAlimento
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbAlimento.ToListAsync());
        }

        // GET: TbAlimento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbAlimento = await _context.TbAlimento
                .FirstOrDefaultAsync(m => m.IdAlimento == id);
            if (tbAlimento == null)
            {
                return NotFound();
            }

            return View(tbAlimento);
        }

        // GET: TbAlimento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbAlimento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAlimento,IdTipoQuantidade,Nome,Carboidrato,VitaminaA,VitaminaB")] TbAlimento tbAlimento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbAlimento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbAlimento);
        }

        // GET: TbAlimento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbAlimento = await _context.TbAlimento.FindAsync(id);
            if (tbAlimento == null)
            {
                return NotFound();
            }
            return View(tbAlimento);
        }

        // POST: TbAlimento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAlimento,IdTipoQuantidade,Nome,Carboidrato,VitaminaA,VitaminaB")] TbAlimento tbAlimento)
        {
            if (id != tbAlimento.IdAlimento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbAlimento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbAlimentoExists(tbAlimento.IdAlimento))
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
            return View(tbAlimento);
        }

        // GET: TbAlimento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbAlimento = await _context.TbAlimento
                .FirstOrDefaultAsync(m => m.IdAlimento == id);
            if (tbAlimento == null)
            {
                return NotFound();
            }

            return View(tbAlimento);
        }

        // POST: TbAlimento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbAlimento = await _context.TbAlimento.FindAsync(id);
            if (tbAlimento != null)
            {
                _context.TbAlimento.Remove(tbAlimento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbAlimentoExists(int id)
        {
            return _context.TbAlimento.Any(e => e.IdAlimento == id);
        }
    }
}
