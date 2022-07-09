using Microsoft.AspNetCore.Mvc;
using N00193217.Web.Models;
using N00193217.Web.DB;
using N00193217.Web.Repositorio;

namespace N00193217.Web.Controllers
{
    public class CuentaController : Controller
    {
        private readonly DbEntities _dbEntities;
        private readonly ICuentaRepositorio _cuentaRepositorio;

        public CuentaController(DbEntities dbEntities, ICuentaRepositorio cuentaRepositorio)
        {
            _dbEntities = dbEntities;
            _cuentaRepositorio = cuentaRepositorio;
        }

        public IActionResult Index()
        {
            ViewBag.ListaCuentas = _cuentaRepositorio.listarCuentas();
            ViewBag.MontoSoles = CalcularTotalSoles();
            ViewBag.MontoDolares = CalcularTotalDolares();
            ViewBag.Conversion = CalcularTotalDolares() * 3.90m;
            return View();
        }

        public decimal CalcularTotalSoles()
        {
            List<Cuenta> cuentas = _cuentaRepositorio.listarCuentasSoles();

            decimal monto = 0.0m;

            foreach(var montos in cuentas)
            {
                monto += montos.SaldoInicial;
            }

            return monto;
        }

        public decimal CalcularTotalDolares()
        {
            List<Cuenta> cuentas = _cuentaRepositorio.listarCuentasDolares();

            decimal monto = 0.0m;

            foreach (var montos in cuentas)
            {
                monto += montos.SaldoInicial;
            }

            return monto;
        }

        [HttpGet]
        public IActionResult Crear()
        {
            ViewBag.Tipos = _cuentaRepositorio.listarTipos();
            return View(new Cuenta());
        }

        [HttpPost]
        public IActionResult Crear(Cuenta cuenta)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Tipos = _cuentaRepositorio.listarTipos();
                return View(cuenta);
            }
            if(cuenta.Tipo == "Efectivo" || cuenta.Tipo == "Tarjeta de Debito") cuenta.Categoria = "Propio";
            else cuenta.Categoria = "Credito";
            _cuentaRepositorio.guardarCuenta(cuenta);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Gasto(int IdCuenta)
        {
            ViewBag.CuentaId = IdCuenta;
            ViewBag.Saldo = _cuentaRepositorio.obtenerSaldo(IdCuenta);
            return View(new Transaccion());
        }

        [HttpPost]
        public IActionResult Gasto(int IdCuenta, Transaccion transaccion)
        {
            if (transaccion.Descripcion == null || transaccion.Monto < 0.0m)
            {
                ViewBag.CuentaId = IdCuenta;
                ViewBag.Saldo = _cuentaRepositorio.obtenerSaldo(IdCuenta);
                return View(transaccion);
            }
            Cuenta cuenta = _cuentaRepositorio.obtenerCuenta(IdCuenta);
            if(cuenta.Tipo != "Tarjeta de Crédito")
            {
                if (cuenta.SaldoInicial >= transaccion.Monto)
                {
                    transaccion.IdCuenta = IdCuenta;
                    transaccion.Tipo = "Gasto";
                    transaccion.Fecha = DateTime.Now;
                    _cuentaRepositorio.guardarGasto(IdCuenta, transaccion);
                    return RedirectToAction("Index");
                }
                else ModelState.AddModelError("Mensaje", "Saldo en cuenta Insuficiente");
                ViewBag.CuentaId = IdCuenta;
                ViewBag.Saldo = _cuentaRepositorio.obtenerSaldo(IdCuenta);
                return View("Gasto", transaccion);
            }
            else
            {
                transaccion.IdCuenta = IdCuenta;
                transaccion.Tipo = "Gasto";
                transaccion.Fecha = DateTime.Now;
                _cuentaRepositorio.guardarGasto(IdCuenta, transaccion);
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult Ingreso(int IdCuenta)
        {
            ViewBag.CuentaId = IdCuenta;
            return View(new Transaccion());
        }

        [HttpPost]
        public IActionResult Ingreso(int IdCuenta, Transaccion transaccion)
        {
            if (transaccion.Descripcion == null || transaccion.Monto < 0.0m)
            {
                ViewBag.CuentaId = IdCuenta;
                return View(transaccion);
            }
            transaccion.IdCuenta = IdCuenta;
            transaccion.Tipo = "Ingreso";
            transaccion.Fecha = DateTime.Now;
            _cuentaRepositorio.guardarIngreso(IdCuenta, transaccion);

            return RedirectToAction("Index"); ;
        }
        public IActionResult Movimientos(int IdCuenta)
        {
            ViewBag.CuentaId = IdCuenta;
            return View();
        }

        public IActionResult VerGastos(int IdCuenta)
        {
            ViewBag.Gastos = _cuentaRepositorio.obtenerGastos(IdCuenta);
            return View();
        }

        public IActionResult VerIngresos(int IdCuenta)
        {
            ViewBag.Ingresos = _cuentaRepositorio.obtenerIngresos(IdCuenta);
            return View();
        }
    }
}
