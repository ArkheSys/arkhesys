using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioFilial : IRepositorio<Filial>
    {
        Modelo.Filial GetFilialPrincipal();

        Modelo.Filial GetFilialbyId(int IdFilial);

        IList<Modelo.Filial> GetEmpresasDoGrupo(int IDGrupo);
        IList<pxyListaEstoque> GetListaEstoque(int idFilial);
        IList<pxyEstoqueInventario> GetEstoquesInventario(Filial filial, DateTime data, TipoValorProduto tipo);
        Modelo.Filial GetPorTipo(string tipo);
    }
}
