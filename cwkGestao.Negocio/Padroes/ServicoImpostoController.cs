using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class ServicoImpostoController : BaseController<ServicoImposto>
    {
        private static ServicoImpostoController instance = new ServicoImpostoController();
        private static IRepositorioServicoImposto repositorioServicoImposto;

        public static ServicoImpostoController Instancia { get { return instance; } }

        static ServicoImpostoController()
        {
            repositorioT = RepositorioFactory<ServicoImposto>.GetRepositorio();
            repositorioServicoImposto = (IRepositorioServicoImposto) repositorioT;
        }
        public override Dictionary<string, string> ValidaObjeto(ServicoImposto objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<object> GetByInicial(string inicial, string final)
        {
            return repositorioServicoImposto.GetByInicial(inicial, final);
        }
    }
}
