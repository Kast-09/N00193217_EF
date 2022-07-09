using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using N00193217.Web.Controllers;
using N00193217.Web.Repositorio;
using N00193217.Web.Models;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace N00193217.Test.Controllers
{
    public class CuentaControllerTest
    {
        [Test]
        public void IndexViewCase01()
        {
            var mockCuentaRepositorio = new Mock<ICuentaRepositorio>();
            mockCuentaRepositorio.Setup(o => o.listarCuentas()).Returns(new List<Cuenta>());
            mockCuentaRepositorio.Setup(o => o.listarCuentasSoles()).Returns(new List<Cuenta>());
            mockCuentaRepositorio.Setup(o => o.listarCuentasDolares()).Returns(new List<Cuenta>());

            var indexT = new CuentaController(null, mockCuentaRepositorio.Object);
            var view = indexT.Index();

            Assert.IsNotNull(view);
        }

        [Test]
        public void CrearGetViewCase01()
        {
            var mockCuentaRepositorio = new Mock<ICuentaRepositorio>();
            mockCuentaRepositorio.Setup(o => o.listarTipos()).Returns(new List<Tipo>());

            var crearT = new CuentaController(null, mockCuentaRepositorio.Object);
            var view = crearT.Crear();

            Assert.IsNotNull(view);
        }

        [Test]
        public void CrearPostViewCase01()
        {
            var mockCuentaRepositorio = new Mock<ICuentaRepositorio>();
            mockCuentaRepositorio.Setup(o => o.listarTipos()).Returns(new List<Tipo>());
            mockCuentaRepositorio.Setup(o => o.guardarCuenta(new Cuenta()));

            var crearT = new CuentaController(null, mockCuentaRepositorio.Object);
            var view = crearT.Crear(new Cuenta());

            Assert.IsNotNull(view);
            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }

        [Test]
        public void GastoGetViewCase01()
        {
            var mockCuentaRepositorio = new Mock<ICuentaRepositorio>();
            mockCuentaRepositorio.Setup(o => o.obtenerSaldo(3)).Returns(0.0m);

            var gastoT = new CuentaController(null, mockCuentaRepositorio.Object);
            var view = gastoT.Gasto(3);

            Assert.IsNotNull(view);
        }

        [Test]
        public void GastoPostViewCase01()
        {
            var mockCuentaRepositorio = new Mock<ICuentaRepositorio>();
            mockCuentaRepositorio.Setup(o => o.obtenerSaldo(3)).Returns(0.0m);
            mockCuentaRepositorio.Setup(o => o.guardarGasto(3, new Transaccion()));

            var gastoT = new CuentaController(null, mockCuentaRepositorio.Object);
            var view = gastoT.Gasto(3, new Transaccion());

            Assert.IsNotNull(view);
        }

        [Test]
        public void IngresoGetViewCase01()
        {
            var ingresoT = new CuentaController(null, null);
            var view = ingresoT.Ingreso(3);

            Assert.IsNotNull(view);
        }

        [Test]
        public void IngresoPostViewCase01()
        {
            var mockCuentaRepositorio = new Mock<ICuentaRepositorio>();
            mockCuentaRepositorio.Setup(o => o.guardarIngreso(3, new Transaccion()));

            var ingresoT = new CuentaController(null, null);
            var view = ingresoT.Ingreso(3);

            Assert.IsNotNull(view);
        }

        [Test]
        public void MovimientosViewCase01()
        {
            var movimientosT = new CuentaController(null, null);
            var view = movimientosT.Movimientos(3);

            Assert.IsNotNull(view);
        }

        [Test]
        public void VerGastosViewCase01()
        {
            var mockCuentaRepositorio = new Mock<ICuentaRepositorio>();
            mockCuentaRepositorio.Setup(o => o.obtenerGastos(3)).Returns(new List<Transaccion>());

            var gastosT = new CuentaController(null, mockCuentaRepositorio.Object);
            var view = gastosT.VerGastos(3);

            Assert.IsNotNull(view);
        }

        [Test]
        public void VerIngresosViewCase01()
        {
            var mockCuentaRepositorio = new Mock<ICuentaRepositorio>();
            mockCuentaRepositorio.Setup(o => o.obtenerIngresos(3)).Returns(new List<Transaccion>());

            var ingresoT = new CuentaController(null, mockCuentaRepositorio.Object);
            var view = ingresoT.VerIngresos(3);

            Assert.IsNotNull(view);
        }
    }
}
