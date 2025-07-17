using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Cliente Telefonia")]
    public class Tel_Cliente : ModeloBase
    {
        public Tel_Cliente()
        {
            Pessoa = new Pessoa() { BCliente = true, BAtivo = true };
            Pessoa.PessoaClientes.Add(new PessoaCliente() { Pessoa = Pessoa });
            Precos = new List<Tel_ClientePreco>();
            Telefones = new List<Tel_ClienteTelefone>();
            Servicos = new List<Tel_ClienteServico>();
        }

        public Tel_Cliente(int id, string nome, string cnpjCpf, string grupo, string fantasia, int codigo, string telefone)
            : this()
        {
            ID = id;
            Pessoa.Nome = nome;
            Pessoa.CNPJ_CPF = cnpjCpf;
            Pessoa.Fantasia = fantasia;
            Pessoa.Codigo = codigo;
            GrupoCliente = new Tel_GrupoCliente { Nome = grupo };
            NumeroPiloto = telefone;
        }

        public override int ID { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        [InformacaoExibicao(0, "Cliente", 300)]
        public virtual string PessoaNome { get { return Pessoa != null ? Pessoa.Nome : String.Empty; } }
        public virtual Tel_GrupoCliente GrupoCliente { get; set; }
        [InformacaoExibicao(2, "Grupo", 200)]
        public virtual string GrupoNome { get { return GrupoCliente != null ? GrupoCliente.Nome : String.Empty; } }
        public virtual Acrescimo Acrescimo { get; set; }
        public virtual bool ImprimirLigacoesLocais { get; set; }
        public virtual IList<Tel_ClientePreco> Precos { get; set; }
        public virtual IList<Tel_ClienteTelefone> Telefones { get; set; }
        public virtual IList<Tel_ClienteServico> Servicos { get; set; }
        public virtual string CpfProp { get; set; }
        public virtual DateTime DtNascimentoProp { get; set; }
        public virtual string NomeProp{ get; set; }
        public virtual string EnderecoProp{ get; set; }
        public virtual int NumeroEnderecoProp{ get; set; }
        public virtual string ComplementoProp { get; set; }
        public virtual string  BairroProp { get; set; }
        public virtual Cidade CidadeProp { get; set; }
        public virtual string TelefoneProp { get; set; }
        public virtual bool CalculaTotalMinutos { get; set; }
        

        [InformacaoExibicao(1, "CPF/CNPJ", 120)]
        public virtual string CPF_CNPJ { get { return Pessoa != null ? Pessoa.CNPJ_CPF : String.Empty; } }
        
        [InformacaoExibicao(3, "Fantasia", 200)]
        public virtual string Fantasia { get { return Pessoa != null ? Pessoa.Fantasia : String.Empty; } }

        [InformacaoExibicao(4, "Num. Piloto", 100, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual string NumeroPiloto { get; set; }

        [InformacaoExibicao(5, "Codigo", 80, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int Codigo { get { return Pessoa != null ? Pessoa.Codigo : 0; } }
    }
}
