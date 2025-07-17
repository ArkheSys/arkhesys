using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao(0, "Endereços", 100)]
#pragma warning disable CS0659 // "PessoaEndereco" substitui Object.Equals(object o), mas não substitui Object.GetHashCode()
	public class PessoaEndereco : ModeloBase
#pragma warning restore CS0659 // "PessoaEndereco" substitui Object.Equals(object o), mas não substitui Object.GetHashCode()
	{
        public override int ID { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual int Sequencia { get; set; }
        public virtual string Descricao { get; set; }
        [InformacaoExibicao(1, "Endereço", 200)]
        public virtual string Endereco { get; set; }
        [InformacaoExibicao(3, "Bairro", 150)]
        public virtual string Bairro { get; set; }
        [InformacaoExibicao(4, "Cidade", 100)]
        public virtual Cidade Cidade { get; set; }
        public virtual string CEP { get; set; }
        public virtual string Telefone { get; set; }
        public virtual string Contato { get; set; }
        public virtual bool BEntrega { get; set; }
        public virtual bool BCobranca { get; set; }
        public virtual bool BAtivo { get; set; }
        public virtual bool BPrincipal { get; set; }
        [InformacaoExibicao(2, "Número", 50)]
        public virtual string Numero { get; set; }
        public virtual string Complemento { get; set; }
        public virtual int IDIntegracao { get; set; }
        public virtual bool RemetenteDestinatario { get; set; }

        public override string ToString()
        {
            return this.Endereco + ", " + this.Numero + " - " + this.Cidade.Nome + " - " + this.Cidade.UF.Sigla;
        }

        public override bool Equals(object obj)
        {
            try
            {
                PessoaEndereco end = (PessoaEndereco)obj;
                bool retorno = false;
                if ((end.Endereco == this.Endereco) &&
                    (end.Bairro == this.Bairro) &&
                    (end.Cidade.Nome == this.Cidade.Nome) &&
                    (end.Cidade.UF.Sigla == this.Cidade.UF.Sigla) &&
                    (end.Numero == this.Numero) &&
                    (end.Complemento == this.Complemento) &&
                    (end.CEP == this.CEP))
                {
                    retorno = true;
                }
                return retorno;
            }
            catch (Exception)
            {

                return false;
            }
        }
	}
}
		