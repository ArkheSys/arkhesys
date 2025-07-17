using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public interface IExpNotas
    {
        /// <summary>
        /// Método para gerar arquivo texto das notas
        /// 23/12/2009 - CRNC
        /// </summary>
        /// <param name="pArquivo">Nome do arquivo a ser gerado</param>
        /// <param name="pDataInicial">Data Inicial</param>
        /// <param name="pDataFinal">Data Final</param>
        /// <param name="pFilial">Filal a ser considerada</param>
        /// <param name="pEntSai">Tipo das notas a serem consideradas (Entrada, Saida, Ambas)</param>
        void GerarArquivo(string pArquivo, DateTime pDataInicial, DateTime pDataFinal, int pFilial, Modelo.EntSaiType pEntSai);
    }
}
