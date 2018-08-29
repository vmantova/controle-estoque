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
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View(new Categoria());
        }

        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                categoria.Status = "A";
                categoria.DataInclusao = DateTime.Now;
                _context.Add(categoria);
                _context.SaveChanges();
            }

            return RedirectToAction("Create");
        }
    }
}
