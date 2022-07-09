using Finanzas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Repositorio
{
    public interface ITransaccionRepositorio
    {
        List<Transaccion> GetTransaccions(int id);
        Cuenta GetCuenta(int id);
        List<Tipo> GetTipos();
        void SaveTransaccion(Transaccion transaccion);
    }
    public class TransaccionRepositorio : ITransaccionRepositorio
    {
        private readonly ContextoFinanzas contexto;

        public TransaccionRepositorio(ContextoFinanzas contexto)
        {
            this.contexto = contexto;
        }

        public Cuenta GetCuenta(int id)
        {
            return contexto.Cuentas.First(o => o.Id == id);
        }

        public List<Tipo> GetTipos()
        {
            return contexto.Tipos.ToList();
        }

        public List<Transaccion> GetTransaccions(int id)
        {
            return contexto.Transaccions.
                Include(o => o.Tipo).
                Where(o => o.IdCuenta == id).ToList();
        }

        public void SaveTransaccion(Transaccion transaccion)
        {
            contexto.Transaccions.Add(transaccion);
            contexto.SaveChanges();
            ModificaMontoCuenta(transaccion.IdCuenta);
        }
        private void ModificaMontoCuenta(int cuentaId)
        {
            var cuenta = contexto.Cuentas
                .Include(o => o.Transaccions)
                .FirstOrDefault(o => o.Id == cuentaId);

            var total = cuenta.Transaccions.Sum(o => o.Monto);
            cuenta.Saldo = total;
            contexto.SaveChanges();
        }
    }
}
