using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class EventosCorreio : ModeloBase
	{
        public override int ID { get; set; }
		public virtual int Codigo { get; set; }
		public virtual string Tipo { get; set; }
		public virtual int Status { get; set; }
		public virtual string Descricao { get; set; }
		public virtual string Detalhe { get; set; }
#pragma warning disable CS0114 // "EventosCorreio.IncData" oculta o membro herdado "ModeloBase.IncData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
		public virtual DateTime IncData { get; set; }
#pragma warning restore CS0114 // "EventosCorreio.IncData" oculta o membro herdado "ModeloBase.IncData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
#pragma warning disable CS0114 // "EventosCorreio.IncUsuario" oculta o membro herdado "ModeloBase.IncUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
		public virtual string IncUsuario { get; set; }
#pragma warning restore CS0114 // "EventosCorreio.IncUsuario" oculta o membro herdado "ModeloBase.IncUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
#pragma warning disable CS0114 // "EventosCorreio.AltData" oculta o membro herdado "ModeloBase.AltData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
		public virtual DateTime AltData { get; set; }
#pragma warning restore CS0114 // "EventosCorreio.AltData" oculta o membro herdado "ModeloBase.AltData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
#pragma warning disable CS0114 // "EventosCorreio.AltUsuario" oculta o membro herdado "ModeloBase.AltUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
		public virtual string AltUsuario { get; set; }
#pragma warning restore CS0114 // "EventosCorreio.AltUsuario" oculta o membro herdado "ModeloBase.AltUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.

    }
}
