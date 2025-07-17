using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkConsultaOS
{
    public class OSOrdemServicoRepositorio
    {
        public OrdemServico LoadObjectById(int id)
        {
            OrdemServicoEntity db = new OrdemServicoEntity();
            return db.ORDEMSERVICO.FirstOrDefault(o => o.IDORDEMSERVICO == id);
        }

        public IList<OrdemServico> GetAll()
        {
            OrdemServicoEntity db = new OrdemServicoEntity();

            return db.ORDEMSERVICO.ToList();
        }
    }
}
