using cwkConsultaOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkConsultaOS.Controller
{
    public class OSOrdemServicoController
    {
        #region Singleton
        private static OSOrdemServicoController instance;
        private static OSOrdemServicoRepositorio repositorio;

        private OSOrdemServicoController()
        { }

        static OSOrdemServicoController()
        {
            instance = new OSOrdemServicoController();
            repositorio = new OSOrdemServicoRepositorio();
        }

        public static OSOrdemServicoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public virtual OrdemServico LoadObjectById(int id)
        {
            return repositorio.LoadObjectById(id);
        }

        public IList<OrdemServico> GetAll()
        {
            return repositorio.GetAll();
        }
    }
}
