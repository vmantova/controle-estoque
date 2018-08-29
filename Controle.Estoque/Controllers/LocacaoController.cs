using Controle.Estoque.Data;
using Controle.Estoque.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace Controle.Estoque.Controllers
{
    [Authorize]
    public class LocacaoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocacaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = _context.Locacoes;

            foreach (var item in model)
            {
                item.Cliente = _context.Clientes.FirstOrDefault(t => t.IdCliente == item.ClienteId);

                item.Produto = _context.Produtos.FirstOrDefault(t => t.Id == item.ProdutoId);
            }
            return View(model.OrderByDescending(t => t.DataDevolucao).ToList());
        }

        public IActionResult New()
        {
            var clientes = _context.Clientes.Where(t => t.Status != "I");
            var produtos = _context.Produtos.Where(t => t.Status == "D");

            if (clientes != null)
                ViewBag.Clientes = new SelectList(clientes.ToList(), "IdCliente", "NomeCliente");

            if (produtos != null)
                ViewBag.Produtos = new SelectList(produtos.ToList(), "Id", "Descricao");

            return View(new Locacao());
        }

        [HttpPost]
        public IActionResult New(Locacao locacao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    locacao.DataLocacao = DateTime.Now;

                    var produto = _context.Produtos.FirstOrDefault(t => t.Id == locacao.ProdutoId);

                    if (locacao.DataDevolucao.Date < DateTime.Now.Date)
                    {
                        return View(nameof(Erro), new Erro { Mensagem = "Data da devolução inválida" });
                    }

                    if (produto != null)
                    {
                        if (locacao.Quantidade <= produto.EstoqueAtual)
                        {
                            if ((produto.EstoqueAtual - locacao.Quantidade) >= produto.EstoqueMinimo)
                            {
                                //criar a locacao
                                locacao.Status = "A"; // aberta
                                produto.EstoqueAtual -= locacao.Quantidade;
                                _context.Add(locacao);
                                _context.Update(produto);
                                _context.SaveChanges();
                            }
                            else
                            {
                                return View(nameof(Erro), new Erro { Mensagem = "Caso a locação seja efetuada esse produto ficará com quantidade no estoque menor que o recomendado" });
                            }
                        }
                        else
                        {
                            return View(nameof(Erro), new Erro { Mensagem = "Quantidade do produto no estoque é insuficiente para essa locação!" });
                        }
                    }

                    return RedirectToAction(nameof(Index),"Locacao");

                }
                else
                {
                    var clientes = _context.Clientes.Where(t => t.Status != "I");
                    var produtos = _context.Produtos.Where(t => t.Status == "D");

                    if (clientes != null)
                        ViewBag.Clientes = new SelectList(clientes.ToList(), "IdCliente", "NomeCliente");

                    if (produtos != null)
                        ViewBag.Produtos = new SelectList(produtos.ToList(), "Id", "Descricao");
                    return View("New", locacao);
                }
            }
            catch (Exception)
            {

                return View(nameof(Erro), new Erro { Mensagem = "Ocorreu um erro na execução " });
            }
        }

        [HttpGet]
        public IActionResult Retorno(int id)
        {
            var locacao = _context.Locacoes.FirstOrDefault(t => t.Id == id);

            if(locacao != null)
            {
                var produto = _context.Produtos.FirstOrDefault(t => t.Id == locacao.ProdutoId);

                produto.EstoqueAtual += locacao.Quantidade;
                locacao.Status = "C"; // Concluída
                _context.Update(locacao);
                _context.Update(produto);
                _context.SaveChanges();
                   
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(nameof(Erro), "Locação não encontrada");
            }

        }

        public IActionResult CalculaPrecoTotal(int id, int quantidade)
        {
            var produto = _context.Produtos.FirstOrDefault(t => t.Id == id);

            if (produto != null)
            {
                var total = produto.Preco * quantidade;
                return Json(total);
            }

            return Json(-1);
        }

        public IActionResult GetEstoqueAtual(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(t => t.Id == id);

            if (produto != null)
            {
                return Json(produto.EstoqueAtual);
            }

            return Json(-1);
        }
    }
}
