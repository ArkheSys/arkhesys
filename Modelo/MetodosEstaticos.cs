using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Reflection;
using System.Data.Linq;

namespace Modelo
{
    public static class MetodosEstaticos
    {

        /// <summary>
        /// Método para validar um CNPJ
        /// </summary>
        /// <param name="cnpj">CNPJ a ser validado</param>
        /// <returns>caso o CNPJ seja válido, é retornado um valor verdadeiro. Caso contrário o retorno é falso</returns>
        public static bool ValidaCnpj(string cnpj)
        {

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma;

            int resto;

            string digito;

            string tempCnpj;

            cnpj = cnpj.Trim();

            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)

                return false;

            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;

            for (int i = 0; i < 12; i++)

                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;

            soma = 0;

            for (int i = 0; i < 13; i++)

                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);

        }

        /// <summary>
        /// Método que verifica se determinada string representa um CPF válido.
        /// </summary>
        /// <param name="cpf">String a ser verificada.</param>
        /// <returns>Caso seja um cpf válido, o retorno é verdadeiro, caso contrário é falso.</returns>
        public static bool ValidaCpf(string cpf)
        {

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf;

            string digito;

            int soma;

            int resto;

            cpf = cpf.Trim();

            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)

                return false;

            tempCpf = cpf.Substring(0, 9);

            soma = 0;

            for (int i = 0; i < 9; i++)

                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;

            for (int i = 0; i < 10; i++)

                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool ValidaData(Object data)
        {
            try
            {
                if (data == null)
                {
                    return false;
                }
                else
                {
                    DateTime aux = DateTime.Parse(data.ToString());
                    return true;
                }
            }
            catch (FormatException)
            {
                return false;
            }

        }

        public static bool ValidaInt(string numero)
        {
            foreach (char c in numero)
            {
                if (!(Char.IsNumber(c)))
                {
                    return false;
                }
            }
            if ((numero != ""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método para verificar se uma string é um número decimal.
        /// </summary>
        /// <param name="numero">string a ser validada.</param>
        /// <returns>Retorna verdadeiro caso o número seja válido e falso caso contrário.</returns>
        public static bool ValidaDecimal(string numero)
        {
            foreach (char c in numero)
            {
                if (!(Char.IsNumber(c)) && (c != ','))
                {
                    return false;
                }
            }
            if (numero != "")
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Método para validar o elemento selecionado de um ComboBox.
        /// </summary>
        /// <param name="ID">valor do elemento selecionado.</param>
        /// <returns>verdadeiro caso seja válido e falso caso contrário.</returns>
        public static bool ValidaCBox(Int32? ID)
        {
            if ((ID != null) && (ID != 0))
            {
                return true;
            }
            return false;
        }

        public static bool ValidaTel(string tel)
        {
            if (tel.Length != 13)
            {
                return false;
            }

            foreach (char c in tel)
            {
                if (Char.IsWhiteSpace(c))
                {
                    return false;
                }
            }
            return true;
        }

        public static string SqlExcecao(SqlException ex)
        {
            string msgEx = ex.Message;
            switch (ex.Number)
            {
                case 2627:
                    msgEx = "Registro duplicado. Verifique!!!\n\n" + ex.Message;
                    break;
                case 2601:
                    msgEx = "Registro duplicado. Verifique!!!\n\n" + ex.Message;
                    break;
                case 547:
                    msgEx = "Você não pode excluir este registro, porque existem\n outros registros que dependem dele.\n\n" + ex.Message;
                    break;
                case 64:
                    break;
                case 10054:
                    msgEx = "O banco de dados não está respondendo.\n\n" + ex.Message;
                    break;
                case 1205:
                    msgEx = "Não foi possível efetuar a operação, essa falha pode ter ocorrido porque existem outros usuários efetuando a mesma operação. Tente novamente.";
                    break;
            }
            return msgEx;
        }

        public static DateTime geraVencimento(DateTime pData, TipoDataType pTipo, int pValor)
        {
            DateTime data = new DateTime();

            if (pTipo == TipoDataType.Dia)
            {
                data = pData.AddDays(Double.Parse(pValor.ToString()));
            }
            else
            {
                data = pData.AddMonths(pValor);
            }

            return data;
        }

        /// <summary>
        /// Copia o valor das propriedades de um objeto para outro.
        /// </summary>
        /// <param name="objOrigem">Objeto origem.</param>
        /// <param name="objDestino">Objeto destino.</param>
        /// <returns>Objeto cópia.</returns>
        public static object CopiaPropriedades(object objOrigem, object objDestino)
        {
            if (objOrigem != null &&
                objDestino != null &&
                objOrigem.GetType().Equals(objDestino.GetType()))
            {
                // Tipo
                Type pTipo = objOrigem.GetType();

                // Nome de todas as propriedades.
                PropertyInfo[] pNome = pTipo.GetProperties();

                // Move todos os dados
                foreach (PropertyInfo propriedade in pNome)
                {
                    propriedade.SetValue(objDestino, propriedade.GetValue(objOrigem, null), null);
                }
            }
            else
            {
                objDestino = null;
            }
            return objDestino;
        }

        public static void DiscardInsertsAndDeletes(System.Data.Linq.DataContext db)
        {
            //Get the changes
            System.Data.Linq.ChangeSet changes = db.GetChangeSet();

            //Delete the insertions
            foreach (var item in changes.Inserts)
            {
                db.GetTable(item.GetType()).DeleteOnSubmit(item);
            }

            //Insert the deletions
            foreach (var item in changes.Deletes)
            {
                db.GetTable(item.GetType()).InsertOnSubmit(item);
            }
        }

        public static void DiscardUpdates(System.Data.Linq.DataContext db)
        {
            //get the changes
            var changes = db.GetChangeSet();

            //refresh the tables with updates
            List<ITable> updatedTables = new List<ITable>();
            foreach (var update in changes.Updates)
            {
                var tbl = db.GetTable(update.GetType());
                //make sure not to refresh the same table twice
                if (updatedTables.Contains(tbl))
                    continue;
                else
                {
                    updatedTables.Add(tbl);
                    db.Refresh(RefreshMode.OverwriteCurrentValues, tbl);
                }
            }
        }

        public static void DiscardAllChanges(System.Data.Linq.DataContext db)
        {
            DiscardInsertsAndDeletes(db);
            DiscardUpdates(db);
        }
    }
}
