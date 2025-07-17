using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class pxyRateio_Mov
    {
        public int Acao { get; set; }
        public int ID { get; set; }
        public int? IDMovimento { get; set; }
        public decimal Valor { get; set; }
        public int? Sequencia { get; set; }
        public Modelo.Projeto Projeto { get; set; }
        public string NomeProjeto { get; set; }

        public Int32 RetornaMAXSequencia(IList<Modelo.Objeto.pxyRateio_Mov> rateios)
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
