using System;
using System.Collections.Generic;

namespace cwkGestao.Modelo
{
    [Serializable]
    // Seus atributos aqui, como [NomeTabela("NCMCEST")] do NHibernate
    public class NCMCEST : ModeloBase
    {
        // A CORREÇÃO ESTÁ AQUI: Adicionado "override"
        public override int ID { get; set; }

        // Removi as propriedades IDNCM e IDCEST pois elas não são necessárias
        // O NHibernate vai gerenciá-las através dos objetos NCM e CEST abaixo.
        // Se o seu código precisa delas por algum motivo, pode mantê-las.

        public virtual NCM NCM { get; set; }

        public virtual CEST CEST { get; set; }
    }
}