using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyRelOSEquipamento
    {
        public pxyRelOSEquipamento() { }

        public pxyRelOSEquipamento(int _id, int _codigo, string _descricao, string _numeroSerie, string _tipoEquipamento)
        {
            ID = _id;
            Codigo = _codigo;
            Descricao = _descricao;
            NumeroSerie = _numeroSerie;
            TipoEquipamento = _tipoEquipamento;
        }

        public int ID { get; set; }
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string NumeroSerie { get; set; }
        public string TipoEquipamento { get; set; }
    }
}
