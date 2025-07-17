using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyListagemPreco
    {
        public pxyListagemPreco(string codigo, string nomeProduto, string unidade
                        , decimal precoBase, decimal margemLucro, string nomeGrupo
                        , string classificacaoGrupoEstoque, int idGrupoEstoque, string nomeGrupoEstoque, string codBarra, string observacao, string reduzida)
        {
            Codigo = codigo;
            NomeProduto = nomeProduto;
            Unidade = unidade;
            Preco = precoBase + (precoBase * margemLucro / 100);
            NomeGrupo = nomeGrupo;
            ClassificacaoGrupoEstoque = classificacaoGrupoEstoque;
            IDGrupoEstoque = idGrupoEstoque;
            NomeGrupoEstoque = nomeGrupoEstoque;
            CodBarra = codBarra;
            Observacao = observacao;
            Reduzida = reduzida;
        }
        public pxyListagemPreco(int id,string codigo, string nomeProduto, string unidade
                            , decimal precoBase, decimal margemLucro, string nomeGrupo
                            , string classificacaoGrupoEstoque, int idGrupoEstoque, string nomeGrupoEstoque, string codBarra, string observacao, string reduzida)
        {
            ID = id;
            Codigo = codigo;
            NomeProduto = nomeProduto;
            Unidade = unidade;
            Preco = precoBase + (precoBase * margemLucro / 100);
            NomeGrupo = nomeGrupo;
            ClassificacaoGrupoEstoque = classificacaoGrupoEstoque; 
            IDGrupoEstoque = idGrupoEstoque;
            NomeGrupoEstoque = nomeGrupoEstoque;
            CodBarra = codBarra;
            Observacao = observacao;
            Reduzida = reduzida;
        }

        public int ID { get; set; }
        public string Codigo { get; set; }
        public string NomeProduto { get; set; }
        public string Unidade { get; set; }
        public decimal Preco { get; set; }
        public string NomeGrupo { get; set; }
        public string ClassificacaoGrupoEstoque { get; set; }
        public int IDGrupoEstoque { get; set; }
        public string NomeGrupoEstoque { get; set; }
        public string CodBarra { get; set; }
        public int QtdEtiquetas { get; set; }
        public string Observacao { get; set; }
        public string Reduzida { get; set; }
        public bool Selecionado { get; set; }
    }
}
