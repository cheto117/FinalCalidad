using System;
using System.Collections.Generic;
using System.Linq;
using Finanzas.Models;
using Finanzas.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Finanzas.Controllers
{
    public class TransaccionController : Controller
    {
        private readonly ITransaccionRepositorio _context;
        public TransaccionController(ITransaccionRepositorio context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult Index(int id)
        {
            var transacciones = _context.GetTransaccions(id);
            ViewBag.Cuenta = _context.GetCuenta(id);
            return View("Index", transacciones);
        }

        [HttpGet]
        public ActionResult Crear(int id)
        {
            ViewBag.Tipos = _context.GetTipos();
            ViewBag.CuentaId = id;
            return View("Crear");
        }

        [HttpPost]
        public ActionResult Crear(Transaccion transaccion)
        {
            var cuenta = _context.GetCuenta(transaccion.IdCuenta);

            if (cuenta.IdCategoria == 1)
            {
                if (transaccion.IdTipo == 2 && (cuenta.Limite + cuenta.Saldo) >= transaccion.Monto)
                    transaccion.Monto *= -1;
                else if (transaccion.IdTipo != 1)
                {
                    //TempData["Cuenta"] = "El monto del egreso supera el saldo de la cuenta"; 
                    ModelState.AddModelError("Cuenta", "Error");
                }
            }
            else
            {
                if (transaccion.IdTipo == 2)
                    transaccion.Monto *= -1;
            }

            if (ModelState.IsValid)
            {
                _context.SaveTransaccion(transaccion);
                return RedirectToAction("Index", new { id = transaccion.IdCuenta });
            }
            else
            {
                ViewBag.Tipos = _context.GetTipos();
                ViewBag.CuentaId = transaccion.IdCuenta;
                return View("Crear", transaccion);
            }
        }
    }
}
