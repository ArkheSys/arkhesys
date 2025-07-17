using cwkGestao.Modelo.Auxiliares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Nota Serviço")]
#pragma warning disable CS0659 // "NotaItemServico" substitui Object.Equals(object o), mas não substitui Object.GetHashCode()
    public class NotaItemServico : ModeloBase
#pragma warning restore CS0659 // "NotaItemServico" substitui Object.Equals(object o), mas não substitui Object.GetHashCode()
    {
        public override int ID { get; set; }

        public virtual Nota Nota { get; set; }


        private Servico servico;
        public virtual Servico Servico 
        {
            get
            {
                return servico;
            }
            set
            {
                servico = value;
            }
        }

        [InformacaoExibicao(3, "Valor",75, cwkGestao.Modelo.InformacaoExibicao.HAlinhamento.Direita,cwkGestao.Modelo.InformacaoExibicao.Mascaras.Dinheiro )]
        public virtual decimal Valor { get; set; }
        [InformacaoExibicao(2, "Quantidade", 75, cwkGestao.Modelo.InformacaoExibicao.HAlinhamento.Direita, cwkGestao.Modelo.InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal Quantidade { get; set; }
        [InformacaoExibicao(5, "SubTotal", 83, cwkGestao.Modelo.InformacaoExibicao.HAlinhamento.Direita, cwkGestao.Modelo.InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal SubTotal { get; set; }
        [InformacaoExibicao(4, "Desconto %", 68, cwkGestao.Modelo.InformacaoExibicao.HAlinhamento.Direita, cwkGestao.Modelo.InformacaoExibicao.Mascaras.Porcentagem4Casas)]
        public virtual decimal PercDesconto { get; set; }
        [InformacaoExibicao(1, "Serviço",354)]
        public virtual string NomeServico { get; set; }
        public virtual string Observacoes { get; set; }
        private int codigoServico;
        public virtual int IDOSServicoItem { get; set; }


        public virtual string Descricao { get; set; }
        
        [InformacaoExibicao(0, "Cód.",63)]
        public virtual int CodigoServico
        {
            get { return Servico != null && codigoServico == 0 ? Servico.Codigo : codigoServico; }
            set { codigoServico = value; }
        }

        public virtual IList<NotaItemServicoPedidoItemServico> NotaItemSvcPedItemSvcs { get; set; }

        public virtual IList<ContratoControle> ContratoControle { get; set; }

        public override bool Equals(object obj)
        {
            if(!typeof(NotaItemServico).IsInstanceOfType(obj)) return false;
            NotaItemServico outro = (NotaItemServico) obj;
            if (outro.ID != ID) return false;
            if (outro.Servico != Servico) return false;
            if (outro.Nota != Nota) return false;
            if (outro.NomeServico != NomeServico) return false;
            return true;
        }

        public static NotaItemServico CloneNewObject(NotaItemServico objNotaItemServico)
        {
            AutoMapper.Mapper.CreateMap<Modelo.NotaItemServico, Modelo.NotaItemServico>().
                ForMember(dest => dest.ID, o => o.Ignore());
            return AutoMapper.Mapper.Map<NotaItemServico, NotaItemServico>(objNotaItemServico);
        }

        public static void GetClones(ref Nota objNota)
        {
            IList<NotaItemServico> lstNotaItemServico = new List<NotaItemServico>(objNota.NotaItemsServicos);
            objNota.NotaItemsServicos = new List<NotaItemServico>();
            foreach (var notaItemServ in lstNotaItemServico)
            {
                NotaItemServico objNotaItemServico = NotaItemServico.CloneNewObject(notaItemServ);
                objNotaItemServico.Nota = objNota;

                objNota.NotaItemsServicos.Add(objNotaItemServico);
            }
        }

        public virtual void PegaDescricao(bool BNumeroExtenso, String descricao, decimal quatidade)
        {
            if (!String.IsNullOrEmpty(descricao))
            {
                if (BNumeroExtenso)
                    Descricao = String.Concat(ConverterNumerico.toExtenso(quatidade), " ", descricao);
                else
                    Descricao = descricao;
            };
        }
    }
}
