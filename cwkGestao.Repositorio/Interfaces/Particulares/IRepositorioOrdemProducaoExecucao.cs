using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioOrdemProducaoExecucao : IRepositorio<OrdemProducaoExecucao>
    {

        IList<object> GetRelatorioApontamento(DateTime dataInicial, DateTime dataFinal, OrdemProducao op, Pessoa funcionario, Servico servico);

        IList<OrdemProducaoExecucao> GetApontamentos(PessoaFuncionario pessoaFuncionario);
    }
}

