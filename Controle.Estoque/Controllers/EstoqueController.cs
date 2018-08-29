using Controle.Estoque.Data;
using Controle.Estoque.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Controle.Estoque.Controllers
{
    [Authorize]
    public class EstoqueController : Controller
    {
        private ApplicationDbContext _context;

        public EstoqueController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchProd = null)
        {
            var model = new List<Produto>();


            model = string.IsNullOrEmpty(searchProd) ? _context.Produtos.Where(t => t.Status == "D").ToList()
                                                      : _context.Produtos.Where(t => t.Status == "D" && t.Descricao.Contains(searchProd)).ToList();
            foreach (var item in model)
            {
                item.Categoria = _context.Categorias.FirstOrDefault(t => t.Id == item.CategoriaId);
                item.UnidadeMedida = _context.Unidades.FirstOrDefault(u => u.Id == item.UnidadeId);
            }

            return View(model);
        }
    }
}
