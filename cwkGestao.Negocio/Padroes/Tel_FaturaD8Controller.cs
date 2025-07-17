using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class Tel_FaturaD8Controller : BaseController<Tel_FaturaD8>
    {
        #region Singleton
        private static Tel_FaturaD8Controller instance;
        private static IRepositorioTel_FaturaD8 repositorioTel_FaturaD8;

        private Tel_FaturaD8Controller()
        { }

        static Tel_FaturaD8Controller()
        {
            instance = new Tel_FaturaD8Controller();
            repositorioT = RepositorioFactory<Tel_FaturaD8>.GetRepositorio();
            repositorioTel_FaturaD8 = (IRepositorioTel_FaturaD8)repositorioT;
        }

        public static Tel_FaturaD8Controller Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Tel_FaturaD8 objeto)
        {
            return new Dictionary<string, string>();
        }

        //internal IList<Tel_FaturaD8> GetByGrupo(Tel_GrupoCliente grupoCliente)
        //{
        //    return repositorioTel_FaturaD8.GetByGrupo(grupoCliente);
        //}

        //public Tel_FaturaD8 GetPessoaID(int ID)
        //{
        //    return repositorioTel_FaturaD8.GetPessoaID(ID);
        //}

        //public Tel_FaturaD8 GetByTelefone(string telefone)
        //{
        //    return repositorioTel_FaturaD8.GetByTelefone(telefone);
        //}

        //public IList<Tel_FaturaD8> GetAllByGrupo(Tel_GrupoCliente tel_GrupoCliente)
        //{
        //    return repositorioTel_FaturaD8.GetAllByGrupo(tel_GrupoCliente);
        //}

    }
}
