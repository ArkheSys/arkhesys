using System;

namespace cwkGestao.Integracao.ACBr.Boleto
{
    public interface DocumentoBoleto
    {
        /// <summary>
        /// ID do Documento
        /// </summary>
        int ID { get; set; }
        /// <summary>
        /// Código do Documento
        /// </summary>
        int Codigo { get; set; }
        /// <summary>
        /// Nome da empresa
        /// </summary>
        string Empresa { get; set; }
        /// <summary>
        /// Número da Requisição do Documento
        /// </summary>
        string Requisicao { get; set; }
        /// <summary>
        /// Código do Cliente no Sistema
        /// </summary>
        int CodigoSacado { get; set; }
        /// <summary>
        /// Nome do Cliente (Razão Social)
        /// </summary>
        string NomeSacado { get; set; }
        /// <summary>
        /// Parcela do Documento
        /// </summary>
        int? Parcela { get; set; }
        /// <summary>
        /// CNPJ do Sacado
        /// </summary>
        string CNPJ { get; set; }
        /// <summary>
        /// CPF do Sacado
        /// </summary>
        string CPF { get; set; }
        /// <summary>
        /// Endereço do Sacado
        /// </summary>
        string Rua { get; set; }
        /// <summary>
        /// Número da Residência
        /// </summary>
        string NumeroResidencia { get; set; }
        /// <summary>
        /// Bairro do Sacado
        /// </summary>
        string Bairro { get; set; }
        /// <summary>
        /// Nome da Cidade do sacado
        /// </summary>
        string Cidade { get; set; }
        /// <summary>
        /// Estado onde mora o sacado
        /// </summary>
        string Estado { get; set; }
        /// <summary>
        /// CEP do endereço do sacado
        /// </summary>
        string CEP { get; set; }
        /// <summary>
        /// Data do Documento
        /// </summary>
        DateTime? Data { get; set; }
        /// <summary>
        /// Vencimento do Documento
        /// </summary>
        DateTime? Vencimento { get; set; }
        /// <summary>
        /// Valor do Documento
        /// </summary>
        Decimal? Valor { get; set; }
        /// <summary>
        /// Saldo do Documento
        /// </summary>
        Decimal? Saldo { get; set; }
        /// <summary>
        /// Dias para Protesto do Documento
        /// </summary>
        int? DiaProtesto { get; set; }
        /// <summary>
        /// Instruções para o Caixa
        /// </summary>
        string InstrucoesCaixa { get; set; }
        /// <summary>
        /// Telefone do Sacado
        /// </summary>
        string Telefone { get; set; }
        /// <summary>
        /// Quantidade de Parcelas
        /// </summary>
        int? QtdParcela { get; set; }

        /// <summary>
        /// Percentual de Juros
        /// </summary>
        Decimal? Juro { get; set; }

        Int32? DiaJuro { get; set; }

        int? DiaMulta { get; }

        Decimal? PercMulta { get; set; }

        /// <summary>
        /// Número do Documento
        /// </summary>
        string NumDocumento { get; set; }

        /// <summary>
        /// Número Agência para Débito Conta Corrente
        /// </summary>
        string AgenciaDebito { get; set; }

        /// <summary>
        /// Número da Conta Corrente para Débito
        /// </summary>
        string ContaCorrenteDebito { get; set; }

        int InstrucaoCobranca3 { get; set; }

        int ValorInstrucaoCobranca3 { get; set; }

        bool? BancoEmiteBoleto { get; set; }

        string EspecieTitulo { get; set; }

        string MovimentoRemessa { get; set; }

        string NumeroNota { get; set; }

        int NossoNumero { get; set; }

        object DocumentoCobreBem { get; set; }

        string ConteudoTitulo { get; set; }
    }
}
