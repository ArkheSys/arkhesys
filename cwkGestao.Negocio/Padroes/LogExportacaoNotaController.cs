using System;
using System.Collections.Generic;

using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class LogExportacaoNotaController : BaseController<LogExportacaoNota>
    {
        #region Singleton
        private static LogExportacaoNotaController instance;
        private static IRepositorioLogExportacaoNota repositorioLogExportacaoNota;

        private LogExportacaoNotaController()
        { }

        static LogExportacaoNotaController()
        {
            instance = new LogExportacaoNotaController();
            repositorioT = RepositorioFactory<LogExportacaoNota>.GetRepositorio();
            repositorioLogExportacaoNota = (IRepositorioLogExportacaoNota)repositorioT;
        }

        public static LogExportacaoNotaController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(LogExportacaoNota objeto) { return new Dictionary<string, string>(); }
    }
}
