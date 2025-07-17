using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace BLL
{
    public class ExpNotaValidaPR : IExpNotas
    {
        /// <summary>
        /// Método para gerar arquivo texto das notas para o Valida-PR
        /// 23/12/2009 - CRNC
        /// </summary>
        /// <param name="pArquivo">Nome do arquivo a ser gerado</param>
        /// <param name="pDataInicial">Data Inicial</param>
        /// <param name="pDataFinal">Data Final</param>
        /// <param name="pFilial">Filal a ser considerada</param>
        /// <param name="pEntSai">Tipo das notas a serem consideradas (Entrada, Saida, Ambas)</param>
        public void GerarArquivo(string pArquivo, DateTime pDataInicial, DateTime pDataFinal, int pFilial, Modelo.EntSaiType pEntSai)
        {
            //Recupera as notas para gerar o arquivo
            Modelo.DataClassesDataContext db = new Modelo.DataClassesDataContext();
            List<Modelo.Nota> notas;
            List<Modelo.RegistrosTipo60> RegistrosTipo60;

            Modelo.Filial objFilial;
            objFilial = (from f in db.Filials
                         where f.ID == pFilial
                         select f).Single();

            if (pEntSai == Modelo.EntSaiType.Ambas)
            {
                notas = (from n in db.Notas
                         where n.Dt >= pDataInicial
                         && n.Dt <= pDataFinal
                         && n.ModeloDocto > 0
                         && n.Filial.Codigo == pFilial
                         && n.ModeloDocto != 33
                         select n).ToList();
            }
            else
            {
                Modelo.InOutType entsai;
                switch (pEntSai)
                {
                    case Modelo.EntSaiType.Entrada:
                        entsai = Modelo.InOutType.Entrada;
                        break;
                    case Modelo.EntSaiType.Saida:
                        entsai = Modelo.InOutType.Saída;
                        break;
                    default:
                        entsai = Modelo.InOutType.Entrada;
                        break;
                }

                notas = (from n in db.Notas
                         where n.Dt >= pDataInicial
                         && n.Dt <= pDataFinal
                         && n.ModeloDocto > 0
                         && n.Filial.Codigo == pFilial
                         && n.Ent_Sai == entsai
                         && n.ModeloDocto != 33
                         select n).ToList();
            }

            RegistrosTipo60 = (from r in db.RegistrosTipo60s
                               where r.Dt >= pDataInicial
                               && r.Dt <= pDataFinal
                               select r).ToList();


            //if (notas.Count == 0)
            //{
            //    MessageBox.Show("Não foi encontrado notas para exportar.\nVerifique.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}

            ValidaPR arqValidaPR = new ValidaPR();

            //Atribui as informações referente ao Registro Tipo 10
            arqValidaPR.Registro10.CNPJ = objFilial.CNPJ;
            arqValidaPR.Registro10.InscricaoEstadual = objFilial.Inscricao;
            arqValidaPR.Registro10.Contribuinte = objFilial.Nome;
            arqValidaPR.Registro10.Municipio = objFilial.Cidade.Nome;
            arqValidaPR.Registro10.Estado = objFilial.Cidade.UF.Sigla;
            arqValidaPR.Registro10.Fax = objFilial.Fax;
            arqValidaPR.Registro10.DataInicial = pDataInicial;
            arqValidaPR.Registro10.DataFinal = pDataFinal;
            arqValidaPR.Registro10.CodigoFinalidade = "1";

            //Atribui as informações referente ao Registro Tipo 90
            arqValidaPR.Registro90.CNPJ = objFilial.CNPJ;
            arqValidaPR.Registro90.InscricaoEstadual = objFilial.Inscricao;

            //Atribui as informações referente ao Registro Tipo 11
            arqValidaPR.Registro11.Logradouro = objFilial.Endereco;
            arqValidaPR.Registro11.Numero = Convert.ToInt32(objFilial.Numero);
            arqValidaPR.Registro11.Complemento = "";
            arqValidaPR.Registro11.Bairro = objFilial.Bairro;
            arqValidaPR.Registro11.Cep = objFilial.CEP;
            arqValidaPR.Registro11.Contato = objFilial.Responsavel;
            arqValidaPR.Registro11.Telefone = objFilial.Telefone;

            foreach (Modelo.Nota objNota in notas)
            {
                foreach (Modelo.NotaICM objNotaIcms in objNota.NotaICMs)
                {
                    Tipo50 registro50 = new Tipo50();

                    registro50.CNPJ = objNota.PessoaCNPJCPF;
                    registro50.InscricaoEstadual = registro50.CNPJ.Length != 14 ? "ISENTO" : objNota.PessoaInscRG;
                    registro50.Data = objNota.Dt;
                    registro50.Estado = objNota.PessoaUF;
                    registro50.Modelo = (int)objNota.ModeloDocto;
                    registro50.Serie = objNota.Serie;
                    registro50.NumeroNF = (int)objNota.Numero;
                    registro50.CFOP = Convert.ToInt32(objNotaIcms.TabelaCFOP.CFOP);
                    registro50.Emitente = objNota.TipoNota.NFOrigem == Modelo.OrigemNFType.Própria ? "P" : "T";
                    registro50.ValorTotal = (Decimal)objNotaIcms.ValorTotalProdutos;// +(Decimal)objNotaIcms.ValorIPI;
                    registro50.BaseCalculoICMS = (Decimal)objNotaIcms.BaseICMS;
                    registro50.ValorICMS = (Decimal)objNotaIcms.ValorICMS;
                    registro50.ValorIsentoICMS = (Decimal)objNotaIcms.ValorIsentoICMS;
                    registro50.ValorOutras = (Decimal)objNotaIcms.ValorOutroICMS + (Decimal)objNotaIcms.ValorIPI;
                    registro50.Aliquota = (Decimal)objNotaIcms.AliqICMSNormal;
                    registro50.Situacao = objNota.CancDt == null ? "N" : "S";

                    arqValidaPR.ListaRegistro50.Add(registro50);

                    if (objNotaIcms.ValorSubst > 0)
                    {
                        Tipo53 registro53 = new Tipo53();

                        registro53.CNPJ = objNota.PessoaCNPJCPF;
                        registro53.InscricaoEstadual = objNota.PessoaInscRG;
                        registro53.Data = objNota.Dt;
                        registro53.Estado = objNota.PessoaUF;
                        registro53.Modelo = (int)objNota.ModeloDocto;
                        registro53.Serie = objNota.Serie;
                        registro53.NumeroNF = (int)objNota.Numero;
                        registro53.CFOP = Convert.ToInt32(objNotaIcms.TabelaCFOP.CFOP);
                        registro53.Emitente = objNota.TipoNota.NFOrigem == Modelo.OrigemNFType.Própria ? "P" : "T";
                        registro53.BaseCalculoICMS_ST = (Decimal)objNotaIcms.BaseSubst;
                        registro53.ValorICMSRetido = (Decimal)objNotaIcms.ValorSubst;
                        registro53.ValorDespesas = 0;
                        registro53.Situacao = objNota.CancDt == null ? "N" : "S";
                        registro53.CodigoAntecipacao = " ";

                        arqValidaPR.ListaRegistro53.Add(registro53);
                    }
                }

                foreach (Modelo.NotaItem objNotaItem in objNota.NotaItems)
                {
                    Tipo54 registro54 = new Tipo54();

                    registro54.CNPJ = objNota.PessoaCNPJCPF;
                    registro54.Modelo = (int)objNota.ModeloDocto;
                    registro54.Serie = objNota.Serie;
                    registro54.NumeroNF = (int)objNota.Numero;
                    registro54.CFOP = Convert.ToInt32(objNotaItem.TabelaCFOP.CFOP);
                    registro54.CST = objNotaItem.SitTrib == null ? (objNotaItem.Produto.OrigemProd.ToString() + NFe.Conv_TAG_CST((int)objNotaItem.Produto.Tributacao)) : objNotaItem.SitTrib;
                    registro54.NumeroItem = (int)objNotaItem.Sequencia;
                    registro54.CodigoProduto = objNotaItem.Produto.Codigo.ToString();
                    registro54.Quantidade = objNotaItem.Quantidade;
                    registro54.ValorProduto = (Decimal)objNotaItem.Total + (Decimal)objNotaItem.RAT_Acrescimo;
                    registro54.ValorDesconto = (Decimal)objNotaItem.RAT_Desconto;
                    registro54.BaseCalculoIcms = (Decimal)objNotaItem.BaseICMS;
                    registro54.BaseCalculoIcms_ST = (Decimal)objNotaItem.BaseICMSSubst;
                    registro54.ValorIPI = (Decimal)objNotaItem.vIPI_O14;
                    registro54.AliquotaIcms = (Decimal)objNotaItem.AliqICMSNormal;

                    arqValidaPR.ListaRegistro54.Add(registro54);

                    // Verificar se o item está na lista do Registro75, caso negativo incluir ele na lista
                    if (arqValidaPR.ListaRegistro75.Where(w => w.CodigoProduto == objNotaItem.Produto.Codigo.ToString()).Count() == 0)
                    {
                        Tipo75 registro75 = new Tipo75();

                        registro75.DataInicial = pDataInicial;
                        registro75.DataFinal = pDataFinal;
                        registro75.CodigoProduto = objNotaItem.Produto.Codigo.ToString();
                        registro75.CodigoNCM = objNotaItem.Produto.NCM;
                        registro75.DescricaoProduto = objNotaItem.Produto.Nome;
                        registro75.Unidade = objNotaItem.Unidade;
                        registro75.AliquotaIPI = (Decimal)objNotaItem.pIPI_O13;
                        registro75.AliquotaICMS = (Decimal)objNotaItem.AliqICMSNormal;
                        registro75.ReducaoBaseCalculo = (Decimal)objNotaItem.pRedBC_N14;
                        registro75.BaseCalculoST = (Decimal)objNotaItem.BaseICMSSubst;

                        arqValidaPR.ListaRegistro75.Add(registro75);
                    }
                }
            }

            foreach (var item in RegistrosTipo60)
            {
                Tipo60 registro60 = new Tipo60();

                registro60.Registro = item.Registro;

                arqValidaPR.ListaRegistro60.Add(registro60);
            }
            arqValidaPR.GeraArquivo(pArquivo);

            return;
        }

        public class ValidaPR
        {
            #region AtibutosPrivados
            private Tipo10 _registro10;
            private Tipo11 _registro11;
            private List<Tipo50> _listaRegistro50;
            private List<Tipo53> _listaRegistro53;
            private List<Tipo54> _listaRegistro54;
            private List<Tipo60> _listaRegistro60;
            private List<Tipo75> _listaRegistro75;
            private List<RegistroOrdem> _registroOrdem;
            private Tipo90 _registro90;
            #endregion

            #region Contrutor
            public ValidaPR()
            {
                _registro10 = new Tipo10();
                _registro11 = new Tipo11();
                _registroOrdem = new List<RegistroOrdem>();
                _listaRegistro50 = new List<Tipo50>();
                _listaRegistro53 = new List<Tipo53>();
                _listaRegistro54 = new List<Tipo54>();
                _listaRegistro60 = new List<Tipo60>();
                _listaRegistro75 = new List<Tipo75>();
                _registro90 = new Tipo90();
            }
            #endregion

            #region Métodos
            /// <summary>
            /// REGISTRO TIPO 10
            /// MESTRE DO ESTABELECIMENTO
            /// </summary>
            public Tipo10 Registro10
            {
                get { return _registro10; }
                set { _registro10 = value; }
            }

            /// <summary>
            /// REGISTRO TIPO 11
            /// DADOS COMPLEMENTARES DO INFORMANTE (Convênio ICMS 131/97)
            /// </summary>
            public Tipo11 Registro11
            {
                get { return _registro11; }
                set { _registro11 = value; }
            }

            /// <summary>
            /// REGISTRO TIPO 50
            /// NOTA FISCAL, MODELO 1 OU 1-A (código 01) , QUANTO AO ICMS
            /// NOTA FISCAL/CONTA DE ENERGIA ELÉTRICA, MODELO 6 (código 06),
            /// NOTA FISCAL DE SERVIÇO DE COMUNICAÇÃO, MODELO 21
            /// NOTA FISCAL DE SERVIÇOS DE TELECOMUNICAÇÕES, MODELO 22 (código 22)
            /// </summary>
            public List<Tipo50> ListaRegistro50
            {
                get { return _listaRegistro50; }
                set { _listaRegistro50 = value; }
            }

            /// <summary>
            /// REGISTRO TIPO 53 
            /// SUBSTITUIÇÃO TRIBUTÁRIA
            /// </summary>
            public List<Tipo53> ListaRegistro53
            {
                get { return _listaRegistro53; }
                set { _listaRegistro53 = value; }
            }

            /// <summary>
            /// REGISTRO TIPO 54 
            /// PRODUTO
            /// </summary>
            public List<Tipo54> ListaRegistro54
            {
                get { return _listaRegistro54; }
                set { _listaRegistro54 = value; }
            }

            public List<Tipo60> ListaRegistro60
            {
                get { return _listaRegistro60; }
                set { _listaRegistro60 = value; }
            }

            /// <summary>
            /// REGISTRO TIPO 75 (Convênio ICMS 76/03)
            /// CÓDIGO DE PRODUTO OU SERVIÇO
            /// </summary>
            public List<Tipo75> ListaRegistro75
            {
                get { return _listaRegistro75; }
                set { _listaRegistro75 = value; }
            }

            /// <summary>
            /// REGISTRO TIPO 90
            /// TOTALIZAÇÃO DO ARQUIVO
            /// </summary>
            public Tipo90 Registro90
            {
                get { return _registro90; }
                set { _registro90 = value; }
            }

            /// <summary>
            /// Lista responsável para ordenar os registros antes de gerar o arquivo
            /// </summary>
            private List<RegistroOrdem> RegistroOrdem
            {
                get { return _registroOrdem; }
                set { _registroOrdem = value; }
            }

            /// <summary>
            /// Método responsável em gerar o arquivo das notas (Saida/Entradas) conforme
            /// Regulamento do ICMS/PR, atualizado pelo Decreto n. 2.438, de 06/01/2004 (Convênio 76/03)
            /// </summary>
            /// <param name="pArquivo">Nome do arquivo a ser gerado</param>
            public void GeraArquivo(string pArquivo)
            {
                //Grava Registro Tipo 10
                RegistroOrdem ro10 = new RegistroOrdem();
                ro10.Linha = this.Registro10.ToString();
                ro10.Ordem = "10";
                this.RegistroOrdem.Add(ro10);

                //Grava Registro Tipo 11
                RegistroOrdem ro11 = new RegistroOrdem();
                ro11.Linha = this.Registro11.ToString();
                ro11.Ordem = "11";
                this.RegistroOrdem.Add(ro11);

                //Grava Registro Tipo 50
                foreach (Tipo50 registro50 in ListaRegistro50)
                {
                    RegistroOrdem ro50 = new RegistroOrdem();
                    ro50.Linha = registro50.ToString();
                    ro50.Ordem = "50" + ro50.Linha.Substring(30, 8);

                    this.RegistroOrdem.Add(ro50);
                }

                //Grava Registro Tipo 53
                foreach (Tipo53 registro53 in ListaRegistro53)
                {
                    RegistroOrdem ro53 = new RegistroOrdem();
                    ro53.Linha = registro53.ToString();
                    ro53.Ordem = "53" + ro53.Linha.Substring(30, 8);

                    this.RegistroOrdem.Add(ro53);
                }

                //Grava Registro Tipo 54
                foreach (Tipo54 registro54 in ListaRegistro54)
                {
                    RegistroOrdem ro54 = new RegistroOrdem();
                    ro54.Linha = registro54.ToString();
                    ro54.Ordem = "54" + ro54.Linha.Substring(2, 14) + ro54.Linha.Substring(18, 3) + ro54.Linha.Substring(21, 6) + ro54.Linha.Substring(34, 3);

                    this.RegistroOrdem.Add(ro54);
                }


                foreach (Tipo60 registro60 in ListaRegistro60)
                {
                    RegistroOrdem ro60 = new RegistroOrdem();
                    ro60.Linha = registro60.Registro;
                    ro60.Ordem = "60" + ro60.Linha;

                    this.RegistroOrdem.Add(ro60);
                }

                //Grava Registro Tipo 75
                foreach (Tipo75 registro75 in ListaRegistro75)
                {
                    RegistroOrdem ro75 = new RegistroOrdem();
                    ro75.Linha = registro75.ToString();
                    ro75.Ordem = "75" + ro75.Linha.Substring(18, 14);

                    this.RegistroOrdem.Add(ro75);
                }

                //Atualiza os totais de registros
                Registro90.Quantidade50 = ListaRegistro50.Count;
                Registro90.Quantidade53 = ListaRegistro53.Count;
                Registro90.Quantidade54 = ListaRegistro54.Count;
                Registro90.Quantidade60 = ListaRegistro60.Count;
                Registro90.Quantidade75 = ListaRegistro75.Count;

                //Grava Registro Tipo 90
                RegistroOrdem ro90 = new RegistroOrdem();
                ro90.Linha = Registro90.ToString();
                ro90.Ordem = "90";
                this.RegistroOrdem.Add(ro90);

                //Processo para gerar o arquivo para valida pr
                StreamWriter sw = new StreamWriter(pArquivo);

                foreach (RegistroOrdem ro in RegistroOrdem)
                {
                    sw.WriteLine(ro.Linha);
                }

                sw.Close();
            }

            /// <summary>
            /// Método responsável em tirar os Acentos de uma string
            /// </summary>
            /// <param name="texto">Texto com os Acentos</param>
            /// <returns>Texto sem os Acentos</returns>
            public static string retiraAcentos(string texto)
            {
                string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇçº";
                string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc.";
                for (int i = 0; i < comAcentos.Length; i++)
                {
                    texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
                }
                return texto;
            }
            #endregion
        }

        /// <summary>
        /// Classe com o atributo ordem para gravar o registro
        /// </summary>
        public class RegistroOrdem
        {
            private string _Linha;
            private string _Ordem;

            public string Linha
            {
                get { return _Linha; }
                set { _Linha = value; }
            }

            public string Ordem
            {
                get { return _Ordem; }
                set { _Ordem = value; }
            }
        }

        /// <summary>
        /// REGISTRO TIPO 10
        /// MESTRE DO ESTABELECIMENTO
        /// </summary>
        public class Tipo10
        {
            #region AtributosPrivados
            private string _Tipo;
            private string _CNPJ;
            private string _InscricaoEstadual;
            private string _Contribuinte;
            private string _Municipio;
            private string _Estado;
            private string _Fax;
            private DateTime _DataInicial;
            private DateTime _DataFinal;
            private string _CodigoConvenio;
            private string _CodigoIdentificacao;
            private string _CodigoFinalidade;
            #endregion

            #region Contrutor
            public Tipo10()
            {
                _Tipo = "10";
                _CodigoConvenio = "3";
                _CodigoIdentificacao = "3";
            }
            #endregion

            #region Métodos
            /// <summary>
            /// Fixo 10 (Somente Leitura)
            /// </summary>
            public string Tipo
            {
                get
                {
                    return _Tipo;
                }
            }

            /// <summary>
            /// CNPJ do estabelecimento informante
            /// </summary>
            public string CNPJ
            {
                get
                {
                    return _CNPJ;
                }
                set
                {
                    _CNPJ = value.Replace(".", "").Replace("/", "").Replace("-", "").Replace("\"", "");
                }
            }

            /// <summary>
            /// Inscrição estadual do estabelecimento informante
            /// </summary>
            public string InscricaoEstadual
            {
                get { return _InscricaoEstadual; }
                set 
                { 
                    _InscricaoEstadual = value.Replace(".", "").Replace("/", "").Replace("-", "").Replace("\"", ""); 
                }
            }

            /// <summary>
            /// Nome comercial (razão social / denominação) do contribuinte
            /// </summary>
            public string Contribuinte
            {
                get { return _Contribuinte; }
                set { _Contribuinte = value; }
            }

            /// <summary>
            /// Município onde está Domiciliado o estabelecimento informante
            /// </summary>
            public string Municipio
            {
                get { return _Municipio; }
                set { _Municipio = ValidaPR.retiraAcentos(value); }
            }

            /// <summary>
            /// Unidade da Federação referente ao  Município
            /// </summary>
            public string Estado
            {
                get { return _Estado; }
                set { _Estado = ValidaPR.retiraAcentos(value); }
            }

            /// <summary>
            /// Número do fax do estabelecimento informante
            /// </summary>
            public string Fax
            {
                get
                {
                    return _Fax;
                }
                set
                {
                    _Fax = value != null ? value.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") : "";
                }
            }

            /// <summary>
            /// A data do início do período referente às informações prestadas
            /// </summary>
            public DateTime DataInicial
            {
                get { return _DataInicial; }
                set { _DataInicial = value; }
            }

            /// <summary>
            /// A data do fim do período referente às informações prestadas
            /// </summary>
            public DateTime DataFinal
            {
                get { return _DataFinal; }
                set { _DataFinal = value; }
            }

            /// <summary>
            /// Fixo 3 (Código de Identificação do convenio 2	Convênio 69/02 / 3	Convênio 76/03) (Somente Leitura)
            /// </summary>
            public string CodigoConvenio
            {
                get { return _CodigoConvenio; }
            }

            /// <summary>
            /// Fixo 3 (3	Totalidade das operações do informante) (Somente Leitura)
            /// </summary>
            public string CodigoIdentificacao
            {
                get { return _CodigoIdentificacao; }
            }

            /// <summary>
            /// Código da finalidade utilizado no   arquivo magnético
            /// </summary>
            public string CodigoFinalidade
            {
                get { return _CodigoFinalidade; }
                set { _CodigoFinalidade = value; }
            }

            /// <summary>
            /// Método para montar a linha conforme layout do Valida-PR
            /// </summary>
            /// <returns>Linha formatada pronta para gravar</returns>
            public override string ToString()
            {
                // Monta a string para ser gravada no arquivo
                // As Colunas informadas no método Insert estão com o valor (-1) em relação ao layout da Receita
                string linha = "";
                linha = linha.Insert(0, String.Format("{0,-2}", this.Tipo));
                linha = linha.Insert(2, String.Format("{0,-14}", (this.CNPJ != null ? this.CNPJ : "")));
                linha = linha.Insert(16, String.Format("{0,-14}", (this.InscricaoEstadual != null ? this.InscricaoEstadual : "")));
                linha = linha.Insert(30, String.Format("{0,-35}", (this.Contribuinte != null ? this.Contribuinte.Substring(0, (this.Contribuinte.Length < 35 ? this.Contribuinte.Length : 35)) : "")));
                linha = linha.Insert(65, String.Format("{0,-30}", (this.Municipio != null ? this.Municipio.Substring(0, (this.Municipio.Length < 30 ? this.Municipio.Length : 30)) : "")));
                linha = linha.Insert(95, String.Format("{0,-2}", (this.Estado != null ? this.Estado : "")));
                linha = linha.Insert(97, String.Format("{0,-10}", (this.Fax != null ? (this.Fax.Length > 0 ? this.Fax.Trim().Substring(0, 10) : "") : "")));
                linha = linha.Insert(107, String.Format("{0,-8}", (this.DataInicial != null ? String.Format("{0:yyyyMMdd}", this.DataInicial) : "")));
                linha = linha.Insert(115, String.Format("{0,-8}", (this.DataFinal != null ? String.Format("{0:yyyyMMdd}", this.DataFinal) : "")));
                linha = linha.Insert(123, String.Format("{0,-1}", (this.CodigoConvenio != null ? this.CodigoConvenio : "")));
                linha = linha.Insert(124, String.Format("{0,-1}", (this.CodigoIdentificacao != null ? this.CodigoIdentificacao : "")));
                linha = linha.Insert(125, String.Format("{0,-1}", (this.CodigoFinalidade != null ? this.CodigoFinalidade : "")));

                return linha;
            }
            #endregion
        }

        /// <summary>
        /// REGISTRO TIPO 11
        /// DADOS COMPLEMENTARES DO INFORMANTE (Convênio ICMS 131/97)
        /// </summary>
        public class Tipo11
        {
            #region AtributosPrivados
            private string _Tipo;
            private string _Logradouro;
            private int _Numero;
            private string _Complemento;
            private string _Bairro;
            private string _Cep;
            private string _Contato;
            private string _Telefone;
            #endregion

            #region Construtor
            public Tipo11()
            {
                this._Tipo = "11";
            }
            #endregion

            #region Métodos
            /// <summary>
            /// Fixo 11 (Somente Leitura)
            /// </summary>
            public string Tipo
            {
                get
                {
                    return _Tipo;
                }
            }

            /// <summary>
            /// Endereço da Filial
            /// </summary>
            public string Logradouro
            {
                get { return _Logradouro; }
                set { _Logradouro = ValidaPR.retiraAcentos(value); }
            }

            /// <summary>
            /// Número do endereço da Filial
            /// </summary>
            public int Numero
            {
                get { return _Numero; }
                set { _Numero = value; }
            }

            /// <summary>
            /// Complemento do endereço da Filial
            /// </summary>
            public string Complemento
            {
                get { return _Complemento; }
                set { _Complemento = ValidaPR.retiraAcentos(value); }
            }

            /// <summary>
            /// Bairro do endereço da Filial
            /// </summary>
            public string Bairro
            {
                get { return _Bairro; }
                set { _Bairro = ValidaPR.retiraAcentos(value); }
            }

            /// <summary>
            /// Cep do endereçco da Filial
            /// </summary>
            public string Cep
            {
                get { return _Cep; }
                set { _Cep = value.Replace("-", ""); }
            }

            /// <summary>
            /// Nome do Responsável na Filial
            /// </summary>
            public string Contato
            {
                get { return _Contato; }
                set { _Contato = ValidaPR.retiraAcentos(value); }
            }

            /// <summary>
            /// Telefone da Filial
            /// </summary>
            public string Telefone
            {
                get { return _Telefone; }
                set { _Telefone = value != null ? (value.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "")).TrimEnd() : ""; }
            }

            /// <summary>
            /// Método para montar a linha conforme layout do Valida-PR
            /// </summary>
            /// <returns>Linha formatada pronta para gravar</returns>
            public override string ToString()
            {
                // Monta a string para ser gravada no arquivo
                // As Colunas informadas no método Insert estão com o valor (-1) em relação ao layout da Receita
                string linha = "";
                linha = linha.Insert(0, String.Format("{0,-2}", this.Tipo));
                linha = linha.Insert(2, String.Format("{0,-34}", this.Logradouro != null ? this.Logradouro.Trim().Substring(0, (this.Logradouro.Length < 34 ? this.Logradouro.Length : 34)) : ""));
                linha = linha.Insert(36, String.Format("{0:D5}", this.Numero));
                linha = linha.Insert(41, String.Format("{0,-22}", this.Complemento != null ? this.Complemento.Trim().Substring(0, (this.Complemento.Length < 22 ? this.Complemento.Length : 22)) : ""));
                linha = linha.Insert(63, String.Format("{0,-15}", this.Bairro != null ? this.Bairro.Trim().Substring(0, (this.Bairro.Length < 15 ? this.Bairro.Length : 15)) : ""));
                linha = linha.Insert(78, String.Format("{0,-8}", this.Cep != null ? this.Cep.Trim().Substring(0, (this.Cep.Length < 8 ? this.Cep.Length : 8)) : ""));
                linha = linha.Insert(86, String.Format("{0,-28}", this.Contato != null ? this.Contato.Trim().Substring(0, (this.Contato.Length < 28 ? this.Contato.Length : 28)) : ""));
                linha = linha.Insert(114, String.Format("{0:D12}", "00" + (this.Telefone != null ? this.Telefone.Trim().Substring(0, 10) : "")));

                return linha;
            }
            #endregion
        }

        /// <summary>
        /// REGISTRO TIPO 50
        /// NOTA FISCAL, MODELO 1 OU 1-A (código 01) , QUANTO AO ICMS
        /// NOTA FISCAL/CONTA DE ENERGIA ELÉTRICA, MODELO 6 (código 06),
        /// NOTA FISCAL DE SERVIÇO DE COMUNICAÇÃO, MODELO 21
        /// NOTA FISCAL DE SERVIÇOS DE TELECOMUNICAÇÕES, MODELO 22 (código 22)
        /// </summary>
        public class Tipo50
        {
            #region AtributosPrivados
            private string _Tipo;
            private string _CNPJ;
            private string _InscricaoEstadual;
            private DateTime _Data;
            private string _Estado;
            private int _Modelo;
            private string _Serie;
            private int _NumeroNF;
            private int _CFOP;
            private string _Emitente;
            private Decimal _ValorTotal;
            private Decimal _BaseCalculoICMS;
            private Decimal _ValorICMS;
            private Decimal _ValorIsentoICMS;
            private Decimal _ValorOutras;
            private Decimal _Aliquota;
            private string _Situacao;
            #endregion

            #region Construtor
            public Tipo50()
            {
                _Tipo = "50";
                this.Modelo = 0;
                this.NumeroNF = 0;
                this.CFOP = 0;
                this.ValorTotal = 0;
                this.BaseCalculoICMS = 0;
                this.ValorICMS = 0;
                this.ValorIsentoICMS = 0;
                this.ValorOutras = 0;
                this.Aliquota = 0;
            }
            #endregion

            #region Métodos
            /// <summary>
            /// Tipo "50"
            /// </summary>
            public string Tipo
            {
                get { return _Tipo; }
            }

            /// <summary>
            /// CNPJ do remetente nas entradas e do destinatário nas saídas
            /// </summary>
            public string CNPJ
            {
                get
                {
                    return _CNPJ;
                }
                set
                {
                    if (value != null)
                        _CNPJ = value.Replace(".", "").Replace("/", "").Replace("-", "").Replace("\"", "");
                    else
                        _CNPJ = "";
                }
            }

            /// <summary>
            /// Inscrição estadual do remetente nas entradas e do destinatário nas saídas
            /// </summary>
            public string InscricaoEstadual
            {
                get { return _InscricaoEstadual; }
                set 
                {
                    if (value != null)
                        _InscricaoEstadual = value.Replace(".", "").Replace("/", "").Replace("-", "").Replace("\"", "");
                    else
                        _InscricaoEstadual = "";
                }
            }

            /// <summary>
            /// Data de emissão na saída ou de recebimento na entrada
            /// </summary>
            public DateTime Data
            {
                get { return _Data; }
                set { _Data = value; }
            }

            /// <summary>
            /// Sigla da unidade da Federação do remetente nas entradas e do destinatário nas saídas
            /// </summary>
            public string Estado
            {
                get { return _Estado; }
                set { _Estado = value; }
            }

            /// <summary>
            /// Código do modelo da nota fiscal	
            /// </summary>
            public int Modelo
            {
                get { return _Modelo; }
                set { _Modelo = value; }
            }

            /// <summary>
            /// Série da nota fiscal
            /// </summary>
            public string Serie
            {
                get { return _Serie; }
                set { _Serie = value; }
            }

            /// <summary>
            /// Número da nota fiscal
            /// </summary>
            public int NumeroNF
            {
                get { return _NumeroNF; }
                set { _NumeroNF = value; }
            }

            /// <summary>
            /// Código Fiscal de Operação e Prestação
            /// </summary>
            public int CFOP
            {
                get { return _CFOP; }
                set { _CFOP = value; }
            }

            /// <summary>
            /// Emitente da Nota Fiscal (P-próprio/T-terceiros)
            /// </summary>
            public string Emitente
            {
                get { return _Emitente; }
                set { _Emitente = value; }
            }

            /// <summary>
            /// Valor total da nota fiscal (com 2 decimais)
            /// </summary>
            public Decimal ValorTotal
            {
                get { return _ValorTotal; }
                set { _ValorTotal = value; }
            }

            /// <summary>
            /// Base de Cálculo do ICMS (com 2 decimais)
            /// </summary>
            public Decimal BaseCalculoICMS
            {
                get { return _BaseCalculoICMS; }
                set { _BaseCalculoICMS = value; }
            }

            /// <summary>
            /// Montante do imposto ICMS (com 2 decimais
            /// </summary>
            public Decimal ValorICMS
            {
                get { return _ValorICMS; }
                set { _ValorICMS = value; }
            }

            /// <summary>
            /// Valor amparado por isenção ou   não  incidência (com 2 decimais)
            /// </summary>
            public Decimal ValorIsentoICMS
            {
                get { return _ValorIsentoICMS; }
                set { _ValorIsentoICMS = value; }
            }

            /// <summary>
            /// Valor que não confira débito ou crédito do ICMS (com 2 decimais)
            /// </summary>
            public Decimal ValorOutras
            {
                get { return _ValorOutras; }
                set { _ValorOutras = value; }
            }

            /// <summary>
            /// Alíquota do ICMS (com 2 decimais)
            /// </summary>
            public Decimal Aliquota
            {
                get { return _Aliquota; }
                set { _Aliquota = value; }
            }

            /// <summary>
            /// Situação da nota fiscal quanto ao cancelamento (S-cancelada, N-não cancelada)
            /// </summary>
            public string Situacao
            {
                get { return _Situacao; }
                set { _Situacao = value; }
            }

            /// <summary>
            /// Método para montar a linha conforme layout do Valida-PR
            /// </summary>
            /// <returns>Linha formatada pronta para gravar</returns>
            public override string ToString()
            {
                // Monta a string para ser gravada no arquivo
                // As Colunas informadas no método Insert estão com o valor (-1) em relação ao layout da Receita
                string linha = "";
                linha = linha.Insert(0, String.Format("{0,-2}", this.Tipo));
                linha = linha.Insert(2, String.Format("{0,-14}", (String.IsNullOrEmpty(this.CNPJ) == false ? String.Format("{0:00000000000000}", Convert.ToInt64(this.CNPJ)) : "00000000000000")));
                linha = linha.Insert(16, String.Format("{0,-14}", (this.Estado == "EX" ? "ISENTO" : this.InscricaoEstadual != null ? this.InscricaoEstadual : "")));
                linha = linha.Insert(30, String.Format("{0,-8}", (this.Data != null ? String.Format("{0:yyyyMMdd}", this.Data) : "")));
                linha = linha.Insert(38, String.Format("{0,-2}", (this.Estado != null ? this.Estado : "")));
                linha = linha.Insert(40, String.Format("{0:D2}", this.Modelo));
                linha = linha.Insert(42, String.Format("{0,-3}", (this.Serie != null ? this.Serie : "")));
                linha = linha.Insert(45, String.Format("{0:D6}", this.NumeroNF));
                linha = linha.Insert(51, String.Format("{0:D4}", this.CFOP));
                linha = linha.Insert(55, String.Format("{0,-1}", (this.Emitente != null ? this.Emitente : "")));
                linha = linha.Insert(56, String.Format("{0:D13}", Convert.ToInt32((this.ValorTotal * 100))));
                linha = linha.Insert(69, String.Format("{0:D13}", Convert.ToInt32((this.BaseCalculoICMS * 100))));
                linha = linha.Insert(82, String.Format("{0:D13}", Convert.ToInt32((this.ValorICMS * 100))));
                linha = linha.Insert(95, String.Format("{0:D13}", Convert.ToInt32((this.ValorIsentoICMS * 100))));
                linha = linha.Insert(108, String.Format("{0:D13}", Convert.ToInt32((this.ValorOutras * 100))));
                linha = linha.Insert(121, String.Format("{0:D4}", Convert.ToInt32((this.Aliquota * 100))));
                linha = linha.Insert(125, String.Format("{0,-1}", (this.Situacao != null ? this.Situacao : "")));

                return linha;
            }

            #endregion
        }

        /// <summary>
        /// REGISTRO TIPO 53 
        /// SUBSTITUIÇÃO TRIBUTÁRIA
        /// </summary>
        public class Tipo53
        {
            #region AtributosPrivados
            private string _Tipo;
            private string _CNPJ;
            private string _InscricaoEstadual;
            private DateTime _Data;
            private string _Estado;
            private int _Modelo;
            private string _Serie;
            private int _NumeroNF;
            private int _CFOP;
            private string _Emitente;
            private Decimal _BaseCalculoICMS_ST;
            private Decimal _ValorICMSRetido;
            private Decimal _ValorDespesas;
            private string _Situacao;
            private string _CodigoAntecipacao;
            #endregion

            #region Construtor
            public Tipo53()
            {
                _Tipo = "53";
                this.Modelo = 0;
                this.NumeroNF = 0;
                this.CFOP = 0;
                this.BaseCalculoICMS_ST = 0;
                this.ValorICMSRetido = 0;
                this.ValorDespesas = 0;
            }
            #endregion

            #region Métodos
            /// <summary>
            /// Tipo "53"
            /// </summary>
            public string Tipo
            {
                get { return _Tipo; }
            }

            /// <summary>
            /// CNPJ do remetente nas entradas e do destinatário nas saídas
            /// </summary>
            public string CNPJ
            {
                get
                {
                    return _CNPJ;
                }
                set
                {
                    _CNPJ = value.Replace(".", "").Replace("/", "").Replace("-", "").Replace("\"", "");
                }
            }

            /// <summary>
            /// Inscrição estadual do remetente nas entradas e do destinatário nas saídas
            /// </summary>
            public string InscricaoEstadual
            {
                get { return _InscricaoEstadual; }
                set
                {
                    if (value != null)
                        _InscricaoEstadual = value.Replace(".", "").Replace("/", "").Replace("-", "").Replace("\"", "");
                    else
                        _InscricaoEstadual = "";
                }
            }

            /// <summary>
            /// Data de emissão na saída ou de recebimento na entrada
            /// </summary>
            public DateTime Data
            {
                get { return _Data; }
                set { _Data = value; }
            }

            /// <summary>
            /// Sigla da unidade da Federação do remetente nas entradas e do destinatário nas saídas
            /// </summary>
            public string Estado
            {
                get { return _Estado; }
                set { _Estado = value; }
            }

            /// <summary>
            /// Código do modelo da nota fiscal	
            /// </summary>
            public int Modelo
            {
                get { return _Modelo; }
                set { _Modelo = value; }
            }

            /// <summary>
            /// Série da nota fiscal
            /// </summary>
            public string Serie
            {
                get { return _Serie; }
                set { _Serie = value; }
            }

            /// <summary>
            /// Número da nota fiscal
            /// </summary>
            public int NumeroNF
            {
                get { return _NumeroNF; }
                set { _NumeroNF = value; }
            }

            /// <summary>
            /// Código Fiscal de Operação e Prestação
            /// </summary>
            public int CFOP
            {
                get { return _CFOP; }
                set { _CFOP = value; }
            }

            /// <summary>
            /// Emitente da Nota Fiscal (P-próprio/T-terceiros)
            /// </summary>
            public string Emitente
            {
                get { return _Emitente; }
                set { _Emitente = value; }
            }

            /// <summary>
            /// Base de cálculo de retenção do ICMS (com 2 decimais)
            /// </summary>
            public Decimal BaseCalculoICMS_ST
            {
                get { return _BaseCalculoICMS_ST; }
                set { _BaseCalculoICMS_ST = value; }
            }

            /// <summary>
            /// ICMS retido pelo substituto (com 2  decimais)
            /// </summary>
            public Decimal ValorICMSRetido
            {
                get { return _ValorICMSRetido; }
                set { _ValorICMSRetido = value; }
            }

            /// <summary>
            /// Soma das despesas acessórias (frete,  seguro e outras - com 2 decimais)
            /// </summary>
            public Decimal ValorDespesas
            {
                get { return _ValorDespesas; }
                set { _ValorDespesas = value; }
            }

            /// <summary>
            /// Situação da nota fiscal quanto ao cancelamento (S-cancelada, N-não cancelada)
            /// </summary>
            public string Situacao
            {
                get { return _Situacao; }
                set { _Situacao = value; }
            }

            /// <summary>
            /// Código que identifica o tipo de antecipação tributária
            /// </summary>
            public string CodigoAntecipacao
            {
                get { return _CodigoAntecipacao; }
                set { _CodigoAntecipacao = value; }
            }

            /// <summary>
            /// Método para montar a linha conforme layout do Valida-PR
            /// </summary>
            /// <returns>Linha formatada pronta para gravar</returns>
            public override string ToString()
            {
                // Monta a string para ser gravada no arquivo
                // As Colunas informadas no método Insert estão com o valor (-1) em relação ao layout da Receita
                string linha = "";
                linha = linha.Insert(0, String.Format("{0,-2}", this.Tipo));
                linha = linha.Insert(2, String.Format("{0,-14}", (String.IsNullOrEmpty(this.CNPJ) == false ? String.Format("{0:00000000000000}", Convert.ToInt64(this.CNPJ)) : "00000000000000")));
                linha = linha.Insert(16, String.Format("{0,-14}", (this.InscricaoEstadual != null ? this.InscricaoEstadual : "")));
                linha = linha.Insert(30, String.Format("{0,-8}", (this.Data != null ? String.Format("{0:yyyyMMdd}", this.Data) : "")));
                linha = linha.Insert(38, String.Format("{0,-2}", (this.Estado != null ? this.Estado : "")));
                linha = linha.Insert(40, String.Format("{0:D2}", this.Modelo));
                linha = linha.Insert(42, String.Format("{0,-3}", (this.Serie != null ? this.Serie : "")));
                linha = linha.Insert(45, String.Format("{0:D6}", this.NumeroNF));
                linha = linha.Insert(51, String.Format("{0:D4}", this.CFOP));
                linha = linha.Insert(55, String.Format("{0,-1}", (this.Emitente != null ? this.Emitente : "")));
                linha = linha.Insert(56, String.Format("{0:D13}", Convert.ToInt32((this.BaseCalculoICMS_ST * 100))));
                linha = linha.Insert(69, String.Format("{0:D13}", Convert.ToInt32((this.ValorICMSRetido * 100))));
                linha = linha.Insert(82, String.Format("{0:D13}", Convert.ToInt32((this.ValorDespesas * 100))));
                linha = linha.Insert(95, String.Format("{0,-1}", (this.Situacao != null ? this.Situacao : "")));
                linha = linha.Insert(96, String.Format("{0,-1}", (this.CodigoAntecipacao != null ? this.CodigoAntecipacao : "")));
                linha = linha.Insert(97, String.Format("{0,29}", ""));

                return linha;
            }

            #endregion
        }

        /// <summary>
        /// REGISTRO TIPO 54 
        /// PRODUTO
        /// </summary>
        public class Tipo54
        {
            #region AtributosPrivados
            private string _tipo;
            private string _cnpj;
            private int _modelo;
            private string _serie;
            private int _numeroNF;
            private int _cfop;
            private string _cst;
            private int _numeroItem;
            private string _codigoProduto;
            private Decimal _quantidade;
            private Decimal _valorProduto;
            private Decimal _valorDesconto;
            private Decimal _baseCalculoIcms;
            private Decimal _baseCalculoIcms_ST;
            private Decimal _valorIPI;
            private Decimal _aliquotaIcms;
            #endregion

            #region Construtor
            public Tipo54()
            {
                _tipo = "54";
                this.Modelo = 0;
                this.NumeroNF = 0;
                this.CFOP = 0;
            }
            #endregion

            #region Métodos
            /// <summary>
            /// Tipo "54"
            /// </summary>
            public string Tipo
            {
                get { return _tipo; }
            }

            /// <summary>
            /// CNPJ do remetente nas entradas e do destinatário nas saídas
            /// </summary>
            public string CNPJ
            {
                get
                {
                    return _cnpj;
                }
                set
                {
                    if (value != null)
                        _cnpj = value.Replace(".", "").Replace("/", "").Replace("-", "").Replace("\"", "");
                    else
                        _cnpj = "";
                }
            }

            /// <summary>
            /// Código do modelo da nota fiscal	
            /// </summary>
            public int Modelo
            {
                get { return _modelo; }
                set { _modelo = value; }
            }

            /// <summary>
            /// Série da nota fiscal
            /// </summary>
            public string Serie
            {
                get { return _serie; }
                set { _serie = value; }
            }

            /// <summary>
            /// Número da nota fiscal
            /// </summary>
            public int NumeroNF
            {
                get { return _numeroNF; }
                set { _numeroNF = value; }
            }

            /// <summary>
            /// Código Fiscal de Operação e Prestação
            /// </summary>
            public int CFOP
            {
                get { return _cfop; }
                set { _cfop = value; }
            }

            /// <summary>
            /// Código da Situação Tributária
            /// </summary>
            public string CST
            {
                get { return _cst; }
                set { _cst = value; }
            }

            /// <summary>
            /// Número de ordem do item na nota fiscal
            /// </summary>
            public int NumeroItem
            {
                get { return _numeroItem; }
                set { _numeroItem = value; }
            }

            /// <summary>
            /// Código do produto ou serviço do informante
            /// </summary>
            public string CodigoProduto
            {
                get { return _codigoProduto; }
                set { _codigoProduto = value; }
            }

            /// <summary>
            /// Quantidade do produto (com 3 decimais)
            /// </summary>
            public Decimal Quantidade
            {
                get { return _quantidade; }
                set { _quantidade = value; }
            }

            /// <summary>
            /// Valor bruto do produto - valor unitário multiplicado por quantidade (com 2 decimais)
            /// </summary>
            public Decimal ValorProduto
            {
                get { return _valorProduto; }
                set { _valorProduto = value; }
            }

            /// <summary>
            /// Valor do Desconto  Concedido no  item (com 2 decimais)
            /// </summary>
            public Decimal ValorDesconto
            {
                get { return _valorDesconto; }
                set { _valorDesconto = value; }
            }

            /// <summary>
            /// Base de cálculo do ICMS (com 2  decimais)
            /// </summary>
            public Decimal BaseCalculoIcms
            {
                get { return _baseCalculoIcms; }
                set { _baseCalculoIcms = value; }
            }

            /// <summary>
            /// Base de cálculo do ICMS de  retenção na Substituição Tributária  (com 2 decimais)
            /// </summary>
            public Decimal BaseCalculoIcms_ST
            {
                get { return _baseCalculoIcms_ST; }
                set { _baseCalculoIcms_ST = value; }
            }

            /// <summary>
            /// Valor do IPI (com 2 decimais)	
            /// </summary>
            public Decimal ValorIPI
            {
                get { return _valorIPI; }
                set { _valorIPI = value; }
            }

            /// <summary>
            /// Alíquota utilizada no cálculo do  ICMS  (com 2 decimais)
            /// </summary>
            public Decimal AliquotaIcms
            {
                get { return _aliquotaIcms; }
                set { _aliquotaIcms = value; }
            }

            public override string ToString()
            {
                // Monta a string para ser gravada no arquivo
                // As Colunas informadas no método Insert estão com o valor (-1) em relação ao layout da Receita
                string linha = "";
                linha = linha.Insert(0, String.Format("{0,-2}", this.Tipo));
                linha = linha.Insert(2, String.Format("{0,-14}", (String.IsNullOrEmpty(this.CNPJ) == false ? String.Format("{0:00000000000000}", Convert.ToInt64(this.CNPJ)) : "00000000000000")));
                linha = linha.Insert(16, String.Format("{0:D2}", this.Modelo));
                linha = linha.Insert(18, String.Format("{0,-3}", (this.Serie != null ? this.Serie : "")));
                linha = linha.Insert(21, String.Format("{0:D6}", this.NumeroNF));
                linha = linha.Insert(27, String.Format("{0:D4}", this.CFOP));
                linha = linha.Insert(31, String.Format("{0,-3}", (this.CST != null ? this.CST : "")));
                linha = linha.Insert(34, String.Format("{0:D3}", this.NumeroItem));
                linha = linha.Insert(37, String.Format("{0,-14}", (this.CodigoProduto != null ? this.CodigoProduto : "")));
                linha = linha.Insert(51, String.Format("{0:D11}", Convert.ToInt32((this.Quantidade * 1000))));
                linha = linha.Insert(62, String.Format("{0:D12}", Convert.ToInt32((this.ValorProduto * 100))));
                linha = linha.Insert(74, String.Format("{0:D12}", Convert.ToInt32((this.ValorDesconto * 100))));
                linha = linha.Insert(86, String.Format("{0:D12}", Convert.ToInt32((this.BaseCalculoIcms * 100))));
                linha = linha.Insert(98, String.Format("{0:D12}", Convert.ToInt32((this.BaseCalculoIcms_ST * 100))));
                linha = linha.Insert(110, String.Format("{0:D12}", Convert.ToInt32((this.ValorIPI * 100))));
                linha = linha.Insert(122, String.Format("{0:D4}", Convert.ToInt32((this.AliquotaIcms * 100))));

                return linha;
            }
            #endregion
        }

        public class Tipo60 
        {
#pragma warning disable CS0414 // O campo "ExpNotaValidaPR.Tipo60._tipo" é atribuído, mas seu valor nunca é usado
            private string _tipo;
#pragma warning restore CS0414 // O campo "ExpNotaValidaPR.Tipo60._tipo" é atribuído, mas seu valor nunca é usado
            private string _Registro;

            public Tipo60() 
            {
                this._tipo = "60";
            }

            public string Registro
            {
                get { return _Registro; }
                set { _Registro = value; }
            }

        }

        /// <summary>
        /// REGISTRO TIPO 75 (Convênio ICMS 76/03)
        /// CÓDIGO DE PRODUTO OU SERVIÇO
        /// </summary>
        public class Tipo75
        {
            #region AtributosPrivados
            private string _tipo;
            private DateTime _dataInicial;
            private DateTime _dataFinal;
            private string _codigoProduto;
            private string _codigoNCM;
            private string _descricaoProduto;
            private string _unidade;
            private Decimal _aliquotaIPI;
            private Decimal _aliquotaICMS;
            private Decimal _reducaoBaseCalculo;
            private Decimal _baseCalculoST;
            #endregion

            #region Construtor
            public Tipo75()
            {
                _tipo = "75";
                _aliquotaIPI = 0;
                _aliquotaICMS = 0;
                _reducaoBaseCalculo = 0;
                _baseCalculoST = 0;
            }

            #endregion

            #region Métodos
            /// <summary>
            /// Tipo "75"
            /// </summary>
            public string Tipo
            {
                get { return _tipo; }
            }

            /// <summary>
            /// Data inicial do período de validade  das informações
            /// </summary>
            public DateTime DataInicial
            {
                get { return _dataInicial; }
                set { _dataInicial = value; }
            }

            /// <summary>
            /// Data final do período de validade das  informações
            /// </summary>
            public DateTime DataFinal
            {
                get { return _dataFinal; }
                set { _dataFinal = value; }
            }

            /// <summary>
            /// Código do produto ou serviço  utilizado pelo contribuinte
            /// </summary>
            public string CodigoProduto
            {
                get { return _codigoProduto; }
                set { _codigoProduto = value; }
            }

            /// <summary>
            /// Codificação da Nomenclatura Comum  do Mercosul
            /// </summary>
            public string CodigoNCM
            {
                get { return _codigoNCM; }
                set { _codigoNCM = value; }
            }

            /// <summary>
            /// Descrição do produto ou Serviço
            /// </summary>
            public string DescricaoProduto
            {
                get { return _descricaoProduto; }
                set { _descricaoProduto = ValidaPR.retiraAcentos(value); }
            }

            /// <summary>
            /// Unidade de medida de  comercialização do produto(un, kg,  mt, m3, sc, frd, kwh, etc.)
            /// </summary>
            public string Unidade
            {
                get { return _unidade; }
                set { _unidade = ValidaPR.retiraAcentos(value); }
            }

            /// <summary>
            /// Alíquota do IPI do produto (com 2 decimais)
            /// </summary>
            public Decimal AliquotaIPI
            {
                get { return _aliquotaIPI; }
                set { _aliquotaIPI = value; }
            }

            /// <summary>
            /// Alíquota do ICMS aplicável a  mercadoria ou serviço nas operações  ou prestações internas ou naquelas  
            /// que tiverem sido iniciadas no exterior (com 2 decimais)
            /// </summary>
            public Decimal AliquotaICMS
            {
                get { return _aliquotaICMS; }
                set { _aliquotaICMS = value; }
            }

            /// <summary>
            /// % de Redução na base de cálculo do  ICMS, nas operações internas (com 2   decimais)
            /// </summary>
            public Decimal ReducaoBaseCalculo
            {
                get { return _reducaoBaseCalculo; }
                set { _reducaoBaseCalculo = value; }
            }

            /// <summary>
            /// Base de Cálculo do ICMS de  substituição tributária (com 2  decimais)
            /// </summary>
            public Decimal BaseCalculoST
            {
                get { return _baseCalculoST; }
                set { _baseCalculoST = value; }
            }

            /// <summary>
            /// Método para montar a linha conforme layout do Valida-PR
            /// </summary>
            /// <returns>Linha formatada pronta para gravar</returns>
            public override string ToString()
            {
                // Monta a string para ser gravada no arquivo
                // As Colunas informadas no método Insert estão com o valor (-1) em relação ao layout da Receita
                string linha = "";
                linha = linha.Insert(0, String.Format("{0,-2}", this.Tipo));
                linha = linha.Insert(2, String.Format("{0,-8}", (this.DataInicial != null ? String.Format("{0:yyyyMMdd}", this.DataInicial) : "")));
                linha = linha.Insert(10, String.Format("{0,-8}", (this.DataFinal != null ? String.Format("{0:yyyyMMdd}", this.DataFinal) : "")));
                linha = linha.Insert(18, String.Format("{0,-14}", (this.CodigoProduto != null ? this.CodigoProduto : "")));
                linha = linha.Insert(32, String.Format("{0,-8}", (this.CodigoNCM != null ? this.CodigoNCM : "")));
                linha = linha.Insert(40, String.Format("{0,-53}", this.DescricaoProduto != null ? this.DescricaoProduto.Substring(0, (this.DescricaoProduto.Length < 53 ? this.DescricaoProduto.Length : 53)) : ""));
                linha = linha.Insert(93, String.Format("{0,-6}", (this.Unidade != null ? this.Unidade : "")));
                linha = linha.Insert(99, String.Format("{0:D5}", Convert.ToInt32((this.AliquotaIPI * 100))));
                linha = linha.Insert(104, String.Format("{0:D4}", Convert.ToInt32((this.AliquotaICMS * 100))));
                linha = linha.Insert(108, String.Format("{0:D5}", Convert.ToInt32((this.ReducaoBaseCalculo * 100))));
                linha = linha.Insert(113, String.Format("{0:D13}", Convert.ToInt32((this.BaseCalculoST * 100))));

                return linha;
            }

            #endregion
        }

        /// <summary>
        /// REGISTRO TIPO 90
        /// TOTALIZAÇÃO DO ARQUIVO
        /// </summary>
        public class Tipo90
        {
            #region AtributosPrivados
            private string _tipo;
            private string _cnpj;
            private string _inscricaoEstadual;
            private int _quantidade50;
            private int _quantidade53;
            private int _quantidade54;
            private int _quantidade60;
            private int _quantidade75;
            #endregion

            #region Construtor
            public Tipo90()
            {
                _tipo = "90";
                _quantidade50 = 0;
                _quantidade53 = 0;
                _quantidade54 = 0;
                _quantidade60 = 0;
                _quantidade75 = 0;
            }
            #endregion

            #region Métodos
            /// <summary>
            /// Tipo "90"
            /// </summary>
            public string Tipo
            {
                get { return _tipo; }
            }

            /// <summary>
            /// CNPJ do remetente nas entradas e do destinatário nas saídas
            /// </summary>
            public string CNPJ
            {
                get
                {
                    return _cnpj;
                }
                set
                {
                    _cnpj = value.Replace(".", "").Replace("/", "").Replace("-", "").Replace("\"", "");
                }
            }

            /// <summary>
            /// Inscrição estadual do remetente nas entradas e do destinatário nas saídas
            /// </summary>
            public string InscricaoEstadual
            {
                get { return _inscricaoEstadual; }
                set { _inscricaoEstadual = value; }
            }

            /// <summary>
            /// Quantidade de registros com tipo 50
            /// </summary>
            public int Quantidade50
            {
                get { return _quantidade50; }
                set { _quantidade50 = value; }
            }

            /// <summary>
            /// Quantidade de registros com tipo 53
            /// </summary>
            public int Quantidade53
            {
                get { return _quantidade53; }
                set { _quantidade53 = value; }
            }

            /// <summary>
            /// Quantidade de registros com tipo 50
            /// </summary>
            public int Quantidade54
            {
                get { return _quantidade54; }
                set { _quantidade54 = value; }
            }

            public int Quantidade60
            {
                get { return _quantidade60; }
                set { _quantidade60 = value; }
            }

            /// <summary>
            /// Quantidade de registros com tipo 75
            /// </summary>
            public int Quantidade75
            {
                get { return _quantidade75; }
                set { _quantidade75 = value; }
            }

            /// <summary>
            /// Quantidade de registros no geral
            /// </summary>
            public int Quantidade99
            {
                get { return Quantidade50 + Quantidade53 + Quantidade54 + Quantidade60 + Quantidade75 + 3; }
            }

            /// <summary>
            /// Método para montar a linha conforme layout do Valida-PR
            /// </summary>
            /// <returns>Linha formatada pronta para gravar</returns>
            public override string ToString()
            {
                // Monta a string para ser gravada no arquivo
                // As Colunas informadas no método Insert estão com o valor (-1) em relação ao layout da Receita
                string linha = new string(' ', 126);
                linha = linha.Insert(0, String.Format("{0,-2}", Tipo));
                linha = linha.Insert(2, String.Format("{0,-14}", (String.IsNullOrEmpty(this.CNPJ) == false ? this.CNPJ : "00000000000000")));
                linha = linha.Insert(16, String.Format("{0,-14}", (InscricaoEstadual != null ? InscricaoEstadual : "")));

                int pos_tipo = 20;
                int pos_reg = 22;

                //Verifica se possui registro tipo 50
                if (Quantidade50 > 0)
                {
                    pos_tipo = pos_tipo + 10;
                    pos_reg = pos_reg + 10;

                    linha = linha.Insert(pos_tipo, String.Format("{0,-2}", "50"));
                    linha = linha.Insert(pos_reg, String.Format("{0:D8}", Quantidade50));
                }

                //Verifica se possui registro tipo 53
                if (Quantidade53 > 0)
                {
                    pos_tipo = pos_tipo + 10;
                    pos_reg = pos_reg + 10;

                    linha = linha.Insert(pos_tipo, String.Format("{0,-2}", "53"));
                    linha = linha.Insert(pos_reg, String.Format("{0:D8}", Quantidade53));
                }

                //Verifica se possui registro tipo 54
                if (Quantidade54 > 0)
                {
                    pos_tipo = pos_tipo + 10;
                    pos_reg = pos_reg + 10;

                    linha = linha.Insert(pos_tipo, String.Format("{0,-2}", "54"));
                    linha = linha.Insert(pos_reg, String.Format("{0:D8}", Quantidade54));
                }

                if (Quantidade60 > 0) 
                {
                    pos_tipo = pos_tipo + 10;
                    pos_reg = pos_reg + 10;

                    linha = linha.Insert(pos_tipo, String.Format("{0,-2}", "60"));
                    linha = linha.Insert(pos_reg, String.Format("{0:D8}", Quantidade60));
                }

                //Verifica se possui registro tipo 75
                if (Quantidade75 > 0)
                {
                    pos_tipo = pos_tipo + 10;
                    pos_reg = pos_reg + 10;

                    linha = linha.Insert(pos_tipo, String.Format("{0,-2}", "75"));
                    linha = linha.Insert(pos_reg, String.Format("{0:D8}", Quantidade75));
                }

                //Verifica se possui registro tipo 99
                if (Quantidade99 > 0)
                {
                    pos_tipo = pos_tipo + 10;
                    pos_reg = pos_reg + 10;

                    linha = linha.Insert(pos_tipo, String.Format("{0,-2}", "99"));
                    linha = linha.Insert(pos_reg, String.Format("{0:D8}", Quantidade99));
                }

                linha = linha.Insert(125, String.Format("{0,-1}", "1"));
                linha = linha.TrimEnd();

                return linha;
            }

            #endregion
        }
    }
}