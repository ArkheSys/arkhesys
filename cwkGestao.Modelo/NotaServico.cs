using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Nota de Serviço")]
    public class NotaServico : ModeloBase
    {
        public NotaServico()
        {
            NotaServicoItems = new List<NotaServicoItem>();
            Data = DateTime.Now;
            Emissao = DateTime.Now;
        }
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(1, "Número", 100)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private Pessoa _Cliente;
        public virtual Pessoa Cliente { get { return _Cliente; } set { _Cliente = value; } }

        private Condicao _Condicao;
        public virtual Condicao Condicao { get { return _Condicao; } set { _Condicao = value; } }
       
        private Filial _Filial;
        public virtual Filial Filial { get { return _Filial; } set { _Filial = value; } }

        private DateTime _Data;
        [InformacaoExibicao(2, "Data", 85)]
        public virtual DateTime Data { get { return _Data; } set { _Data = value; } }

        private DateTime _Emissao;
        [InformacaoExibicao(3, "Emissao", 85)]
        public virtual DateTime Emissao { get { return _Emissao; } set { _Emissao = value; } }

        private string _ClienteNome;
        [InformacaoExibicao(4, "Cliente", 300)]
        public virtual string ClienteNome { get { return _ClienteNome; } set { _ClienteNome = value; } }

        private string _ClienteCnpjCpf;
        public virtual string ClienteCnpjCpf { get { return _ClienteCnpjCpf; } set { _ClienteCnpjCpf = value; } }

        private string _ClienteInscricao;
        public virtual string ClienteInscricao { get { return _ClienteInscricao; } set { _ClienteInscricao = value; } }

        private string _ClienteEndereco;
        public virtual string ClienteEndereco { get { return _ClienteEndereco; } set { _ClienteEndereco = value; } }

        private string _ClienteBairro;
        public virtual string ClienteBairro { get { return _ClienteBairro; } set { _ClienteBairro = value; } }

        private string _ClienteCidade;
        public virtual string ClienteCidade { get { return _ClienteCidade; } set { _ClienteCidade = value; } }

        private string _ClienteUF;
        public virtual string ClienteUF { get { return _ClienteUF; } set { _ClienteUF = value; } }

        private string _ClienteFone;
        [InformacaoExibicao(6, "Telefone", 100)]
        public virtual string ClienteFone { get { return _ClienteFone; } set { _ClienteFone = value; } }              

        private decimal _TotalProduto;
        public virtual decimal TotalProduto { get { return _TotalProduto; } set { _TotalProduto = value; } }       

        private decimal _Desconto;
        public virtual decimal Desconto { get { return _Desconto; } set { _Desconto = value; } }       

        private decimal _TotalNota;
        [InformacaoExibicao(5, "Total", 100, InformacaoExibicao.HAlinhamento.Padrao,InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal TotalNota { get { return _TotalNota; } set { _TotalNota = value; } }

        private string _Observacao;
        public virtual string Observacao { get { return _Observacao; } set { _Observacao = value; } }       
        
        private string _ClienteCep;
        public virtual string ClienteCep { get { return _ClienteCep; } set { _ClienteCep = value; } }              

        private IList<NotaServicoItem> _NotaServicoItems;
        public virtual IList<NotaServicoItem> NotaServicoItems { get { return _NotaServicoItems; } set { _NotaServicoItems = value; } }


        public override ModeloBase Clonar()
        {
            NotaServico objNotaServico = new NotaServico();

            AutoMapper.Mapper.CreateMap<NotaServico, NotaServico>()
                .ForMember(dest => dest.ID, o => o.Ignore());

            objNotaServico = AutoMapper.Mapper.Map<NotaServico>(this);


            //objNotaServico.OSProdutoItems = objNotaServico.OSProdutoItems.Select(pi =>
            //{
            //    pi = (OSProdutoItem)pi.Clonar(); pi.OSOrdemServico = objNotaServico; return pi;
            //}).ToList();

            //objNotaServico.OSServicoItems = objNotaServico.OSServicoItems.Select(si =>
            //{
            //    si = (OSServicoItem)si.Clonar(); si.OSOrdemServico = objNotaServico; return si;
            //}).ToList();

            //objNotaServico.OSAcompanhamentos = new List<OSAcompanhamento>();

            return objNotaServico;
        }
    }
}
