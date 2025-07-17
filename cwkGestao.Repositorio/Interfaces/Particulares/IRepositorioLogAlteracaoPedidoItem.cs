using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioLogAlteracaoPedidoItem : IRepositorio<LogAlteracaoPedidoItem>
    {
        IList<LogAlteracaoPedidoItem> LoadObjectByIdItem(int ID, string Campo);
        IList<LogAlteracaoPedidoItem> LoadObjectByData(DateTime dataInicio, DateTime dataFim);
        void DeleteObjectByIdItem(int ID, string Campo);
    }
}

