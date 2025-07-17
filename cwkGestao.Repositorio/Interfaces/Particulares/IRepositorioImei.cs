using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioImei : IRepositorio<Imei>
    {
        IList<Imei> GetAllByIDProduto(int IDProduto, bool bAtivo);
        IList<Imei> GetAllByIDNotaItemEntrada(int IDNotaItemEntrada, bool bAtivo);
        IList<Imei> GetAllByIDNotaItemSaida(int IDNotaItemSaida, bool bAtivo);
        IList<Imei> GetAllAtivos();
        IList<Imei> GetAllInativos();
        IList<Imei> GetAllByIDProdutoByIDNotaItem(Int32 IDProduto, Int32 IDNotaItem);
    }
}

