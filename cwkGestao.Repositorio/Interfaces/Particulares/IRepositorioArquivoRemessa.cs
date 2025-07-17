using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioArquivoRemessa : IRepositorio<ArquivoRemessa>
    {
        IList<ArquivoRemessa> GetArquivoRemessaPorData(DateTime dataInicio, DateTime dataFim, int filtro, int IDEmpresa, int IDRemessa);       
    }
}
