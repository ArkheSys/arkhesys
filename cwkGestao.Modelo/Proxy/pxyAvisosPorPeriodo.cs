using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyAvisosPorPeriodo
    {
        public pxyAvisosPorPeriodo(int numEventoPassado, int numEventoOntem, int numEventoHoje, int numEventoAmanha, int numEventoFututo,
            string horaInicialEventoAnterior, string horaInicialEventoProximo)
        {
            this.NumEventoPassado = numEventoPassado;
            this.NumEventoOntem = numEventoOntem;
            this.NumEventoHoje = numEventoHoje;
            this.NumEventoAmanha = numEventoAmanha;
            this.NumEventoFuturo = numEventoFututo;
            this.HoraInicialEventoAnterior = horaInicialEventoAnterior;
            this.HoraInicialEventoProximo = horaInicialEventoProximo;
        }

        public int NumEventoPassado { get; set; }
        public int NumEventoOntem { get; set; }
        public int NumEventoHoje { get; set; }
        public int NumEventoAmanha { get; set; }
        public int NumEventoFuturo { get; set; }
        public string HoraInicialEventoAnterior { get; set; }
        public string HoraInicialEventoProximo { get; set; }
    }
}