using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Controle.Estoque.Data;
using Controle.Estoque.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Controle.Estoque.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IActionResult Index(string searchParam = null)
        {
            var model = new List<Cliente>();
            if(string.IsNullOrEmpty(searchParam))
                model = _context.Clientes.Where(x => x.Status == "A").ToList();
            else
                model = _context.Clientes.Where(x => x.Status == "A" && (x.NomeCliente.Contains(searchParam) || x.Documento == searchParam)).ToList();
            return View(model);
        }
        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            var estados = _context.Estados.ToList();

            //ViewBag.DropDownEstados = new SelectList(estados,"Sigla","Nome");

            if(estados != null)
                ViewBag.Estados = estados;

            return View(new Cliente());
        }
        [HttpPost]
        public IActionResult Create([Bind("NomeCliente,Documento,Endereco,Cidade,Estado,Pais,Telefone,Email")]Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.Status = "A";
                cliente.Data_inclusao = DateTime.Now;
                _context.Add(cliente);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Estados = _context.Estados.ToList();
                return View("Create",cliente);
            }

        }
        public IActionResult Delete(int idCliente)
        {
            var cliente = _context.Find<Cliente>(idCliente);
            if (cliente != null)
            {
                cliente.Status = "I";
                cliente.Data_alteracao = DateTime.Now;
                _context.Update(cliente);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit([Bind("NomeCliente,Documento,Endereco,Cidade,Estado,Pais,Telefone,Email")]int idCliente)
        {

            var estados = _context.Estados.ToList();

            if (estados != null)
                ViewBag.Estados = estados;

            var cliente = _context.Find<Cliente>(idCliente);
            if (cliente != null)
                return View(cliente);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit([Bind("NomeCliente,Documento,Endereco,Cidade,Estado,Pais,Telefone,Email")]Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.Data_alteracao = DateTime.Now;
                _context.Update(cliente);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Estados = _context.Estados.ToList();
                return View("Edit", cliente);
            }
        }
        [HttpGet]
        public  IActionResult Details (int idCliente)
        {
            var cliente = _context.Find<Cliente>(idCliente);
            if (cliente != null)
                return View(cliente);
            return RedirectToAction("Index");
        }
    }
}