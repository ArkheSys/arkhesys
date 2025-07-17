using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Orçamento de Serviço")]
    public class OSOrdemServico : ModeloBase
    {

        public enum OrigemOS { Interna = 0, Externa = 1 }
        public enum StatusOS
        {
            AguardandoAgendamento = 0,
            AguardandoAnalise = 1,
            EmAnalise = 2,
            AguardandoAprovacao = 3, 
            AguardandoExecucao = 4,
            AguardandoPecas = 10,
            EmExecucao = 5,
            AguardandoFatMO = 6, 
            NaoAprovada = 7, 
            AguardandoFaturamento = 8, 
            Fechada = 9,
            EmLaboratorio = 11,

            Todos = -1
            
        }
        public enum ImprimirOS 
        {
            Laser = 1, 
            Etiqueta = 2, 
            Recibo = 3,
            Fechar = 4
        }

        public OSOrdemServico()
        {
            Acessorios = "";
            Observacao = "";
            OSProdutoItems = new List<OSProdutoItem>();
            OSServicoItems = new List<OSServicoItem>();
            OSAcompanhamentos = new List<OSAcompanhamento>();
        }

        public override int ID { get; set; }
        [InformacaoExibicao(1, "Código", 80, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int Codigo { get; set; }
        [InformacaoExibicao(7, "Equipamento", 300)]
        public virtual OSEquipamento Equipamento { get; set; }
        [InformacaoExibicao(5, "Cliente", 300)]
        public virtual Pessoa Cliente { get; set; }
        public virtual Filial Empresa { get; set; }
        [InformacaoExibicao(2, "Data", 80, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual DateTime Data { get; set; }
        public virtual String UsuarioLogado { get; set; }
        public virtual String ProblemaRelatado { get; set; }
        public virtual String Acessorios { get; set; }
        [InformacaoExibicao(3, "Origem", 80, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual OrigemOS Origem { get; set; }
        public virtual bool NecessitaAprovacaoCliente { get; set; }
        //public virtual StatusOS Status { get; set; }
        public virtual Modelo.StatusOS Status { get; set; }
        public virtual Pessoa Vendedor { get; set; }
        public virtual Condicao Condicao { get; set; } //
        [InformacaoExibicao(8, "Técnico", 300)]
        public virtual Pessoa Tecnico { get; set; } //
        public virtual TabelaPreco TabelaPreco { get; set; } //
        public virtual string ResultadoTecnico { get; set; }
        [InformacaoExibicao(16, "ResultadoCliente", 300)]
        public virtual string ResultadoCliente { get; set; }
        public virtual decimal ProdutoSubTotal { get; set; }
        public virtual decimal ProdutoDescontoValor { get; set; }
        public virtual decimal ProdutoDescontoPerc { get; set; }
        public virtual decimal ProdutoTotal { get; set; }
        public virtual decimal ServicoSubTotal { get; set; }
        public virtual decimal ServicoDescontoValor { get; set; }
        public virtual decimal ServicoDescontoPerc { get; set; }
        public virtual decimal ServicoTotal { get; set; }
        [InformacaoExibicao(6, "Total", 120, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Total { get; set; }
        public virtual bool BGarantia { get; set; }
        public virtual decimal DespesasAdicionais { get; set; }
        public virtual int CodigoPedido { get; set; }
        public virtual String Observacao { get; set; }

        [InformacaoExibicao(10, "Cod. Imei", 200)]
        public virtual String Imei { get; set; }
        public virtual String NumSerie { get; set; }

        [InformacaoExibicao(9, "Fantasia", 300)]
        public virtual string PessoaFantasia
        {
            get
            {
                if (Cliente.TipoPessoa == "Física")
                    return Cliente.FantasiaApelido;
                else
                    return Cliente.Fantasia;
            }
        }
        
        [InformacaoExibicao(11, "Data Inclusão", 100)]
        public virtual DateTime? dtInc
        {
            get
            {
                return this.IncData;
            }
        }
        
        [InformacaoExibicao(12, "Usuário Inclusão", 200)]
        public virtual string usuInc
        {
            get
            {
                return this.IncUsuario;
            }
        }
        
        [InformacaoExibicao(13, "Data Alteração", 100)]
         public virtual DateTime? dtAlt
         {
             get
             {
                 return this.AltData;
             }
         }
        
        [InformacaoExibicao(14, "Usuário Alteração", 200)]
        public virtual string usuAlt
        {
            get
            {
                return this.AltUsuario;
            }
        }

        public virtual IList<OSAcompanhamento> OSAcompanhamentos { get; set; }
        public virtual IList<OSProdutoItem> OSProdutoItems { get; set; }
        public virtual IList<OSServicoItem> OSServicoItems { get; set; }

        [InformacaoExibicao(4, "Status", 100)]
        public virtual string StatusLegivel
        {
            get
            {
                this.Status = this.Status ?? new Modelo.StatusOS();
                return this.Status.Descricao;
            }
        }

        //public static string StatusToString(StatusOS status)
        //{
        //    string statusLegivel = "";
        //    switch (status)
        //    {
        //        case StatusOS.AguardandoAgendamento:
        //            statusLegivel = "Aguardando Agendamento";
        //            break;
        //        case StatusOS.AguardandoAnalise:
        //            statusLegivel = "Aguardando Análise";
        //            break;
        //        case StatusOS.AguardandoAprovacao:
        //            statusLegivel = "Aguardando Aprovação";
        //            break;
        //        case StatusOS.EmAnalise:
        //            statusLegivel = "Em Análise";
        //            break;
        //        case StatusOS.AguardandoExecucao:
        //            statusLegivel = "Aguardando Execução";
        //            break;
        //        case StatusOS.EmExecucao:
        //            statusLegivel = "Em Execução";
        //            break;
        //        case StatusOS.AguardandoFatMO:
        //            statusLegivel = "Aguardando Fat. MO";
        //            break;
        //        case StatusOS.NaoAprovada:
        //            statusLegivel = "Não Aprovada";
        //            break;
        //        case StatusOS.AguardandoFaturamento:
        //            statusLegivel = "Aguardando Faturamento";
        //            break;
        //        case StatusOS.Fechada:
        //            statusLegivel = "Fechada";
        //            break;
        //        case StatusOS.AguardandoPecas:
        //            statusLegivel = "Aguardando Peças";
        //            break;
        //        case StatusOS.EmLaboratorio:
        //            statusLegivel = "Em Laboratório";
        //            break;
        //    }
        //    return statusLegivel;
        //}

        public virtual DateTime HoraInicio { get; set; }
        public virtual DateTime HoraFim { get; set; }
        public virtual DateTime HoraInicio2 { get; set; }
        public virtual DateTime HoraFim2 { get; set; }
        [InformacaoExibicao(12, "Solicitante", 200)]
        public virtual String Solicitante { get; set; }

        [InformacaoExibicao(15, "Último Envio E-mail", 200,0,cwkGestao.Modelo.InformacaoExibicao.Mascaras.DataHora)]
        public virtual DateTime UltimoEnvioEmail { get; set; }


        public virtual void ReCalculaTotaisProdutos()
        {
            this.ProdutoSubTotal = this.OSProdutoItems.Sum(i => i.Total);
            
            if (this.ProdutoSubTotal == 0)
            {
                this.ProdutoDescontoPerc = 0;
                this.ProdutoDescontoValor = 0;
            }

            this.ProdutoTotal = this.ProdutoSubTotal - this.ProdutoDescontoValor;
            this.Total = this.ProdutoTotal + this.ServicoTotal + this.DespesasAdicionais;
        }

        public virtual void ReCalculaTotaisServicos()
        {
            this.ServicoSubTotal = this.OSServicoItems.Sum(i => i.Total);

            if (this.ServicoSubTotal == 0)
            {
                this.ServicoDescontoPerc = 0;
                this.ServicoDescontoValor = 0;
            }

            this.ServicoTotal = this.ServicoSubTotal - this.ServicoDescontoValor;
            this.Total = this.ProdutoTotal + this.ServicoTotal + this.DespesasAdicionais;

        }

        public virtual void ReCalculaTotais()
        {
            ReCalculaTotaisProdutos();
            ReCalculaTotaisServicos();
        }

        public override ModeloBase Clonar()
        {
            OSOrdemServico objOrdemServico= new OSOrdemServico();

            AutoMapper.Mapper.CreateMap<OSOrdemServico, OSOrdemServico>()
                .ForMember(dest => dest.ID, o => o.Ignore())
                .ForMember(dest => dest.CodigoPedido, o => o.Ignore());

            objOrdemServico = AutoMapper.Mapper.Map<OSOrdemServico>(this);


            objOrdemServico.OSProdutoItems = objOrdemServico.OSProdutoItems.Select(pi => {
                pi = (OSProdutoItem)pi.Clonar(); pi.OSOrdemServico = objOrdemServico; return pi; }).ToList();

            objOrdemServico.OSServicoItems = objOrdemServico.OSServicoItems.Select(si => {
                si = (OSServicoItem)si.Clonar(); si.OSOrdemServico = objOrdemServico; return si; }).ToList();

            objOrdemServico.OSAcompanhamentos = new List<OSAcompanhamento>(); 

            return objOrdemServico;
        }
    }
}
