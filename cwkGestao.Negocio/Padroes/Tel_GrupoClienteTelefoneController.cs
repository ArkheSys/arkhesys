using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Tel_GrupoClienteTelefoneController : BaseController<Tel_GrupoClienteTelefone>
	{
		#region Singleton
        private static Tel_GrupoClienteTelefoneController instance;
        private static IRepositorioTel_GrupoClienteTelefone repositorioTel_GrupoClienteTelefone;

        private Tel_GrupoClienteTelefoneController()
        { }

        static Tel_GrupoClienteTelefoneController()
        {
            instance = new Tel_GrupoClienteTelefoneController();
            repositorioT = RepositorioFactory<Tel_GrupoClienteTelefone>.GetRepositorio();
            repositorioTel_GrupoClienteTelefone = (IRepositorioTel_GrupoClienteTelefone)repositorioT;
        }

        public static Tel_GrupoClienteTelefoneController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Tel_GrupoClienteTelefone objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<Tel_GrupoClienteTelefone> GetDisponiveisPorGrupo(Tel_GrupoCliente grupoCliente, DateTime dataAtivacao, string telefone)
        {
            return repositorioTel_GrupoClienteTelefone.GetDisponiveisPorGrupo(grupoCliente, dataAtivacao, telefone);
        }
	}
}
