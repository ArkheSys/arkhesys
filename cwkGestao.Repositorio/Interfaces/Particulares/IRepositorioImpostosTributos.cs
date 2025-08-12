using cwkGestao.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioImpostosTributos : IRepositorio<ImpostosTributos>
    {
        ImpostosTributos GetByClassificacaoFiscal(int pIDClassificacaoFiscal, int pIDPerfilTributarioCliente);
    }
}
