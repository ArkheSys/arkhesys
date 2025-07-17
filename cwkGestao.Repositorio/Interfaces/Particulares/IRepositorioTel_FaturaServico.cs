using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioTel_FaturaServico : IRepositorio<Tel_FaturaServico>
    {
        IList<cwkGestao.Modelo.Tel_FaturaServico> GetServicos(int IDFatura);

        IList<Tel_FaturaServico> GetByGrupoEReferencia(Tel_GrupoCliente grupoCliente, DateTime referencia);

        IList<Tel_FaturaServico> GetByFaturas(IList<int> idsFaturas);
    }
}

