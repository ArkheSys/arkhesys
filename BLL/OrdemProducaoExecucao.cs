using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class OrdemProducaoExecucao
    {
        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.OrdemProducaoExecucao objOrdemProducaoExecucao)
        {
            Dictionary<string, string> ret = ValidaObjeto(objOrdemProducaoExecucao);
            if (ret.Count == 0)
            {
                db.SubmitChanges();
            }
            return ret;
        }

        public static Dictionary<string, string> ValidaObjeto(Modelo.OrdemProducaoExecucao objOrdemProducaoExecucao)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (String.IsNullOrEmpty(objOrdemProducaoExecucao.Descricao.Trim()))
                ret.Add("txtDescricao", "Campo Obrigatório.");
#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'int' nunca é igual a "null" do tipo 'int?'
            if (objOrdemProducaoExecucao.IdServico == 0 || objOrdemProducaoExecucao.IdServico == null)
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'int' nunca é igual a "null" do tipo 'int?'
                ret.Add("lkServico", "Campo Obrigatório.");
            if (objOrdemProducaoExecucao.Quantidade <= 0 || objOrdemProducaoExecucao.Quantidade == null)
                ret.Add("txtQuantidade", "Campo Obrigatório.");
#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'int' nunca é igual a "null" do tipo 'int?'
            if (objOrdemProducaoExecucao.Sequencia <= 0 || objOrdemProducaoExecucao.Sequencia == null)
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'int' nunca é igual a "null" do tipo 'int?'
                ret.Add("txtSequencia", "Campo Obrigatório.");
            if (objOrdemProducaoExecucao.DataInicio.HasValue && objOrdemProducaoExecucao.DataTermino.HasValue)
            {
                if (objOrdemProducaoExecucao.DataTermino.Value < objOrdemProducaoExecucao.DataInicio.Value)
                    ret.Add("txtDataTermino", "A data de término não pode ser menor do que a data de início.");
            }
            else if (objOrdemProducaoExecucao.DataTermino.HasValue && !objOrdemProducaoExecucao.DataInicio.HasValue)
                ret.Add("txtDataInicio", "Campo Obrigatório.");

            return ret;
        }
    }
}
