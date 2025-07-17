using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioTel_FaturaLigacao : IRepositorio<Tel_FaturaLigacao>
    {
        IList<cwkGestao.Modelo.Proxy.pxyTel_FaturaLigacao> GetLigacoes(int IDLigacao);

        IList<cwkGestao.Modelo.Proxy.pxyTel_FaturaLigacao> GetByGrupoEReferencia(Tel_GrupoCliente grupoCliente, DateTime referencia);

        IList<cwkGestao.Modelo.Proxy.pxyTel_FaturaLigacao> GetByFaturas(IList<int> idsFaturas);


        IList<Tel_Ligacao> GetAllLigacoesPorIDArquivo(int id);
    }
}

