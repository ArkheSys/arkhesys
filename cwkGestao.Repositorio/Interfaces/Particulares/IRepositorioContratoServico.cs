using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioContratoServico : IRepositorio<ContratoServico>
    {
        IList<ContratoServico> GetContratosParaFaturar(DateTime referencia, Int32 vencimento);
        IList<ContratoServico> GetContratosServicoPorIDContrato(int idContrato);
    }
}

