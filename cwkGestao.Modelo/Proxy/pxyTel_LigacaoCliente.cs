using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyTel_LigacaoCliente
    {
        public pxyTel_LigacaoCliente() { }

        public pxyTel_LigacaoCliente(int idCliente, int idLigacao, int tempoSegundos, string origem, int tipoUso, string destino)
        {
            IDCliente = idCliente;
            IDLigacao = idLigacao;
            TempoSegundos = tempoSegundos;
            Origem = origem;
            TipoUso = tipoUso;
            Destino = destino;
          
        }

        public virtual int IDCliente { get; set; }
        public virtual int IDLigacao { get; set; }
        public virtual int TempoSegundos { get; set; }
        public virtual string Origem { get; set; }
        public virtual int TipoUso { get; set; }
        public virtual string Destino { get; set; }
        
    }
}
