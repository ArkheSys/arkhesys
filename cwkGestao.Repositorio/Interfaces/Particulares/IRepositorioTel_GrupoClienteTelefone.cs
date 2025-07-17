using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioTel_GrupoClienteTelefone : IRepositorio<Tel_GrupoClienteTelefone>
    {
        IList<Tel_GrupoClienteTelefone> GetDisponiveisPorGrupo(Tel_GrupoCliente grupoCliente, DateTime dataAtivacao, string telefone);
        bool ExisteTelefone(string telefone);
        Tel_GrupoClienteTelefone GetByTelefone(string telefone);
    }
}

