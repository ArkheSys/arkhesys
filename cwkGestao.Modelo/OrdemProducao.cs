using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class OrdemProducao : ModeloBase
    {
        public override int ID { get; set; }
        public virtual int Codigo { get; set; }
        public virtual ClassificacaoOrdemProducao ClassificacaoOrdemProducao { get; set; }
        public virtual Pessoa Cliente { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Projeto Projeto { get; set; }
        public virtual PedidoItem PedidoItem { get; set; }
#pragma warning disable CS0114 // "OrdemProducao.IncData" oculta o membro herdado "ModeloBase.IncData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public virtual DateTime IncData { get; set; }
#pragma warning restore CS0114 // "OrdemProducao.IncData" oculta o membro herdado "ModeloBase.IncData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public virtual IList<OrdemProducaoExecucao> OrdemProducaoExecucaos { get; set; }

        public virtual IList<OrdemProducaoHistorico> OrdemProducaoHistoricos { get; set; }

    }
}
