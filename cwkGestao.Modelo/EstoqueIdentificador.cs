using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class EstoqueIdentificador : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Estoque Estoque { get; set; }
		public virtual Identificador Identificador { get; set; }
        public virtual decimal CustoUnitario { get; set; }
		public virtual int Quantidade { get; set; }
        public virtual int QuantidadeBaixada { get; set; }
#pragma warning disable CS0114 // "EstoqueIdentificador.IncData" oculta o membro herdado "ModeloBase.IncData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
		public virtual DateTime IncData { get; set; }
#pragma warning restore CS0114 // "EstoqueIdentificador.IncData" oculta o membro herdado "ModeloBase.IncData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
#pragma warning disable CS0114 // "EstoqueIdentificador.IncUsuario" oculta o membro herdado "ModeloBase.IncUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
		public virtual string IncUsuario { get; set; }
#pragma warning restore CS0114 // "EstoqueIdentificador.IncUsuario" oculta o membro herdado "ModeloBase.IncUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
#pragma warning disable CS0114 // "EstoqueIdentificador.AltData" oculta o membro herdado "ModeloBase.AltData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
		public virtual DateTime AltData { get; set; }
#pragma warning restore CS0114 // "EstoqueIdentificador.AltData" oculta o membro herdado "ModeloBase.AltData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
#pragma warning disable CS0114 // "EstoqueIdentificador.AltUsuario" oculta o membro herdado "ModeloBase.AltUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
		public virtual string AltUsuario { get; set; }
#pragma warning restore CS0114 // "EstoqueIdentificador.AltUsuario" oculta o membro herdado "ModeloBase.AltUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.

    }
}
