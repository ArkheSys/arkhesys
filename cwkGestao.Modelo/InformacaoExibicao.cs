using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class InformacaoExibicao : Attribute
    {
        private String[] maskArray = { "", "c2", "f4", "{0:####0.00##}%", "f2", "g", "y"};

        public enum HAlinhamento { Padrao = 0, Esquerda = 1, Centro = 2, Direita = 3 };
        public enum Mascaras { Nenhuma = 0, Dinheiro = 1, Decimal = 2, Porcentagem4Casas = 3,Decimal2Casas = 4, DataHora = 5, MesAno = 6 };
        public enum FormaOrdenacao { Crescente = 0, Decrescente = 1 }
        
        public int Sequencia { get; private set; }
        public string Header { get; set; }
        public int ColumnWidth { get; private set; }
        public HAlinhamento Alinhamento { get; private set; }
        public String FormatString { get; private set; }
        public FormaOrdenacao? Ordenacao { get; private set; }
        public Boolean BNaoInserirColuna { get; private set; } = false;
         


        public InformacaoExibicao(string _header)
            : this(-1, _header)
        {

        }

        /// <summary>
        /// Cria uma annotation padrão com tamanho 50.
        /// </summary>
        /// <param name="_sequencia">Ordem que estará a coluna no grid. Deve ser maior que 0 (zero).</param>
        /// <param name="_header">Nome de apresentação da coluna.</param>
        public InformacaoExibicao(int _sequencia, string _header)
            : this(_sequencia, _header, 50)
        { 
            
        }

        /// <summary>
        /// Cria uma annotation com os parâmetros passados.
        /// </summary>
        /// <param name="_sequencia">Ordem que estará a coluna no grid. Deve ser maior que 0 (zero).</param>
        /// <param name="_header">Nome de apresentação da coluna.</param>
        /// <param name="_columnWidth">Tamanho da coluna.</param>
        public InformacaoExibicao(int _sequencia, string _header, int _columnWidth)
            : this(_sequencia, _header, _columnWidth, HAlinhamento.Padrao)
        {
            
        }

   
        public InformacaoExibicao(int _sequencia, string _header, int _columnWidth, FormaOrdenacao formaOrdenacao)
            : this(_sequencia, _header, _columnWidth, HAlinhamento.Padrao, Mascaras.Nenhuma, formaOrdenacao)
        {

        }

        public InformacaoExibicao(int _sequencia, string _header, int _columnWidth, HAlinhamento _alinhamento)
            : this(_sequencia, _header, _columnWidth, _alinhamento, Mascaras.Nenhuma)
        {
           
        }

        public InformacaoExibicao(int _sequencia, string _header, int _columnWidth, HAlinhamento _alinhamento,Mascaras _formatString)
            : this(_sequencia, _header, _columnWidth, _alinhamento, _formatString, null )
        {
        }


        public InformacaoExibicao(Boolean bNaoInserirColuna, int _sequencia, string _header, int _columnWidth, HAlinhamento _alinhamento, Mascaras _formatString)
            : this(_sequencia, _header, _columnWidth, _alinhamento, _formatString, null)
        {
            BNaoInserirColuna = bNaoInserirColuna;
        }

        public InformacaoExibicao(int _sequencia, string _header, int _columnWidth, HAlinhamento _alinhamento, Mascaras _formatString, FormaOrdenacao? ordenacao)
        {
            Sequencia = _sequencia;
            Header = _header;
            ColumnWidth = _columnWidth;
            Alinhamento = _alinhamento;
            FormatString = maskArray[(int)_formatString];
            Ordenacao = ordenacao;
        }


    }
}
