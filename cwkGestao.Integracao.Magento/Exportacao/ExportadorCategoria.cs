using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Integracao.Magento.WebService;
using Ez.Newsletter.MagentoApi;
using cwkGestao.Negocio;
using System.Threading;

namespace cwkGestao.Integracao.Magento.Exportacao
{
    public class ExportadorCategoria : IExportador<GrupoEstoque>
    {
        private readonly Comunicador comunicador;
        private readonly MagentoConfiguracao configuracao;

        public ExportadorCategoria(MagentoConfiguracao configuracao, Comunicador comunicador)
        {
            this.configuracao = configuracao;
            this.comunicador = comunicador;
        }

        
        public void Exportar(GrupoEstoque objeto)
        {
            /*
            if (configuracao.IDCategoriaPadrao <= 0)
                throw new ArgumentException("Configure a ID da categoria padrão na tabela MagentoConfiguracao.");

            ExportarGrupoEstoque(objeto);
            */
        }

        /*
        private void ExportarGrupoEstoque(GrupoEstoque grupoEstoque)
        {
            int idCategoriaPai = configuracao.IDCategoriaPadrao;
            if (grupoEstoque.GrupoEstoquePai != null)
            {
                if (grupoEstoque.GrupoEstoquePai.IDIntegracao == 0)
                    ExportarGrupoEstoque(grupoEstoque.GrupoEstoquePai);
                idCategoriaPai = grupoEstoque.GrupoEstoquePai.IDIntegracao;
            }

            var categoria = new Category();
            categoria.description = grupoEstoque.Nome;
            categoria.name = grupoEstoque.Nome;
            categoria.is_active = "1";
            categoria.include_in_menu = "1";
            categoria.available_sort_by = "position";
            categoria.default_sort_by = "position";
            if (grupoEstoque.IDIntegracao == 0)
            {
                var idIntegracao = comunicador.IncluirCategoria(idCategoriaPai, categoria);
                grupoEstoque.IDIntegracao = Convert.ToInt32(idIntegracao);
            }
            else
            {
                categoria.parent_id = idCategoriaPai;
                categoria.category_id = grupoEstoque.IDIntegracao.ToString();
                comunicador.AlterarCategoria(categoria);
            }
            GrupoEstoqueController.Instancia.Salvar(grupoEstoque, Acao.Alterar);
            
        }
        */
    }
}
