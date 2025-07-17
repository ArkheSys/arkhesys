using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyOSOrdemServico
    {
        public pxyOSOrdemServico() { }

        public pxyOSOrdemServico(string _NomeEmpresa, string _EndEmpresa, string _NumEmpresa, string _BairroEmpresa,
            string _CidadeEmpresa, string _UFEmpresa, string _CEPEmpresa, string _TelEmpresa, string _EmailEmpresa,
            int _CodigoOS, string _ClienteOS, string _ContatoOS, string _EndOS, string _CidadeOS, string _EmailOS, string _CPF_CNPJOS,
            string _TelOS, string _IEOS, string _CEPOS, string _UFOS, string _EquipamentoOS, string _ServicoSolicitadoOS, string _AcessorioOS,
            string _ComplementoOS, string _NumEndOS, int _CodOS, DateTime _DataOS)
        {
            NomeEmpresa = _NomeEmpresa;
            EndEmpresa = _EndEmpresa;
            NumEmpresa = _NumEmpresa;
            BairroEmpresa = _BairroEmpresa;
            CidadeEmpresa = _CidadeEmpresa;
            UFEmpresa = _UFEmpresa;
            CEPEmpresa = _CEPEmpresa;
            TelEmpresa = _TelEmpresa;
            EmailEmpresa = _EmailEmpresa;

            CodigoOS = _CodigoOS;
            ClienteOS = _ClienteOS;
            ContatoOS = _ContatoOS;
            EndOS = _EndOS;
            CidadeOS = _CidadeOS;
            EmailOS = _EmailOS;
            CPF_CNPJOS = _CPF_CNPJOS;
            TelOS = _TelOS;
            IEOS = _IEOS;
            CEPOS = _CEPOS;
            UFOS = _UFOS;
            EquipamentoOS = _EquipamentoOS;
            ServicoSolicitadoOS = _ServicoSolicitadoOS;
            AcessorioOS = _AcessorioOS;
            ComplementoOS = _ComplementoOS;
            NumEndOS = _NumEndOS;
            CodOS = _CodOS;
            DataOS = _DataOS;

        }

        //Empresa
        public string NomeEmpresa { get; set; }
        public string EndEmpresa { get; set; }
        public string NumEmpresa { get; set; }
        public string BairroEmpresa { get; set; }
        public string CidadeEmpresa { get; set; }
        public string UFEmpresa { get; set; }
        public string CEPEmpresa { get; set; }
        public string TelEmpresa { get; set; }
        public string EmailEmpresa { get; set; }
        
        //Cliente + OS
        public int CodigoOS { get; set; }
        public string ClienteOS { get; set; }
        public string ContatoOS { get; set; }
        public string EndOS { get; set; }
        public string CidadeOS { get; set; }
        public string EmailOS { get; set; }
        public string CPF_CNPJOS { get; set; }
        public string TelOS { get; set; }
        public string IEOS { get; set; }
        public string CEPOS { get; set; }
        public string UFOS { get; set; }
        public string EquipamentoOS { get; set; }
        public string ServicoSolicitadoOS { get; set; }
        public string AcessorioOS { get; set; }
        public string ComplementoOS { get; set; }
        public string NumEndOS { get; set; }
        public int CodOS { get; set; }
        public DateTime DataOS { get; set; }
    }
}
