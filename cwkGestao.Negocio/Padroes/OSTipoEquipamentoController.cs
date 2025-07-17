using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Repositorio;

namespace cwkGestao.Negocio
{
    public class OSTipoEquipamentoController : BaseController<OSTipoEquipamento>
    {
        #region Singleton
        private static OSTipoEquipamentoController instance;
        private static IRepositorioOSTipoEquipamento repositorioOSTipoEquipamento;

        private OSTipoEquipamentoController()
        { }

        static OSTipoEquipamentoController()
        {
            instance = new OSTipoEquipamentoController();
            repositorioT = RepositorioFactory<OSTipoEquipamento>.GetRepositorio();
            repositorioOSTipoEquipamento = (IRepositorioOSTipoEquipamento)repositorioT;
        }

        public static OSTipoEquipamentoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public override Dictionary<string, string> ValidaObjeto(OSTipoEquipamento objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
