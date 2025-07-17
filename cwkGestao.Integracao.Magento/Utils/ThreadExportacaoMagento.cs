using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using cwkGestao.Integracao.Magento.Exportacao;
using cwkGestao.Modelo;
using Aplicacao.Modulos.Utilitarios;
using Aplicacao.Util;

namespace cwkGestao.Integracao.Magento.Utils
{
    public class ThreadExportacaoMagento
    {
        public static void ExportarEntidade(ModeloBase entidade)
        {
            Thread t = new Thread(new ThreadStart(() =>
            {
                try
                {
                    MegaExportador.ExportarUnicoObjeto(entidade);
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }));

            t.Start();
        }
    }
}
