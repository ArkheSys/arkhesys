using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using System.Collections;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioOSOrdemServico : IRepositorio<OSOrdemServico>
    {
        IList<object> GetListagemOrdemServico(int pStatus, string pCliente, DateTime pDataInicial, DateTime pDataFinal, string pEmpresas);

        IList<object> GetOrdemServicoImpressao(int pIDOS);

        OSOrdemServico GetOSporCodigo(int pCodOS);

        OSOrdemServico LoadFullObjectByID(int id);

        IList<Modelo.Proxy.pxyFaturamentoOS> GetFaturamento(Pessoa pessoa, Filial filial, DateTime dataInicial, DateTime dataFinal);


        List<OSOrdemServico> Get(List<int> ids);
        bool EstahAgendado(OSOrdemServico selecionado);

        TimeSpan GetTotHorasFaturadas(int idPessoa, DateTime dtInicio, DateTime dtFim);
    }
}
