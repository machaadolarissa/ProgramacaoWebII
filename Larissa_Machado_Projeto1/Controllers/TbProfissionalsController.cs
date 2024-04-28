using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Larissa_Machado_Projeto1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Larissa_Machado_Projeto1.Controllers
{
    [Authorize]
    public class TbProfissionalsController : Controller
    {
        private readonly db_IFContext _context;

        public TbProfissionalsController(db_IFContext context)
        {
            _context = context;
        }

        public enum Plano
        {
            MedicoTotal = 1,
            MedicoParcial = 5,
            NutricionalTotal = 6,
            NutricionalParcial = 1002
        }

        [Authorize(Roles = "GerenteMedico,GerenteNutricionista,GerenteGeral,Medico,Nutricionista")]
        public IActionResult Index()
        {
            if (User.IsInRole("GerenteMedico"))
            {
                var db_IFContextGerenteMedico = _context.TbProfissional
                                    .Where(t => (Plano)t.IdContratoNavigation.IdPlano == Plano.MedicoTotal || (Plano)t.IdContratoNavigation.IdPlano == Plano.MedicoParcial)
                                    .Select(pro => new ProfissionalResumido
                                    {
                                        Nome = pro.Nome,
                                        NomeCidade = pro.IdCidadeNavigation.Nome,
                                        NomePlano = pro.IdContratoNavigation.IdPlanoNavigation.Nome,
                                        IdProfissional = pro.IdProfissional,
                                        Cpf = pro.Cpf,
                                        CrmCrn = pro.CrmCrn,
                                        Especialidade = pro.Especialidade,
                                        Logradouro = pro.Logradouro,
                                        Numero = pro.Numero,
                                        Bairro = pro.Bairro,
                                        Cep = pro.Cep,
                                        Ddd1 = pro.Ddd1,
                                        Ddd2 = pro.Ddd2,
                                        Telefone1 = pro.Telefone1,
                                        Telefone2 = pro.Telefone2,
                                        Salario = pro.Salario,
                                    });

                return View(db_IFContextGerenteMedico);
            }
            else if (User.IsInRole("GerenteNutricionista"))
            {
                var db_IFContextGerenteNutricionista = _context.TbProfissional
                                    .Where(t => (Plano)t.IdContratoNavigation.IdPlano == Plano.NutricionalTotal || (Plano)t.IdContratoNavigation.IdPlano == Plano.NutricionalParcial)
                                    .Select(pro => new ProfissionalResumido
                                    {
                                        Nome = pro.Nome,
                                        NomeCidade = pro.IdCidadeNavigation.Nome,
                                        NomePlano = pro.IdContratoNavigation.IdPlanoNavigation.Nome,
                                        IdProfissional = pro.IdProfissional,
                                        Cpf = pro.Cpf,
                                        CrmCrn = pro.CrmCrn,
                                        Especialidade = pro.Especialidade,
                                        Logradouro = pro.Logradouro,
                                        Numero = pro.Numero,
                                        Bairro = pro.Bairro,
                                        Cep = pro.Cep,
                                        Ddd1 = pro.Ddd1,
                                        Ddd2 = pro.Ddd2,
                                        Telefone1 = pro.Telefone1,
                                        Telefone2 = pro.Telefone2,
                                        Salario = pro.Salario,
                                    });

                return View(db_IFContextGerenteNutricionista);
            }

            else if ((User.IsInRole("Medico")) || (User.IsInRole("Nutricionista")))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var db_IFContextProfissionals = _context.TbProfissional
                                    .Where(t => t.IdUser == userId)
                                    .Select(pro => new ProfissionalResumido
                                    {
                                        Nome = pro.Nome,
                                        NomeCidade = pro.IdCidadeNavigation.Nome,
                                        NomePlano = pro.IdContratoNavigation.IdPlanoNavigation.Nome,
                                        IdProfissional = pro.IdProfissional,
                                        Cpf = pro.Cpf,
                                        CrmCrn = pro.CrmCrn,
                                        Especialidade = pro.Especialidade,
                                        Logradouro = pro.Logradouro,
                                        Numero = pro.Numero,
                                        Bairro = pro.Bairro,
                                        Cep = pro.Cep,
                                        Ddd1 = pro.Ddd1,
                                        Ddd2 = pro.Ddd2,
                                        Telefone1 = pro.Telefone1,
                                        Telefone2 = pro.Telefone2,
                                        Salario = pro.Salario,
                                    });

                return View(db_IFContextProfissionals);
            }

            else if (User.IsInRole("GerenteGeral"))
            {
                var db_IFContextAll = _context.TbProfissional
                                    .Select(pro => new ProfissionalResumido
                                    {
                                        Nome = pro.Nome,
                                        NomeCidade = pro.IdCidadeNavigation.Nome,
                                        NomePlano = pro.IdContratoNavigation.IdPlanoNavigation.Nome,
                                        IdProfissional = pro.IdProfissional,
                                        Cpf = pro.Cpf,
                                        CrmCrn = pro.CrmCrn,
                                        Especialidade = pro.Especialidade,
                                        Logradouro = pro.Logradouro,
                                        Numero = pro.Numero,
                                        Bairro = pro.Bairro,
                                        Cep = pro.Cep,
                                        Ddd1 = pro.Ddd1,
                                        Ddd2 = pro.Ddd2,
                                        Telefone1 = pro.Telefone1,
                                        Telefone2 = pro.Telefone2,
                                        Salario = pro.Salario,
                                    });

                return View(db_IFContextAll);
            }

            return View();
        }

        [Authorize(Roles = "GerenteMedico,GerenteNutricionista,GerenteGeral,Medico,Nutricionista")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = HttpContext.RequestServices.GetService<UserManager<IdentityUser>>();
            if (user == null)
            {
                return NotFound();
            }

            var tbProfissional = await _context.TbProfissional
                .Include(t => t.IdCidadeNavigation)
                .Include(t => t.IdContratoNavigation)
                .Include(t => t.IdTipoAcessoNavigation)
                .FirstOrDefaultAsync(m => m.IdProfissional == id);

            if (tbProfissional == null)
            {
                return NotFound();
            }

            return View(tbProfissional);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Create()
        {
            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "Nome");
            ViewData["IdPlano"] = new SelectList(_context.TbPlano, "IdPlano", "Nome");
            ViewData["IdTipoAcesso"] = new SelectList(_context.TbTipoAcesso, "IdTipoAcesso", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoProfissional,IdTipoAcesso,IdCidade,IdUser,Nome,Cpf,CrmCrn,Especialidade,Logradouro,Numero,Bairro,Cep,Cidade,Estado,Ddd1,Ddd2,Telefone1,Telefone2,Salario")] TbProfissional tbProfissional, [Bind("IdPlano")] TbContrato IdContratoNavigation)
        {
            ModelState.Remove("IdUser");
            ModelState.Remove("idContrato");
            if (ModelState.IsValid)
            {
                IdContratoNavigation.DataInicio = DateTime.UtcNow;
                IdContratoNavigation.DataFim = IdContratoNavigation.DataInicio.Value.AddMonths(1);
                _context.Add(IdContratoNavigation);
                await _context.SaveChangesAsync();

                var userManager = HttpContext.RequestServices.GetService<UserManager<IdentityUser>>();
                if (userManager != null)
                {
                    var email = User.Identity?.Name;
                    if (email != null)
                    {
                        var user = await userManager.FindByEmailAsync(email);
                        if (user != null)
                        {
                            tbProfissional.IdUser = user.Id;
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
                tbProfissional.IdContrato = IdContratoNavigation.IdContrato;
                _context.Add(tbProfissional);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "Nome");
            ViewData["idPlano"] = new SelectList(_context.TbPlano, "IdPlano", "Nome");
            ViewData["IdTipoAcesso"] = new SelectList(_context.TbTipoAcesso, "IdTipoAcesso", "Nome");
            return View(tbProfissional);
        }

        // GET: TbProfissionals/Edit/5
        [Authorize(Roles = "GerenteMedico,GerenteNutricionista,GerenteGeral,Medico,Nutricionista")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProfissional = await _context.TbProfissional.FindAsync(id);
            if (tbProfissional == null)
            {
                return NotFound();
            }

            bool permiteEditarCPF = User.IsInRole("GerenteGeral") || User.IsInRole("GerenteMedico") || User.IsInRole("GerenteNutricionista");

            ViewData["PermiteEditarCPF"] = permiteEditarCPF;

            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "IdCidade", tbProfissional.IdCidade);
            ViewData["IdContrato"] = new SelectList(_context.TbContrato, "IdContrato", "IdContrato", tbProfissional.IdContrato);
            ViewData["IdTipoAcesso"] = new SelectList(_context.TbTipoAcesso, "IdTipoAcesso", "Nome", tbProfissional.IdTipoAcesso);
            return View(tbProfissional);
        }

        // POST: TbProfissionals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProfissional,IdTipoProfissional,IdContrato,IdTipoAcesso,IdCidade,IdUser,Nome,Cpf,CrmCrn,Especialidade,Logradouro,Numero,Bairro,Cep,Cidade,Estado,Ddd1,Ddd2,Telefone1,Telefone2,Salario")] TbProfissional tbProfissional)
        {
            if (id != tbProfissional.IdProfissional)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbProfissional);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbProfissionalExists(tbProfissional.IdProfissional))
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
            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "IdCidade", tbProfissional.IdCidade);
            ViewData["IdContrato"] = new SelectList(_context.TbContrato, "IdContrato", "IdContrato", tbProfissional.IdContrato);
            ViewData["IdTipoAcesso"] = new SelectList(_context.TbTipoAcesso, "IdTipoAcesso", "Nome", tbProfissional.IdTipoAcesso);
            return View(tbProfissional);
        }

        // GET: TbProfissionals/Delete/5
        [Authorize(Roles = "GerenteMedico,GerenteNutricionista,GerenteGeral")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProfissional = await _context.TbProfissional
                .Include(t => t.IdCidadeNavigation)
                .Include(t => t.IdContratoNavigation)
                .Include(t => t.IdTipoAcessoNavigation)
                .FirstOrDefaultAsync(m => m.IdProfissional == id);

            if (tbProfissional == null)
            {
                return NotFound();
            }

            var hasAssociatedPatients = _context.TbMedicoPaciente.Any(tp => tp.IdProfissional == id);
            if (hasAssociatedPatients)
            {
                TempData["ErrorMessage"] = "Não é possível excluir este profissional pois existem pacientes associados a ele";
                return RedirectToAction(nameof(Index));
            }

            return View(tbProfissional);
        }


        // POST: TbProfissionals/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "GerenteMedico,GerenteNutricionista,GerenteGeral")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbProfissional = await _context.TbProfissional.FindAsync(id);
            if (tbProfissional != null)
            {
                _context.TbProfissional.Remove(tbProfissional);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool TbProfissionalExists(int id)
        {
            return _context.TbProfissional.Any(e => e.IdProfissional == id);
        }
    }
}
