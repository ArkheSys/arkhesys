using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioContratoControle : IRepositorio<ContratoControle>
    {
        IList<ContratoControle> GetRegistrosContratosFaturados(int idContrato, int tpProdutoServico, int idItemContrato);

        ContratoControle LoadObjectByIDNotaItem(int idNotaItem);

        ContratoControle LoadObjectByIDNotaItemServico(int idNotaItemServico);
    }
}

