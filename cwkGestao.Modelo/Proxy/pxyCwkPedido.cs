using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyCwkPedido
    {
        public pxyCwkPedido() { }

        public pxyCwkPedido(int _id, int _codigo, DateTime? _data, DateTime? _hora, string _revenda, string _cliente, int? _numeroSerie, string _sistema, bool _BDemonstracao, OrcamentoStatus _status, string _numeroSerieEmpresas)
        {
            ID = _id;
            Codigo = _codigo;
            Data = _data;
            Hora = _hora;
            Revenda = _revenda;
            Cliente = _cliente;
            NumeroSerie = _numeroSerie;
            Sistema = _sistema;
            BDemonstracao = _BDemonstracao ? "Sim" : "";
            Status = _status;
            NumeroSerieEmpresas = _numeroSerieEmpresas;
        }

        public pxyCwkPedido(int _id, int _codigo, DateTime? _data, DateTime? _hora, string _revenda, string _cliente, int? _numeroSerie, string _sistema, bool _BDemonstracao, bool _Baixar)
        {
            ID = _id;
            Codigo = _codigo;
            Data = _data;
            Hora = _hora;
            Revenda = _revenda;
            Cliente = _cliente;
            NumeroSerie = _numeroSerie;
            Sistema = _sistema;
            BDemonstracao = _BDemonstracao ? "Sim" : "";
            

        }

        public pxyCwkPedido(int _id, string _sistema)
        {
            Sistema = _sistema;
            ID = _id;
            
        }

        public int ID { get; set; }
        public int Codigo { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? Hora { get; set; }
        public string Revenda { get; set; }
        public string Cliente { get; set; }
        public int? NumeroSerie { get; set; }
        public string Sistema { get; set; }
        public string BDemonstracao { get; set; }
        public OrcamentoStatus Status { get; set; }
        public string NumeroSerieEmpresas { get; set; }
    }
}
