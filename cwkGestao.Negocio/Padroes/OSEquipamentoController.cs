using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public partial class OSEquipamentoController : BaseController<OSEquipamento>
	{
		#region Singleton
        private static OSEquipamentoController instance;
        private static IRepositorioEquipamento repositorioOSEquipamento;

        private OSEquipamentoController()
        { }
		
		static OSEquipamentoController()
        {
            instance = new OSEquipamentoController();
            repositorioT = RepositorioFactory<OSEquipamento>.GetRepositorio();
            repositorioOSEquipamento = (IRepositorioEquipamento)repositorioT;
        }
			
		public static OSEquipamentoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(OSEquipamento objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<object> GetListagemEquipamentos(TipoRelatorioEquipamento tipo)
        {
            return repositorioOSEquipamento.GetListagemEquipamentos(tipo);
        }
	}
}
