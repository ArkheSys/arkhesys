using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class StatusOrdemServicoController : BaseController<StatusOrdemServico>
    {
        #region Singleton
        private static StatusOrdemServicoController instance;
        private static IRepositorioStatusOrdemServico repositorioStatusOrdemServico;

        private StatusOrdemServicoController()
        { }

        static StatusOrdemServicoController()
        {
            instance = new StatusOrdemServicoController();
            repositorioT = RepositorioFactory<StatusOrdemServico>.GetRepositorio();
            repositorioStatusOrdemServico = (IRepositorioStatusOrdemServico)repositorioT;
        }

        public static StatusOrdemServicoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(StatusOrdemServico objeto)
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
