using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public partial class Rateio_NotaItem
    {
        /// <summary>
        /// Método responsável por retornar uma lista com todos os rateios de um determinado item de nota
        /// </summary>
        /// <param name="db">contexto</param>
        /// <param name="idMovimento">id do Item da Nota</param>
        /// <returns>lista com os rateios</returns>
        /// WNO - 08/06/2010
        public IQueryable GetRateiosMovimento(DataClassesDataContext db, int idNotaItem)
        {
            IQueryable lista;
            lista = from rateio_NotaItem in db.Rateio_NotaItems
                    where rateio_NotaItem.IDNotaItem == IDNotaItem
                    select new
                    {
                        Projeto = rateio_NotaItem.Projeto.Nome,
                        Valor = rateio_NotaItem.Valor,
                        ID = rateio_NotaItem.ID
                    };
            return lista;
        }

        public Int32 RetornaMAXSequencia(Modelo.NotaItem notaItem)
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (int)(from c in notaItem.Rateio_NotaItems
                                  where c.Sequencia == notaItem.Rateio_NotaItems.Max(b => b.Sequencia)
                                  select c).Single().Sequencia;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }
    }
}
