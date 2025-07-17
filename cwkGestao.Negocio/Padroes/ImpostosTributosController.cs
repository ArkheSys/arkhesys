using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Negocio.Padroes
{
    public class ImpostosTributosController : BaseController<ImpostosTributos>
    {
        #region Singleton
        private static ImpostosTributosController instance;
        private static IRepositorioImpostosTributos repositorioImpostosTributos;

        private ImpostosTributosController()
        { }

        static ImpostosTributosController()
        {
            instance = new ImpostosTributosController();
            repositorioT = RepositorioFactory<ImpostosTributos>.GetRepositorio();
            repositorioImpostosTributos = (IRepositorioImpostosTributos)repositorioT;
        }

        public static ImpostosTributosController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        //public IList<pxImpostosTributosExcessoes> GetTabelaImpostosTributosExcessoes(int IdImpostosTributos)
        //{
        //    ImpostosTributos impostosTributos = repositorioImpostosTributos.LoadObjectById(IdImpostosTributos);
        //    if (impostosTributos == null)
        //        return new List<pxImpostosTributosExcessoes>();

        //    var lista = from tp in impostosTributos.TabelaImpostosTributosExcessoes
        //                select new pxImpostosTributosExcessoes
        //                {
        //                    Codigo = tp.Codigo,
        //                    TabImpostosTributosExcessoes = tp,
        //                };

        //    return lista.ToList();
        //}


        public override Dictionary<string, string> ValidaObjeto(ImpostosTributos objeto)
        {
            return new Dictionary<string, string>();
        }

        public ImpostosTributos GetByClassificacaoFiscal(int pIDClassificacaoFiscal)
        {
            return repositorioImpostosTributos.GetByClassificacaoFiscal(pIDClassificacaoFiscal);
        }

    }
}
