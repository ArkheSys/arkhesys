using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class ArquivoRemessaController : BaseController<ArquivoRemessa>
	{
		#region Singleton
        private static ArquivoRemessaController instance;
        private static IRepositorioArquivoRemessa repositorioArquivoRemessa;

        private ArquivoRemessaController()
        { }
		
		static ArquivoRemessaController()
        {
            instance = new ArquivoRemessaController();
            repositorioT = RepositorioFactory<ArquivoRemessa>.GetRepositorio();
            repositorioArquivoRemessa = (IRepositorioArquivoRemessa)repositorioT;
        }

        public static ArquivoRemessaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ArquivoRemessa objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<ArquivoRemessa> GetArquivoRemessaPorData(DateTime dataInicial, DateTime dataFinal, int filtro, int IDEmpresa, int IDRemessa)
        {
            return repositorioArquivoRemessa.GetArquivoRemessaPorData(dataInicial, dataFinal, filtro, IDEmpresa, IDRemessa);
        }

        public int DevolveUltimoIDDocumento(IList<ArquivoRemessa> listaDeDocumentos)
        {
            if (listaDeDocumentos.Count == 0)
            {
                return 0;
            }
            else
            {
                var id = listaDeDocumentos.Max(q => q.NumRemessa);
                return id;
            }
        }
	}
}
