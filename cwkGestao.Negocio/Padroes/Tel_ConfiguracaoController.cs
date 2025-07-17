using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class Tel_ConfiguracaoController : BaseController<Tel_Configuracao>
    {
        #region Singleton
        private static Tel_ConfiguracaoController instance;
        private static IRepositorioTel_Configuracao repositorioTel_Configuracao;

        private Tel_ConfiguracaoController()
        { }

        static Tel_ConfiguracaoController()
        {
            instance = new Tel_ConfiguracaoController();
            repositorioT = RepositorioFactory<Tel_Configuracao>.GetRepositorio();
            repositorioTel_Configuracao = (IRepositorioTel_Configuracao)repositorioT;
        }

        public static Tel_ConfiguracaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public override Dictionary<string, string> ValidaObjeto(Tel_Configuracao objeto)
        {
            return new Dictionary<string, string>();
        }

        private volatile Tel_Configuracao configuracao;
        private readonly object configuracao_locker = new object();
        public Tel_Configuracao GetConfiguracao()
        {
            lock (configuracao_locker)
            {
                if (configuracao == null)
                    configuracao = repositorioTel_Configuracao.GetAll().FirstOrDefault();

                if (configuracao != null)
                    return configuracao;
                throw new Exception("A configuração de telefonia não foi encontrada, entre em contato com o suporte.");
            }
        }

        public override Dictionary<string, string> Salvar(Tel_Configuracao objeto, Acao acao)
        {
            lock (configuracao_locker)
            {
                var ret = base.Salvar(objeto, acao);
                configuracao = objeto;
                return ret;
            }
        }
    }
}
