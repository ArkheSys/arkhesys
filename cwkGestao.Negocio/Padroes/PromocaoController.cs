using System.Collections.Generic;

using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class PromocaoController : BaseController<Promocao>
    {
        private static PromocaoController instance;
        private static IRepositorioPromocao repositorioPromocao;

        private PromocaoController()
        { }

        static PromocaoController()
        {
            instance = new PromocaoController();
            repositorioT = RepositorioFactory<Promocao>.GetRepositorio();
            repositorioPromocao = (IRepositorioPromocao)repositorioT;
        }

        public static PromocaoController Instancia
        {
            get
            {
                return instance;
            }
        }

        public override Dictionary<string, string> ValidaObjeto(Promocao objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}