using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioTel_Ligacao : IRepositorio<Tel_Ligacao>
    {
        Dictionary<string, Tel_Ligacao> GetHashLigacoes();

        Dictionary<int, IList<Tel_Ligacao>> GetByReferenciaEClientes(DateTime referencia, IList<Tel_Cliente> clientes);
        Dictionary<string, Tel_Ligacao> GetHashLigacoesArquivo(DateTime referencia);
        IList<pxyTel_LigacaoCliente> GetByReferencia(DateTime referencia);
    }
}

