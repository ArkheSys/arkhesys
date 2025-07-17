using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.NFe.Adapters.NH
{
    internal class NHCliente : AdapterCliente
    {
        Modelo.Pessoa pessoa;

        public NHCliente(Modelo.Pessoa _pessoa)
            : base()
        {
            pessoa = _pessoa;
        }
        public override string Bairro
        {
            get { return pessoa.EnderecoPrincipal().Bairro; }
        }

        public override string Cep
        {
            get { return pessoa.EnderecoPrincipal().CEP; }
        }

        public override string Cidade
        {
            get { return pessoa.EnderecoPrincipal().Cidade.Nome; }
        }

        public override string CidadeIBGE
        {
            get { return pessoa.EnderecoPrincipal().Cidade.IBGE; }
        }

        public override string Cnpj
        {
            get { return pessoa.CNPJ_CPF; }
        }

        public override string Complemento
        {
            get { return pessoa.EnderecoPrincipal().Complemento; }
        }

        public override string Cpf
        {
            get { return pessoa.CNPJ_CPF; }
        }

        public override string Email
        {
            get { return pessoa.PrimeiroEmail(i => i.BNFe); }
        }

        public override string Endereco
        {
            get { return pessoa.EnderecoPrincipal().Endereco; }
        }

        public override string Inscricao
        {
            get { return pessoa.Inscricao; }
        }

        public override string InscricaoSuframa
        {
            get { return ""; }
        }

        public override string Numero
        {
            get { return pessoa.EnderecoPrincipal().Numero; }
        }

        public override string Observacao
        {
            get { return pessoa.Observacao; }
        }

        public override string Pais
        {
            get { return pessoa.EnderecoPrincipal().Cidade.UF.Pais.Nome; }
        }

        public override string PaisIBGE
        {
            get { return pessoa.EnderecoPrincipal().Cidade.UF.Pais.IBGE; }
        }

        public override string RazaoSocial
        {
            get { return pessoa.Nome; }
        }

        public override string Telefone
        {
            get { return pessoa.PrimeiroTelefone(i => i.BPrincipal); }
        }

        public override bool Tipo
        {//campo não utilizado
            get { return false; }
        }

        public override string UF
        {
            get { return pessoa.EnderecoPrincipal().Cidade.UF.Sigla; }
        }

        public override string UFIBGE
        {
            get { return pessoa.EnderecoPrincipal().Cidade.UF.IBGE; }
        }

        public override bool bContribuinte
        {
            get { return pessoa.BContribuinte; }
        }
    }
}
