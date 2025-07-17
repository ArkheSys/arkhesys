using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio.Telefonia
{
    public class SelecionadorPrecos
    {
        private readonly int tipoUso;
        private readonly short area, prefixo, range;
        private readonly bool foraCidade;

        public SelecionadorPrecos(int _tipoUso, short _area, short _prefixo, short _range, short _areaDestino)
        {
            tipoUso = _tipoUso;
            area = _area;
            prefixo = _prefixo;
            range = _range;
            foraCidade = (Convert.ToInt32(_areaDestino) < 41 || Convert.ToInt32(_areaDestino) > 46);
        }

        public Tel_PrecoBase GetPreco(IList<Tel_PrecoBase> precos)
        {
            var result = precos.Where(p => p.TipoLigacao.Codigo == tipoUso && NumeroEstaNoRangeDoPreco(p)).ToList();
            if (foraCidade)
                result = result.Where(r => r.BFora).ToList();
            else
                result = result.Where(r => r.BDentro).ToList();

            return result.OrderBy(p => p.Ordem).FirstOrDefault();
        }
        private bool NumeroEstaNoRangeDoPreco(Tel_PrecoBase p)
        {
            return (area >=  p.Area_Inicial  && area <= p.Area_Final
                            && prefixo >= p.Prefixo_Inicial && prefixo <= p.Prefixo_Final
                            && range >= p.Range_Inicial && range <= p.Range_Final);
        }
    }
}
