using Controle.Estoque.Data;
using Controle.Estoque.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controle.Estoque.Controllers
{
    [Authorize]
    public class UnidadesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UnidadesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View(new Unidade());
        }

        [HttpPost]
        public IActionResult Create(Unidade unidade)
        {
            if (ModelState.IsValid)
            {
                unidade.Status = "A";
                unidade.DataInclusao = DateTime.Now;
                _context.Add(unidade);
                _context.SaveChanges();
            }

            return RedirectToAction("Create");
        }
    }
}
