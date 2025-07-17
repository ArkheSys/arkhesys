using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyGridOrdemServico
    {
        public pxyGridOrdemServico() { }
        public pxyGridOrdemServico(int _id, int _codigo, string _resumo, string _cliente, string _clienteRevenda, string _responsavel, DateTime? _dataCadastro, string _status, DateTime? _dataStatus, DateTime? _dataPrevisao, DateTime? _dataPrevisaoFinal, String _sistema, String _versao)
        {
            ID = _id;
            Codigo = _codigo;
            Resumo = _resumo;
            Cliente = _cliente;
            ClienteRevenda = _clienteRevenda;
            Responsavel = _responsavel;
            DataCadastro = _dataCadastro;
            Status = _status;
            DataStatus = _dataStatus;
            DataPrevisao = _dataPrevisao;
            DataprevisaoFinal = _dataPrevisaoFinal;
            Sistema = _sistema;
            Versao = _versao;
        }

        public int ID { get; set; }
        public int Codigo { get; set; }
        public string Resumo { get; set; }
        public string Cliente { get; set; }
        public string ClienteRevenda { get; set; }
        public string Responsavel { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string Status { get; set; }
        public DateTime? DataStatus { get; set; }
        public DateTime? DataPrevisao { get; set; }
        public DateTime? DataprevisaoFinal { get; set; }
        public string Sistema { get; set; }
        public string Versao { get; set; }
    }
}
