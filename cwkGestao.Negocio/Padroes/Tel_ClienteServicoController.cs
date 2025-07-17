using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Tel_ClienteServicoController : BaseController<Tel_ClienteServico>
	{
		#region Singleton
        private static Tel_ClienteServicoController instance;
        private static IRepositorioTel_ClienteServico repositorioTel_ClienteServico;

        private Tel_ClienteServicoController()
        { }
		
		static Tel_ClienteServicoController()
        {
            instance = new Tel_ClienteServicoController();
            repositorioT = RepositorioFactory<Tel_ClienteServico>.GetRepositorio();
            repositorioTel_ClienteServico = (IRepositorioTel_ClienteServico)repositorioT;
        }
			
		public static Tel_ClienteServicoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Tel_ClienteServico objeto)
        {
            return new Dictionary<string, string>();
        }

        internal IList<Tel_ClienteServico> GetByGrupoDataLimiteEReferencia(Tel_GrupoCliente grupoCliente, DateTime dataLimite, DateTime referencia)
        {
            return repositorioTel_ClienteServico.GetByGrupoDataLimiteEReferencia(grupoCliente, dataLimite, referencia);
        }

        public void AtualizaValorServicoCliente(int IdTelServico, decimal ValorNovo)
        {
            repositorioTel_ClienteServico.AtualizaValorServicoCliente(IdTelServico, ValorNovo);
        } 
    }
}
