using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ClassGeraProdutos
    {
        private Modelo.DataClassesDataContext db;
        BLL.Produto BLLProduto;

        public ClassGeraProdutos()
        {
            db = new Modelo.DataClassesDataContext();
            BLLProduto = new BLL.Produto(db);
        }

        public bool incluirProdutos(int qtd)
        {
            bool ret = true;
            try
            {
                for (int i = 0; i < qtd; i++)
                {
                    Modelo.Produto objProduto = new Modelo.Produto();

                    db.Produtos.InsertOnSubmit(objProduto);

                    objProduto.Codigo = BLLProduto.RetornaMAXCodigoStr();
                    objProduto.Nome = "PRODUTO " + GetPassword();
                    objProduto.Barra = objProduto.Codigo.ToString() + "" + RandomNumber(10, 9999).ToString();
                    objProduto.DescReduzida = objProduto.Nome;
                    objProduto.CustoFrete = 5;
                    objProduto.CustoIPI = 5;
                    objProduto.CustoExtra = 0;
                    objProduto.DescontoFornecedor = 0;
                    objProduto.IDFornecedor = 49;
                    objProduto.IDGrupoEstoque = 6;
                    objProduto.IDPlanoNegocio = 1;
                    objProduto.IDUnidade = 1;
                    objProduto.PrecoFornecedor = RandomNumber(10, 400);
                    //objProduto.IPI = 0;
                    objProduto.PesoLiquido = 0;
                    objProduto.PesoBruto = 0;
                    objProduto.UltimoCusto = 0;
                    objProduto.PrecoBase = BLLProduto.getPrecoBase(objProduto);

                    var preco = from tp in db.TabelaPrecos
                                select tp;

                    foreach (Modelo.TabelaPreco objTabelaPreco in preco)
                    {
                        Modelo.TabelaPrecoProduto objTPP = new Modelo.TabelaPrecoProduto();
                        objTPP.Codigo = objTPP.RetornaMAXCodigo(db, objProduto);
                        objTPP.MargemLucro = 80;
                        objTPP.PAcrescimo = 0;
                        objTPP.PDesconto = 0;
                        objTPP.TabelaPreco = objTabelaPreco;

                        objProduto.TabelaPrecoProdutos.Add(objTPP);
                    }

                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                ret = false;
                throw new Exception(ex.Message);
            }

            return ret;
        }

        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        private string GetPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, false));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
