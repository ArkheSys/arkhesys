using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FiscalPrinterBematech;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Negocio.Utils;

namespace cwkGestao.Negocio
{
    public class CupomFiscal
    {
        CupomFiscalFactory cupomFactory;
        ICupomFiscal cupomFiscal;

        public CupomFiscal(string arquitetura)
        {
            cupomFactory = new CupomFiscalFactory();
            cupomFiscal = cupomFactory.Build("x86");
        }

        public void Imprime(Modelo.Nota objNota, List<pxyFormaPagamentoBaixaDoc> formaPagamento)
        {
            int IRetorno;
            string qtdstr = "";
            decimal qtd = 0;
            decimal valor = 0;
            string valorstr = "";
            string nomeproduto = "";

            object loque = new object();

            try
            {
                if (VerificaReducaoZ())
                {
                    throw new Exception("Não é permitido emitir cupom fiscal após a Redução Z");
                }

                IRetorno = cupomFiscal.Bematech_FI_ProgramaArredondamento();
                cupomFiscal.Analisa_iRetorno(IRetorno);

                IRetorno = cupomFiscal.Bematech_FI_AbreCupom("");
                cupomFiscal.Analisa_iRetorno(IRetorno);

                lock (loque)
                {
                    foreach (Modelo.NotaItem item in objNota.NotaItems)
                    {
                        qtd = System.Math.Round(Convert.ToDecimal(item.Quantidade), 3);
                        qtdstr = String.Format("{0:0.000}", Convert.ToDecimal(qtd)).Replace(".", ",");

                        valor = Decimal.Round(item.Valor, 3);
                        valorstr = String.Format("{0:0.000}", Convert.ToDecimal(valor)).Replace(".", ",");

                        if (!String.IsNullOrEmpty(item.ProdutoDescReduzida))
                            nomeproduto = MontaNomeProduto(item.ProdutoDescReduzida);
                        else
                            nomeproduto = MontaNomeProduto(item.ProdutoNome);

                        IRetorno = cupomFiscal.Bematech_FI_VendeItem(item.Produto.Codigo.ToString(), nomeproduto, item.AliqCupom, "F", qtdstr, 3, valorstr, "$", "0,00");
                        cupomFiscal.Analisa_iRetorno(IRetorno);
                    }
                }

                if (objNota.ValorDesconto > 0)
                {
                    qtd = System.Math.Round(objNota.ValorDesconto, 2);
                    qtdstr = String.Format("{0:0.00}", Convert.ToDecimal(qtd)).Replace(".", ",");

                    IRetorno = cupomFiscal.Bematech_FI_IniciaFechamentoCupom("D", "$", qtdstr);
                }
                else
                {
                    IRetorno = cupomFiscal.Bematech_FI_IniciaFechamentoCupom("A", "%", "0");
                }
                cupomFiscal.Analisa_iRetorno(IRetorno);

                foreach (var forma in formaPagamento)
                {
                    string valorPago = forma.Valor.ToString("n");
                    IRetorno = cupomFiscal.Bematech_FI_EfetuaFormaPagamento(forma.FormaPagamento.Nome, valorPago);
                    cupomFiscal.Analisa_iRetorno(IRetorno);
                }

                string aux = "CLIENTE:" + String.Format("{0,-40}", objNota.Pessoa.Nome) + "CNPJ/CPF:" + (objNota.PessoaCNPJCPF == null ? "" : objNota.PessoaCNPJCPF.TrimEnd());

                IRetorno = cupomFiscal.Bematech_FI_TerminaFechamentoCupom(aux);
                cupomFiscal.Analisa_iRetorno(IRetorno);

                Modelo.Nota nota = new Modelo.Nota();
                NotaController nc = NotaController.Instancia;

                nota = nc.LoadObjectById(objNota.ID);

                string num_cupom = "      ";
                IRetorno = cupomFiscal.Bematech_FI_NumeroCupom(ref num_cupom);
                cupomFiscal.Analisa_iRetorno(IRetorno);

                nota.Numero = Convert.ToInt32(num_cupom);
                nota.BImpressa = true;
                nota.ModeloDocto = 33;
                nota.Status = "2";

                nc.Salvar(nota, Acao.Alterar);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Aviso", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand);
                return;
            }
        }

        private string MontaNomeProduto(string pnomeproduto)
        {
            string nome = "";
            if (pnomeproduto.Length > 29)
                nome = pnomeproduto.Substring(0, 29);
            else
                nome = pnomeproduto;

            return nome;
        }

        public void CancelaCupom()
        {
            try
            {
                //Tenta Cancelar a nota no sistema, caso consiga cancela no equipamento.
                NotaController.Instancia.CancelarCupomFiscal();
                int IRetorno;
                IRetorno = cupomFiscal.Bematech_FI_CancelaCupom();
                cupomFiscal.Analisa_iRetorno(IRetorno);
                //So commita as alterações no banco de dados caso consiga cancelar na máquina, se ocorrer erro desfaz as alterações.
                if (IRetorno == 1)
                {
                    NotaController.Instancia.CommitUnitOfWorkTransaction();
                }
                else
                {
                    NotaController.Instancia.RollBackUnitOfWorkTransaction();
                    throw new Exception("Houve um problema de comunicação com o equipamento Fiscal. Entre em contato com o suporte técnico!");
                }
            }
            catch (Exception)
            {
                NotaController.Instancia.RollBackUnitOfWorkTransaction();
                throw;
            }
            
        }

        public void LeituraX()
        {
            int IRetorno;

            IRetorno = cupomFiscal.Bematech_FI_LeituraX();
            cupomFiscal.Analisa_iRetorno(IRetorno);
        }

        public bool VerificaReducaoZ()
        {
            //int Flag = -1;
            int IRetorno = 0;

            //IRetorno = cupomFiscal.Bematech_FI_VerificaReducaoZAutomatica(ref Flag);
            //cupomFiscal.Analisa_iRetorno(IRetorno);

            //if (Flag == 1)
            //    return true;
            //else
            //    return false;
            string DataReducao = "      ";
            string HoraReducao = "      ";
            IRetorno = cupomFiscal.Bematech_FI_DataHoraReducao(ref DataReducao, ref HoraReducao);

            if (String.Format("{0:ddMMyy}", System.DateTime.Today) == DataReducao)
                return true;
            else
                return false;

        }

        public bool ImpressoraLigada()
        {
            if (cupomFiscal.Bematech_FI_VerificaImpressoraLigada() == 1)
                return true;
            else
                return false;
        }

        public void Bematech_FI_AberturaDoDia()
        {
            int IRetorno;
            IRetorno = cupomFiscal.Bematech_FI_AberturaDoDia("0,00", "");
            cupomFiscal.Analisa_iRetorno(IRetorno);
        }
        public void Bematech_FI_FechamentoDoDia()
        {
            int IRetorno;
            IRetorno = cupomFiscal.Bematech_FI_FechamentoDoDia();
            cupomFiscal.Analisa_iRetorno(IRetorno);
        }

        public void Bematech_FI_RelatorioSintegraMFD(int iRelatorios, string Arquivo, string Mes,
             string Ano, string RazaoSocial, string Endereco, string Numero, string Complemento,
             string Bairro, string Cidade, string CEP, string Telefone, string Fax, string Contato)
        {
            BematechHelper param = new BematechHelper ( RazaoSocial, Endereco, Numero, Complemento, Bairro, Cidade, CEP, Telefone, Fax, Contato );
            int IRetorno = cupomFiscal.Bematech_FI_RelatorioSintegraMFD(iRelatorios, Arquivo, Mes, Ano, 
                                param.razaoSocial, param.endereco, param.numero, param.complemento,
                                param.bairro, param.cidade, param.cep, param.telefone, param.fax, param.contato);
            //Analisa o Retorno
             cupomFiscal.Analisa_iRetorno(IRetorno);
        }

        public void ProgramaTruncamento()
        {
            int IRetorno;

            IRetorno = cupomFiscal.Bematech_FI_ProgramaTruncamento();
            cupomFiscal.Analisa_iRetorno(IRetorno);
        }
    }

    public class BematechHelper
    {
        private string _razaoSocial;
        private string _endereco;
        private string _numero;
        private string _complemento;
        private string _bairro;
        private string _cidade;
        private string _cep;
        private string _telefone;
        private string _fax;
        private string _contato;

        public string razaoSocial
        {
            get
            {
                if (_razaoSocial.Length > 35)
                   return _razaoSocial.Substring(0, 35);
                else
                    return _razaoSocial;
            }
            set
            {
                _razaoSocial = value;
            }
        }
        public string endereco {
            get
            {
                if (_endereco.Length > 34)
                    return _endereco.Substring(0, 34);
                else
                    return _endereco;
            }
            set
            {
                _endereco = value;
            }
        }
        public string numero {
            get
            {
                if (_numero.Length > 5)
                    return _numero.Substring(0, 5);
                else return _numero;
            }
            set
            {
                _numero = value;
            }
        }
        public string complemento {
            get
            {
                if (_complemento.Length > 22)
                    return _complemento.Substring(0, 22);
                else return _complemento;
            }
            set
            {
                _complemento = value;
            }
        }
        public string bairro {
            get
            {
                if (_bairro.Length > 15)
                    return _bairro.Substring(0, 15);
                else return _bairro;
            }
            set
            {
                _bairro = value;
            }
        }
        public string cidade {
            get
            {
                if (_cidade.Length > 30)
                    return _cidade.Substring(0, 30);
                else return _cidade;
            }
            set
            {
                _cidade = value;
            }
        }
        public string cep { get { return _cep.Replace("-", "").Replace(".", ""); } set { _cep = value; } }
        public string telefone { get { return _telefone.Replace("(", "").Replace(")", "").Replace("-", ""); } set { _telefone = value; } }
        public string fax { get { return _fax.Replace("(", "").Replace(")", "").Replace("-", ""); } set { _fax = value; } }
        public string contato { 
            get {
                    if (_contato == null)
                    {
                        if (_razaoSocial.Length > 18)
                        {
                            return _razaoSocial.Substring(0, 18);
                        }
                        else { return _razaoSocial; }
                    }
                    else if (_contato.Length > 18)
                    {
                        return _contato.Substring(0, 18);
                    }
                    else return _contato;
                }
            set
            {
                _contato = value;
            }
        }

        public BematechHelper(string RazaoSocial, string Endereco, string Numero, string Complemento,
             string Bairro, string Cidade, string CEP, string Telefone, string Fax, string Contato)
        {
            razaoSocial = RazaoSocial;
            endereco = Endereco;
            numero = Numero;
            complemento = Complemento;
            bairro = Bairro;
            cidade = Cidade;
            cep = CEP;
            telefone = Telefone;
            fax = Fax;
            contato = Contato;
        }
    }
}
