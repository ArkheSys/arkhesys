using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using System.Collections;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioEquipamento : IRepositorio<OSEquipamento>
    {
        IList<object> GetListagemEquipamentos(TipoRelatorioEquipamento tipo);
    }
}

