using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class LogAlteracaoPedidoItem : ModeloBase
	{
        public override int ID { get; set; }
		public virtual string Descricao { get; set; }
		public virtual string DescricaoAnterior { get; set; }
		public virtual string Codigo { get; set; }
		public virtual string CodigoAnterior { get; set; }
        public virtual DateTime DataInicialDesc { get; set; }
        public virtual DateTime DataFinalDesc { get; set; }    
		public virtual Produto Produto { get; set; }
		public virtual Servico Servico { get; set; }


        public static LogAlteracaoPedidoItem CloneNewObject(LogAlteracaoPedidoItem objLogAlteracaoPedidoItem)
        {
            AutoMapper.Mapper.CreateMap<LogAlteracaoPedidoItem, LogAlteracaoPedidoItem>()
                    .ForMember(dest => dest.ID, o => o.Ignore());

            return AutoMapper.Mapper.Map<LogAlteracaoPedidoItem, LogAlteracaoPedidoItem>(objLogAlteracaoPedidoItem);
        }

        public static void GetClones(ref Produto objProduto)
        {
            IList<LogAlteracaoPedidoItem> lstLogAlteracaoPedidoItem = new List<LogAlteracaoPedidoItem>(objProduto.LogAlteracaoPedidoItens);
            objProduto.LogAlteracaoPedidoItens = new List<LogAlteracaoPedidoItem>();
            foreach (var LogAlteracaoPedidoItem in lstLogAlteracaoPedidoItem)
            {
                LogAlteracaoPedidoItem Item = LogAlteracaoPedidoItem.CloneNewObject(LogAlteracaoPedidoItem);
                Item.Produto = objProduto;
                objProduto.LogAlteracaoPedidoItens.Add(Item);
            }
        }


    }
}
