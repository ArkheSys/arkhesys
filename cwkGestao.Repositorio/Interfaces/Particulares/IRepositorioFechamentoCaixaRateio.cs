using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioFechamentoCaixa : IRepositorio<FechamentoCaixa>
    {
        bool HqlPodeFecharCaixa(DateTime data, Banco banco);
        bool HqlPodeAbrirCaixa(DateTime data, Banco banco);
        FechamentoCaixa HqlGetFechamentoPorData(DateTime data, Banco banco);
        IList<FechamentoCaixa> GetBancoPeriodo(Banco banco, DateTime dtInicio, DateTime dtFinal);
        bool HqlExisteFechamentoPosterior(DateTime data, Banco banco);
    }
}

