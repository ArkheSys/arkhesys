using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MO = Modelo;

namespace cwkGestao.NFe.Adapters.Linq
{
    internal class LinqCliente : AdapterCliente
    {
        MO.Pessoa pessoa;

        public LinqCliente(MO.Pessoa _pessoa)
            : base()
        {
            pessoa = _pessoa;
        }

        public override string Bairro
        {
            get { return pessoa.Enderecos.Where(i => i.bPrincipal == 1).Single().Bairro; }
        }

        public override string Cep
        {
            get { return pessoa.Enderecos.Where(i => i.bPrincipal == 1).Single().CEP; }
        }

        public override string Cidade
        {
            get { return pessoa.Enderecos.Where(i => i.bPrincipal == 1).Single().Cidade.Nome; }
        }

        public override string CidadeIBGE
        {
            get { return pessoa.Enderecos.Where(i => i.bPrincipal == 1).Single().Cidade.IBGE; }
        }

        public override string Cnpj
        {
            get { return pessoa.CNPJ_CPF; }
        }

        public override string Complemento
        {
            get { return pessoa.Enderecos.Where(i => i.bPrincipal == 1).Single().Complemento; }
        }

        public override string Cpf
        {
            get { return pessoa.CNPJ_CPF; }
        }

        public override string Email
        {
            get { return pessoa.PrimeiroEmailNFe(); }
        }

        public override string Endereco
        {
            get { return pessoa.Enderecos.Where(i => i.bPrincipal == 1).Single().Rua; }
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
            get { return pessoa.Enderecos.Where(i => i.bPrincipal == 1).Single().Numero; }
        }

        public override string Observacao
        {
            get { return pessoa.Observacao; }
        }

        public override string Pais
        {
            get { return pessoa.Enderecos.Where(i => i.bPrincipal == 1).Single().Cidade.UF.Pai.Nome; }
        }

        public override string PaisIBGE
        {
            get { return pessoa.Enderecos.Where(i => i.bPrincipal == 1).Single().Cidade.UF.Pai.IBGE; }
        }

        public override string RazaoSocial
        {
            get { return pessoa.Nome; }
        }

        public override string Telefone
        {
            get { return pessoa.Telefones.Where(i => i.bPrincipal == 1).Single().Numero; }
        }

        public override bool Tipo
        {//campo não utilizado
            get { return false; }
        }

        public override string UF
        {
            get { return pessoa.Enderecos.Where(i => i.bPrincipal == 1).Single().Cidade.UF.Sigla; }
        }

        public override string UFIBGE
        {
            get { return pessoa.Enderecos.Where(i => i.bPrincipal == 1).Single().Cidade.UF.IBGE; }
        }

        public override bool bContribuinte
        {//campo não utilizado
            get { return false; }
        }
    }
}

