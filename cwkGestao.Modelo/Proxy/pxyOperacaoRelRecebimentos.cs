using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyOperacaoRelRecebimentos
    {
        public pxyOperacaoRelRecebimentos(int operacao, string descricaoOperacao) 
        { 
            Operacao = operacao;
            DescricaoOperacao = descricaoOperacao;
        }

        public int Operacao { get; set; }
        public string DescricaoOperacao { get; set; }
    }
}
