using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Controle.Estoque.Models;
using Microsoft.AspNetCore.Authorization;
using Controle.Estoque.Data;

namespace Controle.Estoque.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = _context.Produtos.Where(t => t.EstoqueAtual == t.EstoqueMinimo && t.Status != "E").ToList();

            foreach (var item in model)
            {
                item.Categoria = _context.Categorias.FirstOrDefault(t => t.Id == item.CategoriaId);
            }
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
