using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class TipoAtendimentoController : BaseController<TipoAtendimento>
    {
        #region Singleton
        private static TipoAtendimentoController instance;
        private static IRepositorioTipoAtendimento repositorioTipoAtendimento;

        private TipoAtendimentoController()
        { }

        static TipoAtendimentoController()
        {
            instance = new TipoAtendimentoController();
            repositorioT = RepositorioFactory<TipoAtendimento>.GetRepositorio();
            repositorioTipoAtendimento = (IRepositorioTipoAtendimento)repositorioT;
        }

        public static TipoAtendimentoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(TipoAtendimento objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (objeto.Codigo == 0)
            {
                ret.Add("txtCodigo", "Valor tem que ser maior do que zero (0).");
            }

            if (String.IsNullOrEmpty(objeto.Descricao) || objeto.Descricao.Trim() == String.Empty)
            {
                ret.Add("txtDescricao", "Campo obrigatório.");
            }
            return ret;
        }
    }
}
