using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Serviço")]
    public class Tel_Servico : ModeloBase
	{
        public Tel_Servico()
        {
        }

        public override int ID { get; set; }
        
        [InformacaoExibicao(1, "Código", 85, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "Descrição", 300, InformacaoExibicao.HAlinhamento.Esquerda)]
        public virtual string Descricao { get; set; }

        public virtual byte Operacao { get; set; }

        [InformacaoExibicao(3, "Operação", 85, InformacaoExibicao.HAlinhamento.Esquerda)]
        public virtual string OperacaoDesc 
        {
            get
            {
                if (Operacao == 0)
                    return "Crédito";
                else
                    return "Débito";
            }
        }

        [InformacaoExibicao(4, "Valor", 120, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Valor { get; set; }
    }
}
		