using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Fatura")]
    public class Tel_Fatura : ModeloBase
    {
        public Tel_Fatura()
        {
            Servicos = new List<Tel_FaturaServico>();
            Ligacoes = new List<Tel_FaturaLigacao>();
        }

        public Tel_Fatura(int id, int codigo, DateTime data, DateTime referencia, DateTime vencimento
                          , decimal valor, DateTime dataInicial, DateTime dataFinal
                          , string nomeCliente, string tipoDocumento, string grupo
                          , string piloto, string nossoNumero)
            : this()
        {
            ID = id;
            Codigo = codigo;
            Data = data;
            Referencia = referencia;
            Vencimento = vencimento;
            Valor = valor;
            DataInicial = dataInicial;
            DataFinal = dataFinal;
            Cliente = new Tel_Cliente { Pessoa = new Pessoa { Nome = nomeCliente }, GrupoCliente = new Tel_GrupoCliente { Nome = grupo } };
            if (!String.IsNullOrEmpty(tipoDocumento))
                Documento = new Documento { TipoDocumento = new TipoDocumento { Nome = tipoDocumento } };
            NumeroPiloto = piloto;
            NossoNumero = nossoNumero;
        }

        public Tel_Fatura(int id, int codigo, DateTime data, DateTime referencia, DateTime vencimento
                         , decimal valor, DateTime dataInicial, DateTime dataFinal
                         , string nomeCliente, string tipoDocumento, string grupo
                         , string piloto, string nossoNumero, DateTime? dataEnvio, bool enviado)
            : this()
        {
            ID = id;
            Codigo = codigo;
            Data = data;
            Referencia = referencia;
            Vencimento = vencimento;
            Valor = valor;
            DataInicial = dataInicial;
            DataFinal = dataFinal;
            Cliente = new Tel_Cliente { Pessoa = new Pessoa { Nome = nomeCliente }, GrupoCliente = new Tel_GrupoCliente { Nome = grupo } };
            if (!String.IsNullOrEmpty(tipoDocumento))
                Documento = new Documento { TipoDocumento = new TipoDocumento { Nome = tipoDocumento } };
            NumeroPiloto = piloto;
            NossoNumero = nossoNumero;
            DataEnvio = dataEnvio;
            bEnviadoEmail = enviado;
        }

        public Tel_Fatura(int id, int codigo, DateTime vencimento, DateTime referencia
                         , decimal valor, string grupo, string nomeCliente, string fantasia
                         , string nossoNumero, DateTime? dataEnvio, bool enviado, string piloto)
            : this()
        {
            ID = id;
            Codigo = codigo;
            Vencimento = vencimento;
            Referencia = referencia;
            Cliente = new Tel_Cliente { Pessoa = new Pessoa { Nome = nomeCliente }, GrupoCliente = new Tel_GrupoCliente { Nome = grupo } };
            GrupoCliente = new Tel_GrupoCliente { Nome = grupo };
            FantasiaStr = fantasia;
            Valor = valor;
            bEnviadoEmail = bEnviadoEmail;
            if (bEnviadoEmail == false)
            {
                dataEnvio = null;
            }
            else
            {
                dataEnvio = DataEnvio;
            }
            NumeroPiloto = piloto;
        }
        public override int ID { get; set; }
        [InformacaoExibicao(1, "Código", 85)]
        public virtual int Codigo { get; set; }
        [InformacaoExibicao(2, "Data", 80)]
        public virtual DateTime Data { get; set; }
        public virtual DateTime Referencia { get; set; }
        public virtual Tel_Cliente Cliente { get; set; }
        public virtual Tel_GrupoCliente GrupoCliente { get; set; }

        [InformacaoExibicao(4, "Vencimento", 80)]
        public virtual DateTime Vencimento { get; set; }
        [InformacaoExibicao(8, "Valor", 85, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Valor { get; set; }
        public virtual Documento Documento { get; set; }

        private string _NossoNumero;
        [InformacaoExibicao(10, "Nosso Número", 100)]
        public virtual String NossoNumero { get { return _NossoNumero; } set { _NossoNumero = value; } }
        
        [InformacaoExibicao(11, "Data Inicial", 80)]
        public virtual DateTime DataInicial { get; set; }
        [InformacaoExibicao(12, "Data Final", 80)]
        public virtual DateTime DataFinal { get; set; }

        [InformacaoExibicao(5, "Grupo", 200)]
        public virtual string GrupoNome { get { return Cliente.GrupoCliente.Nome ?? String.Empty; } }

        [InformacaoExibicao(6, "Cliente", 165)]
        public virtual String NomeCliente { get { return Cliente.Pessoa.Nome; } }

        [InformacaoExibicao(7, "Num. Piloto", 100)]
        public virtual string NumeroPiloto { get; set; }

        public virtual String Fantasia { get { return Cliente.Pessoa.Fantasia; } }

        [InformacaoExibicao(9, "Documento", 85)]
        public virtual String DescDocumento { get { return Documento == null ? null : Documento.TipoDocumento.Nome; } }
        [InformacaoExibicao(3, "Referência", 85)]
        public virtual String ReferenciaShort { get { return String.Format("{0:MM/yyyy}", Referencia.Date); } }

        public virtual IList<Tel_FaturaServico> Servicos { get; set; }
        public virtual IList<Tel_FaturaLigacao> Ligacoes { get; set; }

        private DateTime? _DataEnvio { get; set; }
        public virtual DateTime? DataEnvio { get { return _DataEnvio; } set { _DataEnvio = value; } }

        private bool _bEnviadoEmail { get; set; }
        public virtual bool bEnviadoEmail { get { return _bEnviadoEmail; } set { _bEnviadoEmail = value; } }

        public virtual String FantasiaStr { get; set; }
    }
}
