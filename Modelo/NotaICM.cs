using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Modelo
{
    public partial class NotaICM
    {
        /// <summary>
        /// Método para retornar a maior sequencia de uma determinada lista
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public Int32 RetornaMAXCodigo(Modelo.Nota pNota)
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (int)(from c in pNota.NotaICMs
                                  where c.Codigo == pNota.NotaICMs.Max(b => b.Codigo)
                                  select c).Single().Codigo;
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