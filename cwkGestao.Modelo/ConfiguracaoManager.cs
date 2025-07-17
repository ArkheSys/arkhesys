using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class ConfiguracaoManager : ModeloBase
    {
       public override int ID {get; set;}
       public virtual string grupo { get; set; }
       public virtual string usuario { get; set; }
       public virtual string senha { get; set; }
       public virtual string host { get; set; }
       public virtual string CaminhoPDFNFSE { get; set; }
       public virtual string CaminhoXMLNFSE { get; set; }
    }
}
