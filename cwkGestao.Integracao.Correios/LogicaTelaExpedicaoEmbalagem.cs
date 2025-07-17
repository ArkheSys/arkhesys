using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Negocio;
using cwkGestao.Modelo;

namespace cwkGestao.Integracao.Correios
{
    public class LogicaTelaExpedicaoEmbalagem
    {
        public Pedido Pedido { get; set; }
        private IList<CorreiosPrecoPrazo> ListaExclusoes { get; set; }
        private IList<CorreiosPrecoPrazo> ListaCadastrados { get; set; }
        public IList<RastreamentoCorreio> ListaCodRastreamentoDisponiveis { get; set; }

        public LogicaTelaExpedicaoEmbalagem(Pedido p)
        {

        }
    }
}
