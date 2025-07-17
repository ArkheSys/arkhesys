using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class Vendedor : IDisposable
    {
        private static volatile Vendedor instancia = null;
        private Modelo.DataClassesDataContext db;

        public static Vendedor GetInstancia
        {
            get
            {
                if (instancia == null)
                {
                    lock (typeof(Vendedor))
                    {
                        if (instancia == null)
                        {
                            instancia = new Vendedor();
                        }
                    }
                }
                return instancia;
            }
        }

        private Vendedor()
        {
            db = new Modelo.DataClassesDataContext();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        /// <summary>
        /// Método para validar a senha do vendedor
        /// </summary>senha do gerente
        /// <param name="pVendedor">Qual o vendedor</param>
        /// <param name="pSenha">Senha informada</param>
        /// <returns>Retorna verdadeiro ou falso</returns>
        public bool ValidaSenha(int pVendedor, string pSenha)
        {
            bool ret = false;

            if (pSenha.Length != 0)
            {
                string senha = (from v in db.Vendedors
                                where v.IDPessoa == pVendedor
                                select v).Single().Senha;

                if (pSenha.TrimEnd() == senha.TrimEnd())
                    ret = true;
            }

            return (ret);
        }

        public bool PossuiSenha(int idvendedor)
        {
            string senha = (from v in db.Vendedors
                            where v.IDPessoa == idvendedor
                            select v).Single().Senha;

            return !String.IsNullOrEmpty(senha);
        }
    }
}
