using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class CidadeController : BaseController<Cidade>
    {
        #region Singleton
        private static CidadeController instance;
        private static IRepositorioCidade repositorioCidade;

        private CidadeController()
        { }

        static CidadeController()
        {
            instance = new CidadeController();
            repositorioT = RepositorioFactory<Cidade>.GetRepositorio();
            repositorioCidade = (IRepositorioCidade)repositorioT;
        }

        public static CidadeController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Cidade objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (objeto.Codigo <= 0)
                ret.Add("txtCodigo", "Valor tem que ser maior que 0 (Zero).");

            if (String.IsNullOrEmpty(objeto.Nome) || objeto.Nome.Trim() == String.Empty)
                ret.Add("txtNome", "Campo obrigatório.");

            return ret;
        }

        public System.Collections.Hashtable GetHashNomeCidade()
        {
            return repositorioCidade.GetHashNomeCidade();
        }

        public Cidade GetByLikeName(string nome, string UF)
        {
            return repositorioCidade.GetByLikeName(nome, UF);
        }

        public Cidade GetCidadePorIbge(string CodigoIbge)
        {
            return repositorioCidade.GetCidadePorIbge(CodigoIbge);
        }
    }
}
