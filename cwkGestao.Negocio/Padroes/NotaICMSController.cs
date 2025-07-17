using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public partial class NotaICMSController : BaseController<NotaICMS>
	{
		#region Singleton
        private static NotaICMSController instance;
        private static IRepositorioNotaICMS repositorioNotaICMS;

        private NotaICMSController()
        { }
		
		static NotaICMSController()
        {
            instance = new NotaICMSController();
            repositorioT = RepositorioFactory<NotaICMS>.GetRepositorio();
            repositorioNotaICMS = (IRepositorioNotaICMS)repositorioT;
        }
			
		public static NotaICMSController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(NotaICMS objeto)
        {
            return new Dictionary<string,string>();
        }

        public void AtualizarICMS(Modelo.Nota nota)
        {
            var config = ConfiguracaoController.Instancia.GetConfiguracao();
            nota.NotaICMSs.Clear();
            if (nota.NotaItems.Count > 0)
            {
                foreach (NotaICMS notaIcms in (from n in nota.NotaItems
                                               group n by n.CFOP.ID
                                                   into g
                                                   select new Modelo.NotaICMS
                                                   {
                                                       CFOP = g.First().CFOP,
                                                       ICMS = null,
                                                       AliqICMS = g.First().AliqICMS,
                                                       AliqICMSNormal = g.First().AliqICMSNormal,
                                                       TextoIcms = g.First().TextoLei,
                                                       BaseICMS = g.Sum(a => a.BaseICMS),
                                                       ValorICMS = g.Sum(a => a.ValorICMS),
                                                       ValorIsentoICMS = g.Sum(a => a.ValorIsentoICMS),
                                                       ValorOutroICMS = g.Sum(a => a.ValorOutroICMS),
                                                       BaseSubst = g.Sum(a => a.BaseICMSSubst), // - (a.BaseICMSSubst * (a.PRedBCST_N20 / 100))),
                                                       ValorSubst = g.Sum(a => a.ValorRetidoICMS),
                                                       ValorIPI = g.Sum(a => a.VIPI_O14),
                                                       ValorCOFINS = g.Sum(a => a.VCOFINS_S11),
                                                       ValorPIS = g.Sum(a => a.VPIS_Q09),
                                                       TotalImpostos = g.Sum( a => a.TotalImpostos),
                                                       ValorTotalProdutos = g.Sum(a => a.Total + a.VIPI_O14 + a.ValorRetidoICMS + a.RAT_Acrescimo - a.RAT_Desconto),
                                                       vICMSDeson = g.Sum(a => a.ValorICMSDesoneracao)
                                                   }))
                {
                    if (config.NaoSomarIpiTotalNota == 1)
                        notaIcms.ValorTotalProdutos = notaIcms.ValorTotalProdutos - notaIcms.ValorIPI;
                    notaIcms.Nota = nota;
                    nota.NotaICMSs.Add(notaIcms);
                }
            }
        }
	}
}
