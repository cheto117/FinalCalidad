using Finanzas.Controllers;
using Finanzas.Models;
using Finanzas.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanzasTest
{
    [TestFixture]
    class TransaccionTest
    {
        [Test]
        public void IndexTest()
        {
            var repo = new Mock<ITransaccionRepositorio>();
            repo.Setup(o => o.GetTransaccions(1)).Returns(new List<Transaccion>());
            repo.Setup(o => o.GetCuenta(1)).Returns(new Cuenta());

            var controller = new TransaccionController(repo.Object);
            var view = controller.Index(1) as ViewResult;

            Assert.AreEqual("Index", view.ViewName);
        }

        [Test]
        public void CrearTestGet()
        {
            var repo = new Mock<ITransaccionRepositorio>();
            repo.Setup(o => o.GetTipos()).Returns(new List<Tipo>());

            var controller = new TransaccionController(repo.Object);
            var view = controller.Crear(1) as ViewResult;

            Assert.AreEqual("Crear", view.ViewName);
        }

        [Test]
        public void CrearTestPostGoodIngreso()
        {
            var repo = new Mock<ITransaccionRepositorio>();
            repo.Setup(o => o.GetCuenta(1)).Returns(new Cuenta() { Id = 1 });
            repo.Setup(o => o.SaveTransaccion(new Transaccion()));

            var controller = new TransaccionController(repo.Object);
            var view = controller.Crear(new Transaccion() 
                { IdCuenta = 1, IdTipo = 1 }) as RedirectToActionResult;

            Assert.AreEqual("Index", view.ActionName);
        }

        [Test]
        public void CrearTestPostGoodGasto()
        {
            var repo = new Mock<ITransaccionRepositorio>();
            repo.Setup(o => o.GetCuenta(1)).Returns(new Cuenta()
                {Id=1, Limite=10, Saldo=10});
            repo.Setup(o => o.SaveTransaccion(new Transaccion()));

            var controller = new TransaccionController(repo.Object);
            var view = controller.Crear(new Transaccion()
                {IdCuenta = 1, IdTipo=2, Monto = 10}) as RedirectToActionResult;

            Assert.AreEqual("Index", view.ActionName);
        }

        [Test]
        public void CrearTestPostBadEgreso()
        {
            var repo = new Mock<ITransaccionRepositorio>();
            repo.Setup(o => o.GetCuenta(1)).Returns(new Cuenta()
                { Id = 1, IdCategoria = 1, Limite = 10, Saldo = 10 });
            repo.Setup(o => o.SaveTransaccion(new Transaccion()));

            var controller = new TransaccionController(repo.Object);
            var view = controller.Crear(new Transaccion()
                { IdCuenta = 1, IdTipo = 2, Monto = 50 }) as ViewResult;

            Assert.AreEqual("Crear", view.ViewName);
        }
    }
}
