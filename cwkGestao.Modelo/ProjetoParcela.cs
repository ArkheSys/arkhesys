using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class ProjetoParcela : ParcelaBase
    {
        public ProjetoParcela()
        {
        }

        public ProjetoParcela(ProjetoParcela pp)
        {
            foreach (var prop in pp.GetType().GetProperties().Where(propin => !propin.Name.Equals("ID")))
            {
                if(GetType().GetProperty(prop.Name) != null)
                    GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(pp, null), null);
            }
        }
        public virtual Projeto Projeto { get; set; }

       
    }
}
