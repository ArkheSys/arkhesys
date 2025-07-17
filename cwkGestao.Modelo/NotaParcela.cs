using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Nota Parcela")]
    public class NotaParcela : ParcelaBase
	{
        public virtual Nota Nota { get; set; }

        public virtual int? BandeiraCartao { get; set; }
        public virtual string Operadora { get; set; }
        public virtual string CAut { get; set; }

        public virtual decimal ValorTroco { get; set; }

        public static NotaParcela CloneNewObject(NotaParcela objNotaParcela)
        {
            AutoMapper.Mapper.CreateMap<Modelo.NotaParcela, Modelo.NotaParcela>().ForMember(dest => dest.ID, o => o.Ignore());
            return AutoMapper.Mapper.Map<NotaParcela, NotaParcela>(objNotaParcela);
        }

        public static void GetClones(ref Nota objNota)
        {
            IList<NotaParcela> lstNotaParcela = new List<NotaParcela>(objNota.NotaParcelas);
            objNota.NotaParcelas = new List<NotaParcela>();
            foreach (var notaParc in lstNotaParcela)
            {
                NotaParcela objNotaParcela = NotaParcela.CloneNewObject(notaParc);
                objNotaParcela.Nota = objNota;

                objNota.NotaParcelas.Add(objNotaParcela);
            }
        }
    }
}
		