using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.IO;
using System.Diagnostics;

namespace cwkGestao.Negocio
{
	public class Ofd_PropostaArquivosController : BaseController<Ofd_PropostaArquivos>
	{
		#region Singleton
        private static Ofd_PropostaArquivosController instance;
        private static IRepositorioOfd_PropostaArquivos repositorioOfd_PropostaArquivos;

        private Ofd_PropostaArquivosController()
        {
            PreencherTiposPorExtensao();
        }
		
		static Ofd_PropostaArquivosController()
        {
            instance = new Ofd_PropostaArquivosController();
            repositorioT = RepositorioFactory<Ofd_PropostaArquivos>.GetRepositorio();
            repositorioOfd_PropostaArquivos = (IRepositorioOfd_PropostaArquivos)repositorioT;
        }
			
		public static Ofd_PropostaArquivosController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        private readonly Dictionary<string, int> tiposPorExtensao = new Dictionary<string, int>();

        public int GetTipoPorExtensao(string extensao)
        {
            var extensaoMinuscula = extensao.ToLower();
            if (tiposPorExtensao.ContainsKey(extensaoMinuscula))
                return tiposPorExtensao[extensaoMinuscula];
            return -1;
        }

        private void PreencherTiposPorExtensao()
        {
            tiposPorExtensao.Add("doc", (int)TipoArquivoObra.Word);
            tiposPorExtensao.Add("docx", (int)TipoArquivoObra.Word);
            tiposPorExtensao.Add("txt", (int)TipoArquivoObra.Word);
            tiposPorExtensao.Add("rtf", (int)TipoArquivoObra.Word);
            tiposPorExtensao.Add("xls", (int)TipoArquivoObra.Excel);
            tiposPorExtensao.Add("xlsx", (int)TipoArquivoObra.Excel);
            tiposPorExtensao.Add("jpg", (int)TipoArquivoObra.Imagem);
            tiposPorExtensao.Add("jpeg", (int)TipoArquivoObra.Imagem);
            tiposPorExtensao.Add("jpe", (int)TipoArquivoObra.Imagem);
            tiposPorExtensao.Add("gif", (int)TipoArquivoObra.Imagem);
            tiposPorExtensao.Add("png", (int)TipoArquivoObra.Imagem);
            tiposPorExtensao.Add("bmp", (int)TipoArquivoObra.Imagem);
            tiposPorExtensao.Add("pdf", (int)TipoArquivoObra.PDF);
            tiposPorExtensao.Add("dwg", (int)TipoArquivoObra.CAD);
        }

		public override Dictionary<string, string> ValidaObjeto(Ofd_PropostaArquivos objeto)
        {
            return new Dictionary<string, string>();
        }

        public void AbrirArquivo(string caminho)
        {
            if (!String.IsNullOrEmpty(caminho) && File.Exists(caminho))
                Process.Start(caminho);
        }
    }
}
