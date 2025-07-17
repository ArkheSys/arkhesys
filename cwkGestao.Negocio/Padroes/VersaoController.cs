using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class VersaoController : BaseController<Versao>
    {
        #region Singleton
        private static VersaoController instance;
        private static IRepositorioVersao repositorioVersao;

        private VersaoController()
        { }

        static VersaoController()
        {
            instance = new VersaoController();
            repositorioT = RepositorioFactory<Versao>.GetRepositorio();
            repositorioVersao = (IRepositorioVersao)repositorioT;
        }

        public static VersaoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Versao objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (objeto.Codigo == 0)
            {
                ret.Add("txtCodigo", "Valor tem que ser diferente de zero (0).");
            }

            if (String.IsNullOrEmpty(objeto.Numero))
            {
                ret.Add("txtNumero", "Campo obrigatório.");
            }
            return ret;
        }

        public Dictionary<string, string> Salvar(Versao objeto, Acao acao, Sistema objSistema)
        {
            if (acao == Acao.Excluir)
            {

                if (!repositorioVersao.IsReferenced(objeto))
                    objSistema.Versaos.Remove(objeto);
                else
                    throw new Exception("Versão utilizada em outros registros.\nImpossível excluir.");
                
                
                
                return new Dictionary<string, string>();
            }
            else
            {
                Dictionary<string, string> ret = ValidaObjeto(objeto);
                if (ret.Count == 0 && acao == Acao.Incluir)
                {
                    objeto.Sistema = objSistema;
                    objSistema.Versaos.Add(objeto);
                }
                return ret;
            }
        }

        public int MaxCodigo(IList<Modelo.Versao> versoes)
        {
            try
            {
                return versoes.Max(v => v.Codigo) + 1;
            }
            catch
            {
                return 1;
            }
        }

        public IList<Versao> GetAllBySistema(int pIdSistema)
        {
            return repositorioVersao.GetAllBySistema(pIdSistema);
        }

        public IList<cwkGestao.Modelo.Versao> GetVersaoSistema(Int32 IDSistema)
        {
            return repositorioVersao.GetAllBySistema(IDSistema);
        }
    }
}
