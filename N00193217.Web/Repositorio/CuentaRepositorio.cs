using N00193217.Web.DB;
using N00193217.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace N00193217.Web.Repositorio
{
    public interface ICuentaRepositorio
    {
        List<Cuenta> listarCuentas();
        List<Cuenta> listarCuentasSoles();
        List<Cuenta> listarCuentasDolares();
        List<Tipo> listarTipos();
        void guardarCuenta(Cuenta cuenta);
        decimal obtenerSaldo(int IdCuenta);
        Cuenta obtenerCuenta(int IdCuenta);
        void guardarGasto(int IdCuenta, Transaccion transaccion);
        void guardarIngreso(int IdCuenta, Transaccion transaccion);
        List<Transaccion> obtenerGastos(int IdCuenta);
        List<Transaccion> obtenerIngresos(int IdCuenta);
    }
    public class CuentaRepositorio : ICuentaRepositorio
    {
        private readonly DbEntities _dbEntities;

        public CuentaRepositorio(DbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public List<Cuenta> listarCuentas()
        {
            return _dbEntities.cuentas.ToList();
        }

        public List<Cuenta> listarCuentasSoles()
        {
            return _dbEntities.cuentas
                                .Where(o => o.Moneda == "Soles" && o.Tipo != "Tarjeta de Crédito")
                                .ToList();
        }
        public List<Cuenta> listarCuentasDolares()
        {
            return _dbEntities.cuentas
                                .Where(o => o.Moneda == "Dolares" && o.Tipo != "Tarjeta de Crédito")
                                .ToList();
        }

        public List<Tipo> listarTipos()
        {
            return _dbEntities.tipos.ToList();
        }

        public void guardarCuenta(Cuenta cuenta)
        {
            _dbEntities.cuentas.Add(cuenta);
            _dbEntities.SaveChanges();
        }

        public decimal obtenerSaldo(int IdCuenta)
        {
            return _dbEntities.cuentas.FirstOrDefault(o => o.Id == IdCuenta).SaldoInicial;
        }

        public Cuenta obtenerCuenta(int IdCuenta)
        {
            return _dbEntities.cuentas.FirstOrDefault(o => o.Id == IdCuenta);
        }

        public void guardarGasto(int IdCuenta, Transaccion transaccion)
        {
            Cuenta cuenta = _dbEntities.cuentas.FirstOrDefault(o => o.Id == IdCuenta);
            cuenta.SaldoInicial -= transaccion.Monto;
            _dbEntities.transaccions.Add(transaccion);
            _dbEntities.SaveChanges();
        }

        public void guardarIngreso(int IdCuenta, Transaccion transaccion)
        {
            Cuenta cuenta = _dbEntities.cuentas.FirstOrDefault(o => o.Id == IdCuenta);
            cuenta.SaldoInicial += transaccion.Monto;
            _dbEntities.transaccions.Add(transaccion);
            _dbEntities.SaveChanges();
        }

        public List<Transaccion> obtenerGastos(int IdCuenta)
        {
            return _dbEntities.transaccions
                                    .Where(o => o.IdCuenta == IdCuenta && o.Tipo == "Gasto")
                                    .ToList();
        }

        public List<Transaccion> obtenerIngresos(int IdCuenta)
        {
            return _dbEntities.transaccions
                                    .Where(o => o.IdCuenta == IdCuenta && o.Tipo == "Ingreso")
                                    .ToList();
        }
    }
}
