using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BLL
{
    public class Remessa : IDisposable
    {
        Modelo.DataClassesDataContext db;

        public Remessa(Modelo.DataClassesDataContext pdb)
        {
            db = pdb;
        }

        public Remessa()
        {
            db = new Modelo.DataClassesDataContext();
        }

        public IQueryable getLista()
        {
            IQueryable lista;
            lista = from r in db.Remessas
                    select new
                    {
                        ID = r.ID,
                        Codigo = r.Codigo,
                        Descricao = r.Descricao,
                        ContaCorrente = r.ContaCorrente,
                        CodigoCedente = r.CodigoCedente,
                        CodigoAgencia = r.CodigoAgencia
                    };
            return lista;
        }

        public IQueryable getLista(string pFiltro)
        {
            throw new NotImplementedException();
        }

        public IQueryable getListaCombo()
        {
            throw new NotImplementedException();
        }

        public IQueryable getListaCombo(string pFiltro)
        {
            throw new NotImplementedException();
        }

        public object getObjeto(int pID)
        {
            return (from u in db.Remessas where u.ID == pID select u).Single();
        }

        public int getID(string pValor)
        {
            if (pValor.Length != 0)
            {
                return (from u in db.Pessoas where u.Codigo.ToString() == pValor select u).Single().ID;
            }
            else
            {
                return 0;
            }
        }

        public int? RetornaMAXCodigo()
        {
            int? maxCodigo = 0;
            try
            {
                maxCodigo = (from p in db.Remessas
                             where p.Codigo == db.Remessas.Max(b => b.Codigo)
                             select p).Single().Codigo;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }

        #region IDisposable Members
        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.Remessa ObjRemessa)
        {
            Dictionary<string, string> ret = ValidaObjeto(ObjRemessa);
            if (ret.Count == 0)
            {
                db.SubmitChanges();
            }
            return ret;
        }

        public static Dictionary<string, string> ValidaObjeto(Modelo.Remessa ObjRemessa)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (String.IsNullOrEmpty(ObjRemessa.Descricao) || ObjRemessa.Descricao.Trim() == String.Empty)
            {
                ret.Add("txtDescricao", "Preencha a Descrição da Remessa.");
            }

            if (String.IsNullOrEmpty(ObjRemessa.CodigoAgencia) || ObjRemessa.CodigoAgencia.Trim() == String.Empty)
            {
                ret.Add("txtCodAgencia", "Preencha o código da agencia.");
            }

            if (String.IsNullOrEmpty(ObjRemessa.ContaCorrente) || ObjRemessa.ContaCorrente.Trim() == String.Empty)
            {
                ret.Add("txtContaCorrente", "Preencha o código da conta corrente.");
            }

            if (String.IsNullOrEmpty(ObjRemessa.CodigoCedente) || ObjRemessa.CodigoCedente.Trim() == String.Empty)
            {
                ret.Add("txtCodigoCedente", "Preencha o código do cedente.");
            }

            if (String.IsNullOrEmpty(ObjRemessa.CaminhoLayoutBoleto) || ObjRemessa.CaminhoLayoutBoleto.Trim() == String.Empty)
            {
                ret.Add("txtArqModelo", "Escolha um arquivo que contenha o modelo do boleto.");
            }

            if (String.IsNullOrEmpty(ObjRemessa.ArquivoDeLicensa) || ObjRemessa.ArquivoDeLicensa.Trim() == String.Empty)
            {
                ret.Add("txtArqLicenca", "Escolha um arquivo que contenha a licença.");
            }

            if (ObjRemessa.bGeraRemessa == true && (String.IsNullOrEmpty(ObjRemessa.CaminhoRemessa) || ObjRemessa.CaminhoRemessa.Trim() == String.Empty))
                ret.Add("txtCaminhoRemessa", "Escolha o caminho da remessa.");

            if (ObjRemessa.bGeraEmail == true && (String.IsNullOrEmpty(ObjRemessa.LayoutBoletoEmail) || ObjRemessa.LayoutBoletoEmail.Trim() == String.Empty))
                ret.Add("txtModeloBoletoEmail", "Escolha o modelo boleto email.");

            if (ObjRemessa.bGeraEmail == true)
            {
                if (!String.IsNullOrEmpty(ObjRemessa.AssuntoEmail) && ObjRemessa.AssuntoEmail.Trim() == String.Empty)
                {
                    ret.Add("txtAssuntoEmail", "Um email não pode ser enviado sem assunto.");
                }
                if (!String.IsNullOrEmpty(ObjRemessa.NomeDoEmail) && ObjRemessa.NomeDoEmail.Trim() == String.Empty)
                {
                    ret.Add("txtNomeEmail", "Coloque o nome de quem esta enviando este email.");
                }
                if (!String.IsNullOrEmpty(ObjRemessa.URLLogotipo) && ObjRemessa.URLLogotipo.Trim() == String.Empty)
                {
                    ret.Add("txtURLLogotipo", "Escolha a URL dos logotipos.");
                }
                if (!String.IsNullOrEmpty(ObjRemessa.URLCodigodeBarras) && ObjRemessa.URLCodigodeBarras.Trim() == String.Empty)
                {
                    ret.Add("txtURLCodigoBarras", "Escolha a URL do codigo de barra.");
                }
            }
            if (ObjRemessa.bGeraPDF == true && String.IsNullOrEmpty(ObjRemessa.ImpressoraNome))
            {
                ret.Add("cbPrinter", "Escolha a imprssora de PDF's que deseja utilisar.");
            }
            if (ObjRemessa.Banco == null)
            {
                ret.Add("cbBanco", "Escolha o Banco o qual deseja criar uma remessa ou boleto");
            }
            if (ObjRemessa.NumDiasBaixaDevolucao < Convert.ToInt32(ObjRemessa.NumeroDiasProtesto == "" ? int.MaxValue.ToString() : ObjRemessa.NumeroDiasProtesto))
            {
                ret.Add("txtNumDiasProtesto", "A quantidade de dias para protesto não pode ser menor que os dias para a devolução");
            }
            if (ObjRemessa.Banco == 1 && String.IsNullOrEmpty(ObjRemessa.VariacaoCarteira))
            {
                ret.Add("txtVariacaoCarteira", "Por favor coloque a variação da carteira.");
            }
            if (ObjRemessa.Banco == 2 && ObjRemessa.SicrediTipoCobranca != 1 && ObjRemessa.SicrediTipoCobranca != 3)
            {
                ret.Add("cbTipoCobrancaSicredi", "Por favor escolha um tipo de cobrança.");
            }
            if (ObjRemessa.Banco == 2 && ObjRemessa.Sequencia < 1999) 
            {
                ret.Add("txtSequencia", "O numero da sequencia do Banco Sicredi deve ser maior do que 1999");
            }
            return ret;
        }

        public int GetCodigoPorID(int pID)
        {
            int? codigo;
            codigo = (from c in db.Remessas
                      where c.ID == pID
                      select c.Codigo).First();

            return Convert.ToInt32(codigo);
        }
    }
}
