using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class Projeto_Material : ModeloBase
    {
        public override int ID { get; set; }
        public virtual Projeto Projeto { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual decimal Quantidade { get; set; }
#pragma warning disable CS0114 // "Projeto_Material.IncData" oculta o membro herdado "ModeloBase.IncData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public virtual DateTime IncData { get; set; }
#pragma warning restore CS0114 // "Projeto_Material.IncData" oculta o membro herdado "ModeloBase.IncData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public virtual DateTime IncHora { get; set; }
#pragma warning disable CS0114 // "Projeto_Material.IncUsuario" oculta o membro herdado "ModeloBase.IncUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public virtual string IncUsuario { get; set; }
#pragma warning restore CS0114 // "Projeto_Material.IncUsuario" oculta o membro herdado "ModeloBase.IncUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
#pragma warning disable CS0114 // "Projeto_Material.AltData" oculta o membro herdado "ModeloBase.AltData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public virtual DateTime AltData { get; set; }
#pragma warning restore CS0114 // "Projeto_Material.AltData" oculta o membro herdado "ModeloBase.AltData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public virtual DateTime AltHora { get; set; }
#pragma warning disable CS0114 // "Projeto_Material.AltUsuario" oculta o membro herdado "ModeloBase.AltUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public virtual string AltUsuario { get; set; }
#pragma warning restore CS0114 // "Projeto_Material.AltUsuario" oculta o membro herdado "ModeloBase.AltUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
    }
}