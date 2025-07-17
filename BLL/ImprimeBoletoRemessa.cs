using System;
using System.Collections.Generic;
using System.Linq;
using cwkGestao.Integracao.ACBr.Boleto;
using cwkGestao.Negocio;
using Modelo;
using Modelo.Objeto;

namespace BLL
{
    public class ImprimeBoletoRemessa
    {
        private readonly Boleto Boleto = new Boleto();
        private DocumentoBoleto DocumentoBoleto;
        private string Opcao;
        private string CaminhoRemessa;
#pragma warning disable CS0169 // O campo "ImprimeBoletoRemessa.Retorno" nunca é usado
        private string Retorno;
#pragma warning restore CS0169 // O campo "ImprimeBoletoRemessa.Retorno" nunca é usado

        private int Sequencia;
        private int NumRemessaSicredi;
        private int? Banco;
        private Modelo.Remessa Remessa;

        public List <string> GeraBoleto(List<pxyDocCobreBem> pListaRows, string pOpcao, string Email)
        {
            Opcao = pOpcao;
            Banco = pListaRows.First().BancoRemessa;

            var filial = FilialController.Instancia.GetFilialPrincipal();
            Boleto.Remessa.Nome = filial.Nome;
            Boleto.Remessa.CpfCnpj = filial.CNPJ;
            Boleto.Remessa.Logradouro = filial.Endereco;
            Boleto.Remessa.NumeroResidencia = filial.Numero;
            Boleto.Remessa.Bairro = filial.Bairro;
            Boleto.Remessa.Cep = filial.CEP;
            Boleto.Remessa.Cidade = filial.Cidade.Nome;
            Boleto.Remessa.Complemento = filial.Complemento;
            Boleto.Remessa.Telefone = filial.Telefone;
            Boleto.Remessa.UF = filial.Cidade.UF.Sigla;

            Boleto.Remessa.Banco = pListaRows.First().BancoRemessa.GetValueOrDefault(0);
            Boleto.Remessa.TipoCarteira = Convert.ToInt32(pListaRows.First().SicrediTipoCobranca);
            Boleto.Remessa.CodigoAgencia = pListaRows.First().CodigoAgencia;
            Boleto.Remessa.DigitoAgencia = pListaRows.First().DigitoAgencia;
            Boleto.Remessa.ContaCorrente = pListaRows.First().ContaCorrente;
            Boleto.Remessa.DigitoConta = pListaRows.First().DigitoConta;
            Boleto.Remessa.CodigoCedente = pListaRows.First().CodigoCedente;
            Boleto.Remessa.DigitoAgenciaConta = pListaRows.First().DigitoAgenciaConta;
            Boleto.Remessa.InicioNossoNumero = pListaRows.First().InicioNossoNumero;
            Boleto.Remessa.FimNossoNumero = pListaRows.First().FimNossoNumero;
            Boleto.Remessa.Logotipo = pListaRows.First().Logotipo;
            Boleto.Remessa.TipoDocumentoCobranca = pListaRows.First().TipoDocumentoCobranca;
            Boleto.Remessa.InstrucoesCaixa = pListaRows.First().InstrucoesCaixa;
            Boleto.Remessa.Convenio = pListaRows.First().Convenio;

            //Boleto.Remessa.ImpressoraPDF = pListaRows.First().ImpressoraPDF;
            //Boleto.Remessa.CaminhoImagem = pListaRows.First().CaminhoImagem;
            //Boleto.Remessa.OutroDadoConfiguracao2 = Convert.ToInt32(pListaRows.First().SicrediTipoCobranca);
            //Boleto.Remessa.CaminhoLayoutBoleto = pListaRows.First().CaminhoLayoutBoleto;
            //if (Banco != 8) //SICREDI
            //    Boleto.Remessa.OutroDadoConfiguracao1 = pListaRows.First().VariacoCarteira;
            //else
            //    Boleto.Remessa.OutroDadoConfiguracao1 = pListaRows.First().Sequencia.ToString().Substring(0, 1);

            // campos de email da remessa
            Boleto.Remessa.MensagemEmail = pListaRows.First().MensagemEmail;
            Boleto.Remessa.AssuntoEmail = pListaRows.First().AssuntoEmail;
            Boleto.Remessa.NomeEmail = pListaRows.First().NomeEmail;
            Boleto.Remessa.EmailRemetente = pListaRows.First().EmailRemetente;

            //campos de email da empresa
            Boleto.Remessa.ServidorSMTP = pListaRows.First().ServidorSMTP;
            Boleto.Remessa.PortaSMTP = pListaRows.First().PortaSMPT;
            Boleto.Remessa.UsuarioEmail = pListaRows.First().UsuarioEmail;
            Boleto.Remessa.SenhaEmail = pListaRows.First().SenhaEmail;
            Boleto.Remessa.EmailRemetente = pListaRows.First().EmailRemetente;
            Boleto.Remessa.ProximoNossoNumero = pListaRows.First().ProximoNossoNumero;
            
            Boleto.Remessa.LayoutRemessa = pListaRows.First().LayoutRemessa;
            Boleto.Remessa.CaminhoRemessa = pListaRows.First().CaminhoRemessa;
            Boleto.Remessa.NomeRemessa = pListaRows.First().NomeRemessa; 
            Boleto.Remessa.LayoutRemessa = pListaRows.First().LayoutRemessa;
            Boleto.Remessa.LayoutBoleto = pListaRows.First().LayoutBoleto;

            Boleto.Remessa.VersaoLayoutArquivo = pListaRows.First().VersaoLayoutArquivo;
            Boleto.Remessa.VersaoLayoutLote = pListaRows.First().VersaoLayoutLote;


            Boleto.ListaDocumento.Clear();
            foreach (pxyDocCobreBem item in pListaRows)
            {
                DocumentoBoleto = new DocumentoBoletoImpl();
                DocumentoBoleto.Codigo = item.Codigo;
                DocumentoBoleto.Empresa = item.Empresa;
                DocumentoBoleto.Requisicao = item.Requisicao;
                DocumentoBoleto.NomeSacado = item.Nome;
                DocumentoBoleto.CodigoSacado = item.CodigoPessoa;
                DocumentoBoleto.Parcela = item.Parcela;
                DocumentoBoleto.CNPJ = item.CPF_CNPJ;
                DocumentoBoleto.CPF = item.CPF_CNPJ;
                DocumentoBoleto.Rua = item.Rua;
                DocumentoBoleto.Bairro = item.Bairro;
                DocumentoBoleto.Cidade = item.Cidade;
                DocumentoBoleto.Estado = item.Estado;
                DocumentoBoleto.CEP = item.CEP;
                DocumentoBoleto.Data = item.DataDocumento;
                DocumentoBoleto.Vencimento = item.DataVencimento;
                DocumentoBoleto.Valor = item.Valor;
                DocumentoBoleto.Saldo = item.Saldo;
                DocumentoBoleto.DiaProtesto = item.DiaProtesto;
                DocumentoBoleto.InstrucoesCaixa = item.InstrucoesCaixa;
                DocumentoBoleto.Telefone = item.Telefone;
                DocumentoBoleto.QtdParcela = item.QtdParcela;
                DocumentoBoleto.Juro = item.Juro;
                DocumentoBoleto.NumDocumento = item.NumeroDocumento;
                DocumentoBoleto.AgenciaDebito = item.AgenciaDebito;
                DocumentoBoleto.ContaCorrenteDebito = item.ContaCorrenteDebito;

                DocumentoBoleto.PercMulta = item.Acrescimo ;
                DocumentoBoleto.DiaJuro = item.DiaJuro;

                DocumentoBoleto.InstrucaoCobranca3 = item.InstrucaoCobranca3 ?? 1; //VALORES DEFAULT
                DocumentoBoleto.ValorInstrucaoCobranca3 = item.ValorInstrucaoCobranca ?? 28; //VALORES DEFAULT
                DocumentoBoleto.BancoEmiteBoleto = item.BancoEmiteBoleto;
                DocumentoBoleto.DiaProtesto = Convert.ToInt32(item.NumeroDiasProtesto);
                DocumentoBoleto.EspecieTitulo = item.TipoDocumentoCobranca;
                DocumentoBoleto.MovimentoRemessa = item.MovimentoRemessa;
                
                Boleto.ListaDocumento.Add(DocumentoBoleto);
            }

            CaminhoRemessa = pListaRows.First().CaminhoRemessa;
            Sequencia = pListaRows.First().Sequencia.GetValueOrDefault(0);
            Banco = pListaRows.First().BancoRemessa;
            
            // VERIFICAR AQUI
            //if (Banco == 8)
            //{
            //    List<string> ListaRetorno = new List<string>();
            //    ListaRetorno.Add(SicrediNumeroRemessa(pListaRows));

            //    return ListaRetorno;      
            //}

            return null; // Boleto.GeraBoletoRemessa(CaminhoRemessa, Sequencia, Opcao, Email, Banco, NumRemessaSicredi, true, Remessa.SomaNossoNumero, Remessa.BNaoGeraMsgProtesto, Remessa.CodigoBaixaDevolucao, false);   
        }
        private string SicrediNumeroRemessa(List<pxyDocCobreBem> pListaRows)
        {
            DateTime DataAtual = DateTime.Now.Date;
            string NumSicredi = DataAtual.Year + string.Format("{0:00}", DataAtual.Month) +  string.Format("{0:00}", DataAtual.Day) + "1";

            DataClassesDataContext db = new DataClassesDataContext();
            Remessa = (from r in db.Remessas
                       where r.ID == pListaRows.First().IDRemessa
                       select r).First();

            if (Remessa.SicrediNumeroRemessa == null || Remessa.SicrediNumeroRemessa < Convert.ToInt32(NumSicredi))
            {
                NumRemessaSicredi = 1;
                Remessa.SicrediNumeroRemessa = Convert.ToInt32(NumSicredi);
                db.SubmitChanges();
            }
            else
            {
                int aux = Convert.ToInt32(Remessa.SicrediNumeroRemessa.ToString().Substring(8));
                if (aux < 9)
                {
                    NumRemessaSicredi = ++aux;
                    string NovoNumeroSicredi = Remessa.SicrediNumeroRemessa.ToString().Substring(0, 8) + aux;
                    Remessa.SicrediNumeroRemessa = Convert.ToInt32(NovoNumeroSicredi);
                    db.SubmitChanges();
                }
                else if (Opcao == "Remessa")
                {
                    return "O numero limite de remessas enviadas hoje foi alcançado, não é possivel enviar mais remessas hoje.";
                }
            }
            return string.Empty;
        }
    }

    public class DocumentoBoletoImpl : DocumentoBoleto
    {
        /// <summary>
        /// ID do Documento
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Código do Documento
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// Nome da empresa
        /// </summary>
        public string Empresa { get; set; }
        /// <summary>
        /// Número da Requisição do Documento
        /// </summary>
        public string Requisicao { get; set; }
        /// <summary>
        /// Nome do Cliente (Razão Social)
        /// </summary>
        public string NomeSacado { get; set; }
        /// <summary>
        /// Parcela do Documento
        /// </summary>
        public int? Parcela { get; set; }
        /// <summary>
        /// CNPJ do Sacado
        /// </summary>
        public string CNPJ { get; set; }
        /// <summary>
        /// CPF do Sacado
        /// </summary>
        public string CPF { get; set; }
        /// <summary>
        /// Endereço do Sacado
        /// </summary>
        public string Rua { get; set; }
        /// <summary>
        /// Número da Residencia
        /// </summary>
        public string NumeroResidencia { get; set; }
        /// <summary>
        /// Bairro do Sacado
        /// </summary>
        public string Bairro { get; set; }
        /// <summary>
        /// Nome da Cidade do sacado
        /// </summary>
        public string Cidade { get; set; }
        /// <summary>
        /// Estado onde mora o sacado
        /// </summary>
        public string Estado { get; set; }
        /// <summary>
        /// CEP do endereço do sacado
        /// </summary>
        public string CEP { get; set; }
        /// <summary>
        /// Data do Documento
        /// </summary>
        public DateTime? Data { get; set; }
        /// <summary>
        /// Vencimento do Documento
        /// </summary>
        public DateTime? Vencimento { get; set; }
        /// <summary>
        /// Valor do Documento
        /// </summary>
        public decimal? Valor { get; set; }
        /// <summary>
        /// Saldo do Documento
        /// </summary>
        public decimal? Saldo { get; set; }
        /// <summary>
        /// Dias para Protesto do Documento
        /// </summary>
        public int? DiaProtesto { get; set; }
        /// <summary>
        /// Instruções para o Caixa
        /// </summary>
        public string InstrucoesCaixa { get; set; }
        /// <summary>
        /// Telefone do Sacado
        /// </summary>
        public string Telefone { get; set; }
        /// <summary>
        /// Quantidade de Parcelas
        /// </summary>
        public int? QtdParcela { get; set; }

        /// <summary>
        /// Percentual de Juros
        /// </summary>
        public decimal? Juro { get; set; }

        public int? DiaJuro { get; set; }

        public int? DiaMulta { get; }
        public decimal? PercMulta { get; set; }

        /// <summary>
        /// Número do Documento
        /// </summary>
        public string NumDocumento { get; set; }

        /// <summary>
        /// Número Agência para Débito Conta Corrente
        /// </summary>
        public string AgenciaDebito { get; set; }

        /// <summary>
        /// Número da Conta Corrente para Débito
        /// </summary>
        /// 
        public int NossoNumero { get; set; }

        public string ContaCorrenteDebito { get; set; }

        public int InstrucaoCobranca3 { get; set; }

        public int ValorInstrucaoCobranca3 { get; set; }

        public bool? BancoEmiteBoleto { get; set; }

        public string TipoDocumentoCobranca { get; set; }

        public string EspecieTitulo { get; set; }

        public string MovimentoRemessa { get; set; }

        public int CodigoSacado { get; set; }

        public string NumeroNota
        {
            get;
            set;
        }

        #region DocumentoBoleto Members

        public object DocumentoCobreBem { get; set; }
        public string ConteudoTitulo { get; set; }

        #endregion

        public TipoJuroType TipoJuro { get; set; }
        public decimal VlrJuro { get; set;        }
    }
}
