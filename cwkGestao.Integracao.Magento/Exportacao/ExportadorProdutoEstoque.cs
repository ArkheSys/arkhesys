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
    public class ExportadorProdutoEstoque : IExportador<Nota>
    {
        private Comunicador comunicador;

        public ExportadorProdutoEstoque(Comunicador comunicador)
        {
            this.comunicador = comunicador;
        }

        public void Exportar(Nota nota)
        {
            comunicador.AlterarProdutoEstoque(nota);

            //NotaController.Instancia.Salvar(nota, Acao.Alterar);
        }

        public void ExportarBalanco(Balanco Balanco)
        {
            comunicador.AlterarEstoqueBalanco(Balanco);
        }
    }
}