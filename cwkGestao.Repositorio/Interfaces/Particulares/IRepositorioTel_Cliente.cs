using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioTel_Cliente : IRepositorio<Tel_Cliente>
    {

        IList<Tel_Cliente> GetByGrupo(Tel_GrupoCliente grupoCliente);

        Tel_Cliente GetPessoaID(int ID);

        Tel_Cliente GetByTelefone(string telefone);

        IList<Tel_Cliente> GetAllByGrupo(Tel_GrupoCliente grupoCliente);
    }
}

