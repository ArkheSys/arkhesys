using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Perfil Tributário do Cliente")]
    public class PerfilTributarioCliente : ModeloBase
    {
        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get; set; }
        
        [InformacaoExibicao(2, "Descrição", 100)]
        public virtual string Descricao { get; set; }

        public virtual bool ConsumidorFinal { get; set; }
        public virtual int PerfilTributario { get; set; }

    }
}
