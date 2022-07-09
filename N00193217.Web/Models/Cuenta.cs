using N00193217.Web.Models;

namespace N00193217.Web.Models
{
    public class Cuenta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Categoria { get; set; }
        public string Tipo { get; set; }
        public decimal SaldoInicial { get; set; }
        public string Moneda { get; set; }
    }
}
