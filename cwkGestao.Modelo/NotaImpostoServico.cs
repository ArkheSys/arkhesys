using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Imposto de Serviço")]
    public class NotaImpostoServico : ModeloBase
    {
        public override int ID { get; set; }
        public virtual Nota Nota { get; set; }
        /*public virtual Servico Servico { get; set; }*/
        [InformacaoExibicao(1, "Imposto",180)]
        public virtual ServicoImposto Imposto { get; set; }
        [InformacaoExibicao(4, "Total",100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Valor { get; set; }
        [InformacaoExibicao(2, "Base de Cálculo", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal BaseCalculo { get; set; }
        [InformacaoExibicao(3, "Alíquota", 50, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Porcentagem4Casas)]
        public virtual decimal Aliquota { get; set; }
        [InformacaoExibicao(0, "Ret.", 30)]
        public virtual bool BaseRetencaoImposto { get; set; }

        public static NotaImpostoServico CloneNewObject(NotaImpostoServico objNotaImpostoServico)
        {
            AutoMapper.Mapper.CreateMap<Modelo.NotaImpostoServico, Modelo.NotaImpostoServico>().ForMember(dest => dest.ID, o => o.Ignore());
            return AutoMapper.Mapper.Map<NotaImpostoServico, NotaImpostoServico>(objNotaImpostoServico);
        }

        public static void GetClones(ref Nota objNota)
        {
            IList<NotaImpostoServico> lstNotaImpostoServico = new List<NotaImpostoServico>(objNota.NotaImpostoServicos);
            objNota.NotaImpostoServicos = new List<NotaImpostoServico>();
            foreach (var notaImpostoServ in lstNotaImpostoServico)
            {
                NotaImpostoServico objNotaImpostoServico = NotaImpostoServico.CloneNewObject(notaImpostoServ);
                objNotaImpostoServico.Nota = objNota;

                objNota.NotaImpostoServicos.Add(objNotaImpostoServico);
            }
        }

    }
}
