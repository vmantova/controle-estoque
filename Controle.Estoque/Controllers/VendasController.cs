using Controle.Estoque.Data;
using Controle.Estoque.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Controle.Estoque.Controllers
{
    [Authorize]
    public class VendasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendasController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchVenda = null)
        {
            var model = new List<Venda>();
            if(string.IsNullOrEmpty(searchVenda))
                model = _context.Vendas.Where(x => x.StatusVenda == "A").ToList();
            else
                model = _context.Vendas.Where(x =>(x.Cliente.NomeCliente.Contains(searchVenda) || x.Produto.Descricao == searchVenda)).ToList();

            foreach (var venda in model)
            {
                venda.Cliente = _context.Clientes.FirstOrDefault(t => t.IdCliente == venda.ClienteId);
                venda.Produto = _context.Produtos.FirstOrDefault(t => t.Id== venda.ProdutoId);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var produtos = _context.Produtos.Where(t => t.Status == "D" && t.EstoqueAtual > t.EstoqueMinimo).ToList();
            var unidades = _context.Unidades.Where(u => u.Status == "A").ToList();
            var clientes = _context.Clientes.Where(v => v.Status == "A").ToList();

            if (produtos != null)
                ViewBag.Produtos = produtos;

            if (unidades != null)
                ViewBag.Unidades = unidades;
            if (clientes != null)
                ViewBag.Clientes = clientes;

            return View(new Venda());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create ([Bind("ClienteId,ProdutoId,Quantidade,UnidadeId")]Venda venda)
        {
            Produto produto = null;
            if (ModelState.IsValid)
            {
                produto = new Produto();
                produto = _context.Find<Produto>(venda.ProdutoId);
                if(produto.EstoqueAtual < venda.Quantidade)
                {
                    return View(nameof(Erro), new Erro { Mensagem = "Não há estoque suficiente para essa venda!" });
                }
                venda.DataVenda = DateTime.Now;
                var chars = "0123456789";
                var random = new Random();
                venda.Pedido = new string(Enumerable.Repeat(chars, 7).Select(s => s[random.Next(s.Length)]).ToArray());
                venda.StatusVenda = "A";
                _context.Add(venda);
                produto.EstoqueAtual -= venda.Quantidade;
                _context.Update(produto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Produtos = _context.Produtos.Where(t => t.Status == "D").ToList();
            ViewBag.Unidades = _context.Unidades.Where(u => u.Status == "A").ToList();
            ViewBag.Clientes = _context.Clientes.Where(v => v.Status == "A").ToList();
            return View("Create",venda); 
        }
        public IActionResult Delete (int idVenda)
        {
            var venda = _context.Find<Venda>(idVenda);
            var produto = _context.Find<Produto>(venda.ProdutoId);
            if(venda != null)
            {
                venda.StatusVenda = "C";
                produto.EstoqueAtual += venda.Quantidade;
                venda.Observacao = "Cancelada as:" + DateTime.Now;
                _context.Update(venda);
                _context.Update(produto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nameof(Erro), new Erro { Mensagem = "Não foi possivel cancelar venda!" });
        }

    }
}
