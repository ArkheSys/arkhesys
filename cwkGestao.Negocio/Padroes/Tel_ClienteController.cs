using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public partial class Tel_ClienteController : BaseController<Tel_Cliente>
	{
		#region Singleton
        private static Tel_ClienteController instance;
        private static IRepositorioTel_Cliente repositorioTel_Cliente;

        private Tel_ClienteController()
        { }
		
		static Tel_ClienteController()
        {
            instance = new Tel_ClienteController();
            repositorioT = RepositorioFactory<Tel_Cliente>.GetRepositorio();
            repositorioTel_Cliente = (IRepositorioTel_Cliente)repositorioT;
        }
			
		public static Tel_ClienteController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Tel_Cliente objeto)
        {
            return new Dictionary<string, string>();
        }

        internal IList<Tel_Cliente> GetByGrupo(Tel_GrupoCliente grupoCliente)
        {
            return repositorioTel_Cliente.GetByGrupo(grupoCliente);
        }

        public Tel_Cliente GetPessoaID(int ID)
        {
            return repositorioTel_Cliente.GetPessoaID(ID);
        }

        public Tel_Cliente GetByTelefone(string telefone)
        {
            return repositorioTel_Cliente.GetByTelefone(telefone);
        }

        public IList<Tel_Cliente> GetAllByGrupo(Tel_GrupoCliente tel_GrupoCliente)
        {
            return repositorioTel_Cliente.GetAllByGrupo(tel_GrupoCliente);
        }
    }
}
