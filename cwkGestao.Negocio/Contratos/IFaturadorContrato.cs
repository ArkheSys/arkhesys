using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using System.Collections;
using cwkGestao.Negocio.Faturamento;

namespace cwkGestao.Negocio.Contratos
{
    public interface IFaturadorContrato<T> where T : ModeloBase, new()
    {
        bool VerificaSeGeraNotaNoMes(DateTime dataDaNota, IList<ContratoControle> contratosControle);
        void GerarFaturas(ref bool sucesso);
        IList<ContratoControle> BuscaContratosControle(T itemContratos);
    }
}
