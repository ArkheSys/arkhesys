using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class Cw_acessoController : BaseController<Cw_acesso>
    {
        #region Singleton
        private static Cw_acessoController instance;
        private static IRepositorioCw_acesso repositorioCw_acesso;

        private Cw_acessoController()
        { }

        static Cw_acessoController()
        {
            instance = new Cw_acessoController();
            repositorioT = RepositorioFactory<Cw_acesso>.GetRepositorio();
            repositorioCw_acesso = (IRepositorioCw_acesso)repositorioT;
        }

        public static Cw_acessoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Cw_acesso objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }


        public Cw_acesso LoadAcessoByGrupoTextoForm(int idGrupo, String textoFormulario)
        {
            return repositorioCw_acesso.LoadAcessoByGrupoTextoForm(idGrupo, textoFormulario);
        }

        public bool PossuiAcesso(Cw_acesso acessoCampoGrupoUsuario, string nomeControle, int tipoGrupo)
        {
            Boolean bRetorno = false;
            try
            {
                if (tipoGrupo == 0)
                {
                    bRetorno = true;
                }
                else
                {
                    Cw_acessocampo acessoCampo = acessoCampoGrupoUsuario.Cw_acessocampos.Where(s => s.Campo == nomeControle).FirstOrDefault();
                    bRetorno = Convert.ToBoolean(acessoCampo.Acesso);
                }
            }
            catch
            {
                bRetorno = false;
            }

            return bRetorno;
        }
    }
}
