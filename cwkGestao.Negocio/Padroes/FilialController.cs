using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Repositorio;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Negocio
{
    public partial class FilialController : BaseController<Filial>
    {
        #region Singleton

        private static FilialController instance;
        private static IRepositorioFilial repositorioFilial;

        private FilialController()
        { }

        static FilialController()
        {
            instance = new FilialController();
            repositorioT = RepositorioFactory<Filial>.GetRepositorio();
            repositorioFilial = (IRepositorioFilial)repositorioT;
        }

        public static FilialController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Filial objeto)
        {
            return new Dictionary<string, string>();
        }

        public Modelo.Filial GetFilialPrincipal()
        {
            return repositorioFilial.GetFilialPrincipal();
        }

        public Modelo.Filial GetFilialbyId(int IdFilial)
        {
            return repositorioFilial.GetFilialbyId(IdFilial);
        }

        public IList<Modelo.Filial> GetEmpresasDoGrupo(int IDGrupo)
        {
            return repositorioFilial.GetEmpresasDoGrupo(IDGrupo);
        }

        public IList<pxyListaEstoque> GetListaEstoque(int idFilial)
        {
            return repositorioFilial.GetListaEstoque(idFilial);
        }

        public IList<pxyEstoqueInventario> GetEstoquesInventario(Filial filial, DateTime data, TipoValorProduto tipo)
        {
            return repositorioFilial.GetEstoquesInventario(filial, data, tipo);
        }

        public Modelo.Filial GetPorTipo(string tipo)
        {
            return repositorioFilial.GetPorTipo(tipo);
        }

    }
}
