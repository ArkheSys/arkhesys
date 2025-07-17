using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioAtendimento : IRepositorio<Atendimento>
    {
        IList<Atendimento> GetAllByAnalistaPeriodoEFaturados(Cw_usuario usuario, DateTime start, DateTime end, bool ConsiderarFaturados);

        IList<Atendimento> GetAllPorIDNota(int IDNota);
    }
}

