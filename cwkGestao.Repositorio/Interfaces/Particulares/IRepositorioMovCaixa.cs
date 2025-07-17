using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioMovCaixa : IRepositorio<MovCaixa>
    {
        IList<pxySaldoBanco> GetSaldoBancos(IList<Modelo.Banco> bancos, DateTime data);

        IList<MovCaixa> GetPorBancoEData(Banco banco, DateTime dataInicial, DateTime dataFinal);

        IList<pxyMovimentoCaixa> GetRelatorioDeMovimentoCaixa(Modelo.Banco banco, DateTime dataInicial, DateTime dataFinal);

        IList<pxyMovCaixaPorFluxoCaixa> GetMovimentosPorFluxoCaixa(int IDFluxoCaixa);
        int GetNovoCodigoMovimento();
    }
}

