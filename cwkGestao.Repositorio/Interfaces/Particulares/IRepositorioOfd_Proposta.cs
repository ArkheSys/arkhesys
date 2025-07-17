using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioOfd_Proposta : IRepositorio<Ofd_Proposta>
    {
        IList<Ofd_Proposta> GetAllByObra(Ofd_Obra obra);

        Ofd_Proposta LoadObjectByIdWithMedicoes(int id);

        Object GetMaxNumMedicao(int prop);

        int GetUltimoID_Ofd_Proposta();
    }
}

