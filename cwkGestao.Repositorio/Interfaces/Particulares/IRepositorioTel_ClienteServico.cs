using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioTel_ClienteServico : IRepositorio<Tel_ClienteServico>
    {

        IList<Tel_ClienteServico> GetByGrupoDataLimiteEReferencia(Tel_GrupoCliente grupoCliente, DateTime dataLimite, DateTime referencia);

        void AtualizaValorServicoCliente(int IdTelServico, decimal ValorNovo);
    }
}

