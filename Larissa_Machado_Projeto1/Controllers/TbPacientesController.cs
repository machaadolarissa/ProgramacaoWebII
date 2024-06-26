﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Larissa_Machado_Projeto1.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;

namespace Larissa_Machado_Projeto1.Controllers
{
    public class TbPacientesController : Controller
    {
        private readonly db_IFContext _context;

        public TbPacientesController(db_IFContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Medico,Nutricionista")]
        // GET: TbPacientes
        public async Task<IActionResult> Index()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var tbProfissional = await _context.TbProfissional.FirstOrDefaultAsync(u => u.IdUser == user);

            var idProfissional = tbProfissional.IdProfissional;

            var db_IFContext = _context.TbPaciente
                                    .Join(
                                        _context.TbMedicoPaciente,
                                        paciente => paciente.IdPaciente,
                                        medicoPaciente => medicoPaciente.IdPaciente,
                                        (paciente, medicoPaciente) => new { Paciente = paciente, MedicoPaciente = medicoPaciente }
                                    )
                                    .Where(join => join.MedicoPaciente.IdProfissional == idProfissional)
                                    .Select(join => join.Paciente)
                                    .Include(p => p.IdCidadeNavigation);

            return View(await db_IFContext.ToListAsync());
        }

        [Authorize(Roles = "Medico,Nutricionista")]
        // GET: TbPacientes/Details/5
        // Larissa Machado
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var tbPaciente = await _context.TbPaciente
                .Include(t => t.IdCidadeNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdPaciente == id);
            if (tbPaciente == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(tbPaciente);
        }

        [Authorize(Roles = "Medico,Nutricionista")]
        // GET: TbPacientes/Create
        // Larissa Machado
        public IActionResult Create()
        {
            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "Nome");
            ViewData["SimNaoOptions"] = new SelectList(new[]
            {
                    new { Value = true, Text = "Sim" },
                    new { Value = false, Text = "Não" }
            }, "Value", "Text");
            ViewData["EtniasOptions"] = new SelectList(new[]
            {
                    new { Value = 0, Text = "Negro"},
                    new { Value = 1, Text = "Branco"},
                    new { Value = 2, Text = "Pardo"},
                    new { Value = 3, Text = "Indígena"},
                    new { Value = 4, Text = "Amarelo"},
            }, "Value", "Text");
            ViewData["SexoOptions"] = new SelectList(new[]
            {
                    new { Value = "F", Text = "Feminino" },
                    new { Value = "M", Text = "Masculino" }
            }, "Value", "Text");
            return View();
        }

        [Authorize(Roles = "Medico,Nutricionista")]
        // POST: TbPacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Larissa Machado
        public async Task<IActionResult> Create([Bind("Nome,Rg,Cpf,DataNascimento,NomeResponsavel,Sexo,Etnia,Endereco,Bairro,IdCidade,TelResidencial,TelComercial,TelCelular,Profissao,FlgAtleta,FlgGestante")] TbPaciente tbPaciente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(tbPaciente);
                    await _context.SaveChangesAsync();

                    var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    var tbProfissional = await _context.TbProfissional.FirstOrDefaultAsync(u => u.IdUser == user);

                    var userId = tbProfissional.IdProfissional;
                    var idPaciente = tbPaciente.IdPaciente;
                    var informacaoResumida = tbPaciente.Nome;

                    TbMedicoPaciente tbMedicoPaciente = new TbMedicoPaciente
                    {
                        IdPaciente = idPaciente,
                        IdProfissional = userId,
                        InformacaoResumida = informacaoResumida
                    };

                    _context.TbMedicoPaciente.Add(tbMedicoPaciente);
                    await _context.SaveChangesAsync();
                    
                    return RedirectToAction(nameof(Index));
                }
            } catch (DbUpdateException dex)
            {
                ModelState.AddModelError("", "Não foi possível salvar" + dex.ToString());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro Genérico" + ex.ToString());
            }
            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "Nome", tbPaciente.IdCidade);
            ViewData["SimNaoOptions"] = new SelectList(new[]
            {
                    new { Value = true, Text = "Sim" },
                    new { Value = false, Text = "Não" }
            }, "Value", "Text");
            ViewData["EtniasOptions"] = new SelectList(new[]
{
                    new { Value = 0, Text = "Negro"},
                    new { Value = 1, Text = "Branco"},
                    new { Value = 2, Text = "Pardo"},
                    new { Value = 3, Text = "Indígena"},
                    new { Value = 4, Text = "Amarelo"},
            }, "Value", "Text");
            ViewData["SexoOptions"] = new SelectList(new[]
{
                    new { Value = "F", Text = "Feminino" },
                    new { Value = "M", Text = "Masculino" }
            }, "Value", "Text");
            return View(tbPaciente);
        }

        [Authorize(Roles = "Medico,Nutricionista")]
        // GET: TbPacientes/Edit/5
        // Larissa Machado
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var tbPaciente = await _context.TbPaciente.FindAsync(id);
            if (tbPaciente == null)
            {
                return RedirectToAction("Error", "Home");
            }
            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "Nome", tbPaciente.IdCidade);
            ViewData["SimNaoOptions"] = new SelectList(new[]
            {
                    new { Value = true, Text = "Sim" },
                    new { Value = false, Text = "Não" }
            }, "Value", "Text");
            ViewData["EtniasOptions"] = new SelectList(new[]
{
                    new { Value = 0, Text = "Negro"},
                    new { Value = 1, Text = "Branco"},
                    new { Value = 2, Text = "Pardo"},
                    new { Value = 3, Text = "Indígena"},
                    new { Value = 4, Text = "Amarelo"},
            }, "Value", "Text");
            ViewData["SexoOptions"] = new SelectList(new[]
{
                    new { Value = "F", Text = "Feminino" },
                    new { Value = "M", Text = "Masculino" }
            }, "Value", "Text");
            return View(tbPaciente);
        }

        [Authorize(Roles = "Medico,Nutricionista")]
        // POST: TbPacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        // Larissa Machado
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            var tbPaciente = await _context.TbPaciente.FirstOrDefaultAsync(p => p.IdPaciente == id);

            if (await TryUpdateModelAsync<TbPaciente>(
                tbPaciente,
                "",
                p => p.Nome, p => p.Rg, p => p.Cpf, p => p.DataNascimento, p => p.NomeResponsavel, p => p.Sexo,
                p => p.Etnia, p => p.Endereco, p => p.Bairro, p => p.IdCidade, p => p.TelResidencial, p => p.TelComercial,
                p => p.Profissao, p => p.FlgAtleta, p => p.FlgGestante))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator." + ex.ToString());
                }
            }
            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "Nome", tbPaciente.IdCidade);
            ViewData["SimNaoOptions"] = new SelectList(new[]
            {
                    new { Value = true, Text = "Sim" },
                    new { Value = false, Text = "Não" }
            }, "Value", "Text");
            ViewData["EtniasOptions"] = new SelectList(new[]
{
                    new { Value = 0, Text = "Negro"},
                    new { Value = 1, Text = "Branco"},
                    new { Value = 2, Text = "Pardo"},
                    new { Value = 3, Text = "Indígena"},
                    new { Value = 4, Text = "Amarelo"},
            }, "Value", "Text");
            ViewData["SexoOptions"] = new SelectList(new[]
{
                    new { Value = "F", Text = "Feminino" },
                    new { Value = "M", Text = "Masculino" }
            }, "Value", "Text");
            return View(tbPaciente);
        }

        [Authorize(Roles = "Medico,Nutricionista")]
        // GET: TbPacientes/Delete/5
        // Larissa Machado
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var tbPaciente = await _context.TbPaciente
                .Include(t => t.IdCidadeNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdPaciente == id);
            if (tbPaciente == null)
            {
                return RedirectToAction("Error", "Home");
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(tbPaciente);
        }

        [Authorize(Roles = "Medico,Nutricionista")]
        // POST: TbPacientes/Delete/5
        // Larissa Machado
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbPaciente = await _context.TbPaciente.FindAsync(id);
            if (tbPaciente == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.TbPaciente.Remove(tbPaciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
    }
}
