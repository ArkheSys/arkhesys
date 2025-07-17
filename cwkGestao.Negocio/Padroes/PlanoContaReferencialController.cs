using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Repositorio;

namespace cwkGestao.Negocio.Padroes
{
    public class PlanoContaReferencialController : BaseController<PlanoContaReferencial>
    {
        private static PlanoContaReferencialController instance;
        private static IRepositorioPlanoContaReferencial repositorioPlanoContaReferencial;

        private PlanoContaReferencialController()
        { }

        static PlanoContaReferencialController()
        {
            instance = new PlanoContaReferencialController();
            repositorioT = RepositorioFactory<PlanoContaReferencial>.GetRepositorio();
            repositorioPlanoContaReferencial = (IRepositorioPlanoContaReferencial)repositorioT;
        }
			
		public static PlanoContaReferencialController Instancia
        {
            get
            {
                return instance;
            }
        }

        public override Dictionary<string, string> ValidaObjeto(PlanoContaReferencial objeto)
        {
            return new Dictionary<string, string>();
        }

#pragma warning disable CS0114 // "PlanoContaReferencialController.GetAll()" oculta o membro herdado "BaseController<PlanoContaReferencial>.GetAll()". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public IList<PlanoContaReferencial> GetAll()
#pragma warning restore CS0114 // "PlanoContaReferencialController.GetAll()" oculta o membro herdado "BaseController<PlanoContaReferencial>.GetAll()". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        {
            return CorrigirDatas(repositorioPlanoContaReferencial.GetAll());
        }

        public PlanoContaReferencial HqlLoadByCode(int p)
        {
            return repositorioPlanoContaReferencial.HqlLoadByCode(p);
        }

        public IList<PlanoContaReferencial> CorrigirDatas(IList<PlanoContaReferencial> lista)
        {
            IList<PlanoContaReferencial> lp = new List<PlanoContaReferencial>();

            foreach (PlanoContaReferencial item in lista)
            {
                if (item.inicioValidade.Equals(01/01/0001) || item.inicioValidade.Equals(01/01/1900))
                {
                    item.inicioValidade = null;
                }
                if (item.fimValidade.Equals(01/01/0001) || item.fimValidade.Equals(01/01/1900))
                {
                    item.fimValidade = null;
                }

                lp.Add(item);
                
            }
            return lp;
        }
    }
}
