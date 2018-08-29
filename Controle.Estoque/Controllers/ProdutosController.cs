using Controle.Estoque.Data;
using Controle.Estoque.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Controle.Estoque.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;
    
        public ProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            var categorias = _context.Categorias.Where(t => t.Status == "A").ToList();
            var unidades = _context.Unidades.Where(u => u.Status == "A").ToList();

            if (categorias != null)
                ViewBag.Categorias = categorias;

            if (unidades != null)
                ViewBag.Unidades = unidades;

            return View(new Produto());
        }

        [HttpPost]
        public IActionResult Create(Produto produto)
        {

            var categorias = _context.Categorias.Where(t => t.Status == "A").ToList();
            var unidades = _context.Unidades.Where(u => u.Status == "A").ToList();

            if (categorias != null)
                ViewBag.Categorias = categorias;

            if (unidades != null)
                ViewBag.Unidades = unidades;


            if (ModelState.IsValid)
            {
                produto.Codigo = Guid.NewGuid().ToString("N").ToUpper().Substring(0,8);
                produto.DataInclusao = DateTime.Now;
                produto.Status = "D";
                produto.Cor.ToUpper();

               _context.Add(produto);

                _context.SaveChanges();
                return RedirectToAction("Index", nameof(Estoque));
            }
            else
            {
                categorias = _context.Categorias.Where(t => t.Status == "A").ToList();
                unidades = _context.Unidades.Where(u => u.Status == "A").ToList();

                if (categorias != null)
                    ViewBag.Categorias = categorias;

                if (unidades != null)
                    ViewBag.Unidades = unidades;
                return View("Create", produto);
            }
            
        }

        public IActionResult Detail(int id)
        {
            var categorias = _context.Categorias.Where(t => t.Status == "A").ToList();
            var unidades = _context.Unidades.Where(u => u.Status == "A").ToList();

            if (categorias != null)
                ViewBag.Categorias = categorias;

            if (unidades != null)
                ViewBag.Unidades = unidades;

            var produto = _context.Find<Produto>(id);
            return View(produto);
        }

        public IActionResult Edit(int id)
        {
            var categorias = _context.Categorias.Where(t => t.Status == "A").ToList();
            var unidades = _context.Unidades.Where(u => u.Status == "A").ToList();

            if (categorias != null)
                ViewBag.Categorias = categorias;

            if (unidades != null)
                ViewBag.Unidades = unidades;

            var produto = _context.Find<Produto>(id);

            if (produto == null)
                return RedirectToAction("Index", nameof(Estoque));

            return View(produto);
        }

        [HttpPost]
        public IActionResult Edit(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Update(produto);
                _context.SaveChanges();
                return RedirectToAction("Index", nameof(Estoque));

            }
            else
            {
                var categorias = _context.Categorias.Where(t => t.Status == "A").ToList();
                var unidades = _context.Unidades.Where(u => u.Status == "A").ToList();

                if (categorias != null)
                    ViewBag.Categorias = categorias;

                if (unidades != null)
                    ViewBag.Unidades = unidades;
                return View("Edit", produto);
            }
        }

        public IActionResult ConfirmDelete(int id)
        {
            var model = _context.Produtos.FirstOrDefault(t => t.Id == id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id, int qtde)
        {
            var item = _context.Produtos.FirstOrDefault(t => t.Id == id);

            if (qtde > item.EstoqueAtual)
                return View(nameof(Erro), new Erro { Mensagem = "Você está tentando excluir mais do que o estoque de itens desse produto" });

            if (item != null &&  qtde == item.EstoqueAtual)
                item.Status = "E"; //Excluído

            if (item != null && (qtde <= item.EstoqueAtual))
                item.EstoqueAtual -= qtde;

            _context.Update(item);

            _context.SaveChanges();
           
            return RedirectToAction("Index", nameof(Estoque));
        }

    }
}
