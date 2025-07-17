using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.PCP
{
    public class OrdemProducao
    {
        public static bool GerarOrdemProducao(Modelo.DataClassesDataContext db, Modelo.Pedido objPedido)
        {
            bool ret = false;
            var itens = objPedido.PedidoItems.Where(p => p.PedidoItems.Count > 0);
            if (itens.Count() > 0)
            {
                Modelo.DataClassesDataContext dc = new Modelo.DataClassesDataContext();
                Modelo.OrdemProducao objOrdemProducao;
                Modelo.OrdemProducaoExecucao objExecucao;
                Modelo.OrdemProducaoHistorico objHistorico;
                int seq = 0;
                foreach (Modelo.PedidoItem item in itens)
                {
                    objOrdemProducao = new Modelo.OrdemProducao();
                    objOrdemProducao.Codigo = objOrdemProducao.RetornaMAXCodigo(dc);
                    objOrdemProducao.IdPedidoItem = item.ID;
                    objOrdemProducao.IdCliente = objPedido.IDPessoa.Value;
                    objOrdemProducao.IdFilial = objPedido.IDFilial.Value;
                    var classif = dc.ClassificacaoOrdemProducaos.OrderBy(c => c.Codigo);
                    if (classif.Count() > 0)
                    {
                        objOrdemProducao.ClassificacaoOrdemProducao = classif.First();
                        objHistorico = new Modelo.OrdemProducaoHistorico();
                        objHistorico.ClassificacaoOrdemProducao = objOrdemProducao.ClassificacaoOrdemProducao;
                        objHistorico.Data = DateTime.Now;
                        objHistorico.Sequencia = 1;
                        objOrdemProducao.OrdemProducaoHistoricos.Add(objHistorico);
                    }
                    seq = 1;
                    foreach (Modelo.EtapaProducao etapa in item.Produto.EtapaProducaos)
                    {
                        objExecucao = new Modelo.OrdemProducaoExecucao();
                        objExecucao.IdServico = etapa.IDServico;
                        objExecucao.Quantidade = 1;
                        objExecucao.Sequencia = seq++;
                        objExecucao.Descricao = dc.Servicos.Where(s => s.id == objExecucao.IdServico).First().nome;
                        objOrdemProducao.OrdemProducaoExecucaos.Add(objExecucao);
                    }

                    objOrdemProducao.IncData = DateTime.Now;
                    dc.OrdemProducaos.InsertOnSubmit(objOrdemProducao);
                }
                dc.SubmitChanges();
                ret = true;
            }
            return ret;
        }
    }
}
