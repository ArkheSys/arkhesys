using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class Tel_TipoLigacaoPrecoController : BaseController<Tel_TipoLigacaoPreco>
    {
        #region Singleton
        private static Tel_TipoLigacaoPrecoController instance;
        private static IRepositorioTel_TipoLigacaoPreco repositorioTipoLigacaoPreco;

        private Tel_TipoLigacaoPrecoController()
        { }

        static Tel_TipoLigacaoPrecoController()
        {
            instance = new Tel_TipoLigacaoPrecoController();
            repositorioT = RepositorioFactory<Tel_TipoLigacaoPreco>.GetRepositorio();
            repositorioTipoLigacaoPreco = (IRepositorioTel_TipoLigacaoPreco)repositorioT;
        }

        public static Tel_TipoLigacaoPrecoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public override Dictionary<string, string> ValidaObjeto(Tel_TipoLigacaoPreco objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
