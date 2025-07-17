using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Regra de Imposto")]
	public class Produto_Icms : IcmsBase
	{        
        public virtual Produto Produto { get; set; }


        public static Produto_Icms CloneNewObject(Produto_Icms objProduto_Icms)
        {
            AutoMapper.Mapper.CreateMap<Produto_Icms, Produto_Icms>()
                    .ForMember(dest => dest.ID, o => o.Ignore());

            return AutoMapper.Mapper.Map<Produto_Icms, Produto_Icms>(objProduto_Icms);
        }

        public static void GetClones(ref Produto objProduto)
        {
            IList<Produto_Icms> lstProduto_Icms = new List<Produto_Icms>(objProduto.Produto_Icmss);
            objProduto.Produto_Icmss = new List<Produto_Icms>();
            foreach (var Produto_Icms in lstProduto_Icms)
            {
                Produto_Icms Item = Produto_Icms.CloneNewObject(Produto_Icms);
                Item.Produto = objProduto;
                objProduto.Produto_Icmss.Add(Item);
            }
        }
	}
}
		