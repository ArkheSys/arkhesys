using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyRelOSOrdemServico
    {
        public pxyRelOSOrdemServico() { }

        //public pxyRelOSOrdemServico(int codigo, DateTime data, int status, string cliente, string equipamento, string servicosolicitado)
        //{
        //    Codigo = codigo;
        //    Data = data;
        //    Status = OSOrdemServico.StatusToString((OSOrdemServico.StatusOS)status);
        //    Cliente = cliente;
        //    Equipamento = equipamento;
        //    ServicoSolicitado = servicosolicitado;
            
        //}

        public pxyRelOSOrdemServico(int codigo, DateTime data, string status, string cliente, string equipamento, string servicosolicitado)
        {
            Codigo = codigo;
            Data = data;
            Status = status;
            Cliente = cliente;
            Equipamento = equipamento;
            ServicoSolicitado = servicosolicitado;

        }

        public int Codigo { get; set; }
        public string Status { get; set; }
        public string Cliente { get; set; }
        public string Equipamento { get; set; }
        public string ServicoSolicitado { get; set; }
        public DateTime Data { get; set; }
        
    }
    
}
