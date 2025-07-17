using System;

using cwkGestao.Integracao.ACBr.Boleto;
using cwkGestao.Modelo;

using Remessa = cwkGestao.Modelo.Remessa;
using TipoJuroType = cwkGestao.Integracao.ACBr.Boleto.TipoJuroType;

namespace cwkGestao.Negocio.Financeiro.BoletoRemessa
{
    internal class DocumentoBoletoImpl : DocumentoBoleto
    {
        public Documento Documento { get; private set; }
        public int NossoNumero { get; set; }
        private readonly Remessa remessa;
#pragma warning disable CS0169 // O campo "DocumentoBoletoImpl.Doc" nunca é usado
        private DocumentoBoleto Doc;
#pragma warning restore CS0169 // O campo "DocumentoBoletoImpl.Doc" nunca é usado

        public DocumentoBoletoImpl(Remessa remessa, Documento documento)
        {
            this.remessa = remessa;
            Documento = documento;
            Documento.NossoNumero = documento.NossoNumero;
        }

        public string AgenciaDebito
        {
            get => Documento.Pessoa.Agencia;
            set { }
        }

        public string Bairro
        {
            get => Documento.Pessoa.EnderecoPrincipal().Bairro;
            set { }
        }

        public bool? BancoEmiteBoleto
        {
            get => remessa.BancoEmiteBoleto;
            set { }
        }

        public string CEP
        {
            get => Documento.Pessoa.EnderecoPrincipal().CEP;
            set { }
        }

        public string CNPJ
        {
            get => Documento.Pessoa.CNPJ_CPF;
            set { }
        }

        public string CPF
        {
            get => Documento.Pessoa.CNPJ_CPF;
            set { }
        }

        public string Cidade
        {
            get => Documento.Pessoa.EnderecoPrincipal().Cidade.Nome;
            set { }
        }

        public int Codigo
        {
            get => Documento.Codigo;
            set { }
        }

        public string ContaCorrenteDebito
        {
            get => remessa.ContaCorrente;
            set { }
        }

        public DateTime? Data
        {
            get => Documento.Dt;
            set { }
        }

        public int? DiaJuro
        {
            get => Documento.Acrescimo.DiasJuro;
            set { }
        }

        public int? DiaMulta => Documento.Acrescimo.DiasMulta;

        public int? DiaProtesto
        {
            get => Documento.Acrescimo.DiasProtesto;
            set { }
        }

        public string Empresa
        {
            get => Documento.Filial.Nome;
            set { }
        }

        public string EspecieTitulo
        {
            get => remessa.EspecieTitulo;
            set { }
        }

        public string Estado
        {
            get => Documento.Pessoa.EnderecoPrincipal().Cidade.UF.Sigla;
            set { }
        }

        public int ID
        {
            get => Documento.ID;
            set { }
        }

        public int InstrucaoCobranca3
        {
            get => remessa.CodigoBaixaDevolucao.HasValue ? remessa.CodigoBaixaDevolucao.Value : 1 /*Valor padrão*/;
            set { }
        }

        public string InstrucoesCaixa
        {
            get => remessa.InstrucoesCaixa;
            set { }
        }

        public decimal? Juro
        {
            get => Documento.Acrescimo.PercJuro;
            set { }
        }

        public string MovimentoRemessa
        {
            get => Documento.MovimentoRemessa;
            set { }
        }

        public string NomeSacado
        {
            get => Documento.Pessoa.Nome;
            set { }
        }

        public string NumDocumento
        {
            get => Documento.Codigo.ToString();
            set { }
        }

        public int? Parcela
        {
            get => Documento.Parcela;
            set { }
        }

        public decimal? PercMulta
        {
            get => Documento.Acrescimo.PercMulta;
            set { }
        }

        public int? QtdParcela
        {
            get => Documento.QtParcela;
            set { }
        }

        public string Requisicao
        {
            get => Documento.NumRequisicao;
            set { }
        }

        public string Rua
        {
            get => Documento.Pessoa.EnderecoPrincipal().Endereco;
            set { }
        }

        public string NumeroResidencia
        {
            get => Documento.Pessoa.EnderecoPrincipal().Numero == string.Empty ? "SN" : Documento.Pessoa.EnderecoPrincipal().Numero;
            set { }
        }

        public decimal? Saldo
        {
            get => Documento.Saldo;
            set { }
        }

        public string Telefone
        {
            get => Documento.Pessoa.TelefonePrincipal;
            set { }
        }

        public decimal? Valor
        {
            get => Documento.Valor;
            set { }
        }

        public int ValorInstrucaoCobranca3
        {
            get => remessa.NumDiasBaixaDevolucao.HasValue ? remessa.NumDiasBaixaDevolucao.Value : 28 /*Valor padrão*/;
            set { }
        }

        public DateTime? Vencimento
        {
            get => Documento.DtVencimento;
            set { }
        }

        public int CodigoSacado
        {
            get => Documento.Pessoa.Codigo;
            set { }
        }

        public string NumeroNota
        {
            get => Documento.Nota != null ? string.Format("{0:000000000}", Documento.Nota.Numero) : string.Empty;
            set { }
        }

        public object DocumentoCobreBem { get; set; }
        public string ConteudoTitulo { get; set; }

        public TipoJuroType TipoJuro
        {
            get => (TipoJuroType)Documento.Acrescimo.TipoJuro;
            set { }
        }

        public decimal VlrJuro
        {
            get => Documento.Acrescimo.VlrJuro;
            set { }
        }
    }
}