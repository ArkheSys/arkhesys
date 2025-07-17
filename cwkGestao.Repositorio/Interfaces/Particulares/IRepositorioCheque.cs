using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioCheque : IRepositorio<Cheque> 
    {
        IList<ChequeHistorico> GetHistorico(Cheque cheque);

        IList<Cheque> GetChequesNaoUtilizados();

        IList<cwkGestao.Modelo.Proxy.pxyChequeSaldoRestante> GetChequesComSaldoRestante(int IDMovCaixaExcluido, int EntSai);

        IList<cwkGestao.Modelo.Proxy.pxyChequeSaldoRestante> GetChequesComSaldoRestante();
    }
}
