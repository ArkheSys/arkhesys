using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public partial class Rateio_Mov
    {
        /// <summary>
        /// Método responsável por retornar uma lista com todos os rateios de um determinado movimento
        /// </summary>
        /// <param name="db">contexto</param>
        /// <param name="idMovimento">id do movimento</param>
        /// <returns>lista com os rateios</returns>
        /// WNO - 08/06/2010
        public IQueryable GetRateiosMovimento(DataClassesDataContext db, int idMovimento)
        {
            IQueryable lista;
            lista = from rateio_mov in db.Rateio_Movs
                    where rateio_mov.IDMovimento == idMovimento
                    select new
                    {
                        Projeto = rateio_mov.Projeto.Nome,
                        Valor = rateio_mov.Valor,
                        ID = rateio_mov.ID
                    };
            return lista;
        }

        public Int32 RetornaMAXSequencia(IList<Modelo.Rateio_Mov> rateios)
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (int)(from c in rateios
                                  where c.Sequencia == rateios.Max(b => b.Sequencia)
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
