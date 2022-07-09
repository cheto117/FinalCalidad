using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Finanzas.Models
{
    public class Cuenta
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int IdCategoria { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; }
        public decimal Saldo { get; set; }
        public decimal Limite { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int IdMoneda { get; set; }
        public Categoria Categoria { get; set; }
        public Moneda Moneda { get; set; }
        public List<Transaccion> Transaccions { get; set; }
    }
}
