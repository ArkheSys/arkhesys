using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioAcrescimo : IRepositorio<Acrescimo>
    {
        decimal CalculaJuros(Acrescimo acrescimo, decimal valor, DateTime dataBase, DateTime dataVencimento,
                             decimal pctMora);
        decimal CalculaMulta(Acrescimo acrescimo, decimal valor, DateTime dataBase, DateTime dataVencimento, decimal valorjuro, decimal rpporcmulta, int rpdiasmulta);
    }
}

