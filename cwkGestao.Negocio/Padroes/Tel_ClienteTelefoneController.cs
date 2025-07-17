using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Tel_ClienteTelefoneController : BaseController<Tel_ClienteTelefone>
	{
		#region Singleton
        private static Tel_ClienteTelefoneController instance;
        private static IRepositorioTel_ClienteTelefone repositorioTel_ClienteTelefone;

        private Tel_ClienteTelefoneController()
        { }
		
		static Tel_ClienteTelefoneController()
        {
            instance = new Tel_ClienteTelefoneController();
            repositorioT = RepositorioFactory<Tel_ClienteTelefone>.GetRepositorio();
            repositorioTel_ClienteTelefone = (IRepositorioTel_ClienteTelefone)repositorioT;
        }
			
		public static Tel_ClienteTelefoneController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Tel_ClienteTelefone objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<Tel_ClienteTelefone> GetAtivosByCliente(Tel_Cliente pessoa)
        {
            return repositorioTel_ClienteTelefone.GetAtivosByCliente(pessoa);
        }

        public void DesativarTelefones(IEnumerable<Tel_ClienteTelefone> telefones, DateTime data, string motivo, string contato)
        {
            foreach (var tel in telefones)
            {
                tel.DataDesligamento = data;
                tel.MotivoDesligamento = motivo;
                tel.ContatoPediuDesligamento = contato;
                RegisterNewIntoTransaction(tel);
            }
            CommitUnitOfWorkTransaction();
        }
    }
}
