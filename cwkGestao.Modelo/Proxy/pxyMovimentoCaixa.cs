using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyMovimentoCaixa
    {
        public pxyMovimentoCaixa() { }

        public pxyMovimentoCaixa(string origem, string doc, string pessoa, string historico, string planoConta, 
            string formaPagamento, Ent_SaiMovCaixa ent_sai, DebCreType deb_cre, TipoDocumentoType tipo_doc, 
            TipoMovimentoType tipo_mov, decimal valor)
        {
            Origem = origem;
            Doc = doc;
            Pessoa = pessoa;
            Historico = historico;
            PlanoConta = planoConta;
            FormaPagamento = formaPagamento;
            Ent_Sai = ent_sai;
            Deb_Cre = deb_cre;
            Valor = valor;
            TipoDoc = tipo_doc;
            TipoMov = tipo_mov;
        }

        public string Origem { get; set; }
        public string Doc { get; set; }
        public string Pessoa { get; set; }
        public string Historico { get; set; }
        public string PlanoConta { get; set; }
        public string FormaPagamento { get; set; }
        public Ent_SaiMovCaixa Ent_Sai { get; set; }
        public DebCreType Deb_Cre { get; set; }
        public TipoDocumentoType TipoDoc { get; set; }
        public TipoMovimentoType TipoMov { get; set; }
        public decimal Valor { get; set; }
    }
}
