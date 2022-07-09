using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using N00193217.Web.DB;
using N00193217.Web.Models;
using N00193217.Web.Repositorio;
using N00193217.Test.Helpers;

namespace N00193217.Test.Repositories
{
    public class CuentaRepositorioTest
    {
        private IQueryable<Cuenta> data;
        private IQueryable<Transaccion> data2;
        private IQueryable<Tipo> data3;

        [SetUp]
        public void SetUp()
        {
            data = new List<Cuenta>
            {
                new() { Id = 1, Nombre = "Cuenta 01", Categoria = "Propio", Moneda = "Soles", SaldoInicial = 500.0m, Tipo = "Efectivo"},
                new() { Id = 2, Nombre = "Cuenta 02", Categoria = "Credito", Moneda = "Soles", SaldoInicial = 500.0m, Tipo = "Tarjeta de Crédito"},
                new() { Id = 3, Nombre = "Cuenta 03", Categoria = "Propio", Moneda = "Dolares", SaldoInicial = 500.0m, Tipo = "Efectivo"},
            }.AsQueryable();

            data2 = new List<Transaccion>
            {
                new() {Id = 1, Descripcion = "Compra Pantalon", IdCuenta = 2, Monto = 50.0m, Tipo = "Gasto"},
                new() {Id = 2, Descripcion = "Sueldo", IdCuenta = 1, Monto = 1500.0m, Tipo = "Ingreso"},
                new() {Id = 3, Descripcion = "Compra Pantalon", IdCuenta = 3, Monto = 50.0m, Tipo = "Gasto"},
            }.AsQueryable();

            data3 = new List<Tipo>
            {
                new() {Id = 1, Descripcion = "Efectivo", IdCategoria = 1},
                new() {Id = 2, Descripcion = "Tarjeta de Debito", IdCategoria = 1},
                new() {Id = 3, Descripcion = "Tarjeta de Crédito", IdCategoria = 2},
                new() {Id = 4, Descripcion = "Préstamo", IdCategoria = 2},
            }.AsQueryable();
        }

        [Test]
        public void listarCuentasViewCase01()
        {
            var mockDbSetCuenta = new MockDBSet<Cuenta>(data);
            var mockDb = new Mock<DbEntities>();
            mockDb.Setup(o => o.cuentas).Returns(mockDbSetCuenta.Object);

            var listarT = new CuentaRepositorio(mockDb.Object);
            var result = listarT.listarCuentas();

            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public void listarCuentasSolesViewCase01()
        {
            var mockDbSetCuenta = new MockDBSet<Cuenta>(data);
            var mockDb = new Mock<DbEntities>();
            mockDb.Setup(o => o.cuentas).Returns(mockDbSetCuenta.Object);

            var listarT = new CuentaRepositorio(mockDb.Object);
            var result = listarT.listarCuentasSoles();

            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void listarCuentasDolaresViewCase01()
        {
            var mockDbSetCuenta = new MockDBSet<Cuenta>(data);
            var mockDb = new Mock<DbEntities>();
            mockDb.Setup(o => o.cuentas).Returns(mockDbSetCuenta.Object);

            var listarT = new CuentaRepositorio(mockDb.Object);
            var result = listarT.listarCuentasDolares();

            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void listarTiposViewCase01()
        {
            var mockDbSetCuenta = new MockDBSet<Tipo>(data3);
            var mockDb = new Mock<DbEntities>();
            mockDb.Setup(o => o.tipos).Returns(mockDbSetCuenta.Object);

            var listarT = new CuentaRepositorio(mockDb.Object);
            var result = listarT.listarTipos();

            Assert.AreEqual(4, result.Count);
        }

        [Test]
        public void obtenerGastosViewCase01()
        {
            var mockDbSetCuenta = new MockDBSet<Transaccion>(data2);
            var mockDb = new Mock<DbEntities>();
            mockDb.Setup(o => o.transaccions).Returns(mockDbSetCuenta.Object);

            var listarT = new CuentaRepositorio(mockDb.Object);
            var result = listarT.obtenerGastos(1);

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void obtenerIngresosViewCase01()
        {
            var mockDbSetCuenta = new MockDBSet<Transaccion>(data2);
            var mockDb = new Mock<DbEntities>();
            mockDb.Setup(o => o.transaccions).Returns(mockDbSetCuenta.Object);

            var listarT = new CuentaRepositorio(mockDb.Object);
            var result = listarT.obtenerIngresos(1);

            Assert.AreEqual(1, result.Count);
        }
    }
}
