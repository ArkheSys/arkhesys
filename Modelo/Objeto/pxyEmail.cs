using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class pxyEmail
    {
        public int ID { get; set; }
        public int IDPessoa { get; set; }
        public string Descricao { get; set; }
        public byte? bFinanceiro { get; set; }
        public byte? bAdministrativo { get; set; }
        public byte? bContato { get; set; }
        public byte? bMsn { get; set; }
        public byte? bSugestao { get; set; }
        public byte? bVenda { get; set; }
        public byte? bCompra { get; set; }
        public bool BNFe { get; set; }
            
    }
}
