using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Telefonia;

namespace cwkGestao.Negocio.Telefonia
{
    public delegate void AvancaProgressBarDelegate();

    public class ImportadorLigacoes
    {
        private Modelo.Tel_ArquivoLigacao arquivoLigacao;
        private readonly IImportador importador;
        private readonly Dictionary<string,cwkGestao.Modelo.Tel_Ligacao> ligacoesExistentes;
        private readonly HashSet<int> tiposCadastrados; 
        public AvancaProgressBarDelegate AvancarProgressBar { get; set; }

        public ImportadorLigacoes(Modelo.TipoArquivoLigacoes tipo, string caminho, string referencia, int area, int range)
        {
            arquivoLigacao = new Modelo.Tel_ArquivoLigacao();
            arquivoLigacao.Codigo = ArquivoLigacaoController.Instancia.MaxCodigo();
            arquivoLigacao.CaminhoArquivo = caminho;
            arquivoLigacao.DataImportacao = DateTime.Now;
            arquivoLigacao.TipoArquivo = tipo;
            arquivoLigacao.Referencia = Convert.ToDateTime("01/" + referencia);

            int tipoSumus = 0;
            switch (tipo)
            {
                case Modelo.TipoArquivoLigacoes.GVT:
                    importador = new ImportadorAccess(caminho);
                    break;
                case Modelo.TipoArquivoLigacoes.SUMUS:
                    tipoSumus = 1;
                    importador = new ImportadorTXT(caminho, area, range, tipoSumus);
                    break;
                case Modelo.TipoArquivoLigacoes.SUMUS2:
                    tipoSumus = 2;
                    importador = new ImportadorTXT(caminho, area, range, tipoSumus);
                    break;
                case Modelo.TipoArquivoLigacoes.SERCONTEL:
                    importador = new ImportadorExcel(caminho);
                    break;
                case Modelo.TipoArquivoLigacoes.ALGAR:
                    importador = new ImportadorExcel(caminho, true);
                    break;
            }

            ligacoesExistentes = LigacaoController.Instancia.GetHashLigacoesArquivo(arquivoLigacao.Referencia);

            tiposCadastrados = Tel_TipoLigacaoController.Instancia.GetHashCodigos();
        }

        public void Importar()
        {
            foreach (var item in importador.Importar())
            {
                if (item == null)
                    continue;

                try
                {
                    var ligacao = new cwkGestao.Modelo.Tel_Ligacao
                    {
                        Cidade = item.Cidade,
                        DataHora = item.DataHora,
                        DescricaoUso = item.DescricaoUso,
                        Destino = item.Destino,
                        Horario = item.Horario,
                        Origem = item.Origem,
                        TempoSegundos = item.TempoSegundos,
                        TipoUso = item.TipoUso,
                        UF = item.Uf,
                        ValorComImposto = item.ValorComImposto,
                        ArquivoLigacao = arquivoLigacao
                    };
                    var chave = ligacao.GetChaveUnica();
                    if (!ligacoesExistentes.ContainsKey(chave))
                    {
                        ligacoesExistentes.Add(chave, ligacao);
                        LigacaoController.Instancia.RegisterNewIntoTransaction(ligacao);
                    }
                    AdicionarTipoUso(item);
                    TenteAvancarProgressBar();
                }
                catch 
                {

                }
                
            }
            LigacaoController.Instancia.CommitUnitOfWorkTransaction();
        }

        public void ImportarFixoECelular()
        {
            foreach (var item in importador.ImportarFixoECelular())
            {
                if (item == null)
                    continue;

                var ligacao = new cwkGestao.Modelo.Tel_Ligacao
                {
                    Cidade = item.Cidade,
                    DataHora = item.DataHora,
                    DescricaoUso = item.DescricaoUso,
                    Destino = item.Destino,
                    Horario = item.Horario,
                    Origem = item.Origem,
                    TempoSegundos = item.TempoSegundos,
                    TipoUso = item.TipoUso,
                    UF = item.Uf,
                    ValorComImposto = item.ValorComImposto,
                    ArquivoLigacao = arquivoLigacao
                };
                var chave = ligacao.GetChaveUnica();
                if (!ligacoesExistentes.ContainsKey(chave))
                {
                    ligacoesExistentes.Add(chave, ligacao);
                    LigacaoController.Instancia.RegisterNewIntoTransaction(ligacao);
                }
                AdicionarTipoUso(item);
                TenteAvancarProgressBar();
            }
            LigacaoController.Instancia.CommitUnitOfWorkTransaction();
        }

        private void AdicionarTipoUso(Ligacao ligacao)
        {
            if (tiposCadastrados.Contains(ligacao.TipoUso)) return;

            tiposCadastrados.Add(ligacao.TipoUso);
            Tel_TipoLigacaoController.Instancia.RegisterNewIntoTransaction(new cwkGestao.Modelo.Tel_TipoLigacao
            {
                Codigo = ligacao.TipoUso,
                Descricao = ligacao.DescricaoUso
            });
        }

        private void TenteAvancarProgressBar()
        {
            if (AvancarProgressBar != null)
                AvancarProgressBar();
        }

        public int GetQuantidadeRegistros()
        {
            return importador.GetQuantidadeRegistros();
        }
    }    
}
