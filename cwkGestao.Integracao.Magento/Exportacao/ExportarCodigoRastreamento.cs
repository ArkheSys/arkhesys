using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Integracao.Magento.WebService;
using cwkGestao.Integracao.Magento.br.com.redfeet.cwork;
using cwkGestao.Negocio;

namespace cwkGestao.Integracao.Magento.Exportacao
{
    public class ExportarCodigoRastreamento : IExportador<ExpedicaoRastreamento>
    {
        private Comunicador comunicador;

        public ExportarCodigoRastreamento(Comunicador comunicador)
        {
            this.comunicador = comunicador;
        }

        public void Exportar(ExpedicaoRastreamento rastreamento)
        {
            comunicador.ExportarCodigoRastreamento(rastreamento);
        }
    }
}
