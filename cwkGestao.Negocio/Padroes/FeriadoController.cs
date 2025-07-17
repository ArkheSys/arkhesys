using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class FeriadoController : BaseController<Feriado>
	{
		#region Singleton
        private static FeriadoController instance;
        private static IRepositorioFeriado repositorioFeriado;

        private FeriadoController()
        { }

        static FeriadoController()
        {
            instance = new FeriadoController();
            repositorioT = RepositorioFactory<Feriado>.GetRepositorio();
            repositorioFeriado = (IRepositorioFeriado)repositorioT;
        }

        public static FeriadoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
	
        public override Dictionary<string, string> ValidaObjeto(Feriado objeto)
        {
            Dictionary<string, string> erros = new Dictionary<string, string>();
            if (objeto.Data == null)
                erros.Add("txtData", "Informe a data.");
#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'int' nunca é igual a "null" do tipo 'int?'
            if (objeto.Codigo == null || objeto.Codigo <= 0)
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'int' nunca é igual a "null" do tipo 'int?'
                erros.Add("txtCodigo", "Código inválido");
            return erros;
        }
    }
}
