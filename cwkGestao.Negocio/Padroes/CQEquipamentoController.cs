using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class CQEquipamentoController : BaseController<CQEquipamento>
	{
		#region Singleton
        private static CQEquipamentoController instance;
        private static IRepositorioCQEquipamento repositorioCQEquipamento;

        private CQEquipamentoController()
        { }
		
		static CQEquipamentoController()
        {
            instance = new CQEquipamentoController();
            repositorioT = RepositorioFactory<CQEquipamento>.GetRepositorio();
            repositorioCQEquipamento = (IRepositorioCQEquipamento)repositorioT;
        }
			
		public static CQEquipamentoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(CQEquipamento objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
