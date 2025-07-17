using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioCw_acesso : IRepositorio<Cw_acesso>
    {
        Cw_acesso LoadAcessoByGrupoTextoForm(int idGrupo, String textoFormulario);
    }
}

