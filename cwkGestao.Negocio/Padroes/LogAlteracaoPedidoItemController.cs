using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class LogAlteracaoPedidoItemController : BaseController<LogAlteracaoPedidoItem>
	{
		#region Singleton
        private static LogAlteracaoPedidoItemController instance;
        private static IRepositorioLogAlteracaoPedidoItem repositorioLogAlteracaoPedidoItem;

        private LogAlteracaoPedidoItemController()
        { }
		
		static LogAlteracaoPedidoItemController()
        {
            instance = new LogAlteracaoPedidoItemController();
            repositorioT = RepositorioFactory<LogAlteracaoPedidoItem>.GetRepositorio();
            repositorioLogAlteracaoPedidoItem = (IRepositorioLogAlteracaoPedidoItem)repositorioT;
        }
			
		public static LogAlteracaoPedidoItemController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(LogAlteracaoPedidoItem objeto)
        {
            return new Dictionary<string, string>();
        }


        public IList<LogAlteracaoPedidoItem> LoadObjectByIdItem(int ID, string Campo)
        {
            return repositorioLogAlteracaoPedidoItem.LoadObjectByIdItem(ID, Campo);
        }

        public IList<LogAlteracaoPedidoItem> LoadObjectByData(DateTime dataInicio, DateTime dataFim)
        {
            return repositorioLogAlteracaoPedidoItem.LoadObjectByData(dataInicio, dataFim);
        }

        public void DeleteObjectByIdItem(int ID, string Campo)
        {
            repositorioLogAlteracaoPedidoItem.DeleteObjectByIdItem(ID, Campo);
        }
	}
}
