using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class ImpressaoEtiquetaPessoaView
    {

        public ImpressaoEtiquetaPessoaView(string nome, string cidade, string uf, string endereco
                                        , string numero, string Complemento, string bairro, string cep, int codigo
                                        , bool bFuncionario, bool bVendedor)
        {
            Nome = nome;
            Cidade = cidade + " - " + uf;
            Endereco = endereco + (String.IsNullOrEmpty(numero) ? "" : ", " + numero) +  (String.IsNullOrEmpty(Complemento) ? "" : ", " + Complemento);
            Bairro = bairro;
            CEP = cep;
            Codigo = codigo;
            BFuncionario = bFuncionario;
            BVendedor = bVendedor;
        }

        [InformacaoExibicao(1, "Imprimir", 75)]
        public virtual bool Imprimir { get; set; }
        [InformacaoExibicao(2, "Qtd.", 75)]
        public virtual decimal QuantidadeImprimir { get; set; }
        [InformacaoExibicao(3, "Nome", 429)]
        public virtual string Nome { get; set; }
        [InformacaoExibicao(4, "Cidade", 215)]
        public virtual string Cidade { get; set; }
        [InformacaoExibicao(5, "Endereço", 475)]
        public virtual string Endereco { get; set; }
        [InformacaoExibicao(6, "Bairro", 215)]
        public virtual string Bairro { get; set; }
        [InformacaoExibicao(7, "CEP", 100)]
        public virtual string CEP { get; set; }
        [InformacaoExibicao(8, "Codigo", 100)]
        public virtual int Codigo { get; set; }
        [InformacaoExibicao(9, "Funcionário", 80)]
        public virtual bool BFuncionario { get; set; }
        [InformacaoExibicao(10, "Vendedor", 80)]
        public virtual bool BVendedor { get; set; }
    }
}
