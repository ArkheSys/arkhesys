using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.NFe.Adapters.NH
{
    class NHEnderecoEntrega :AdapterEnderecoEntrega
    {
        private readonly PessoaEndereco _enderecoEntrega;

        public NHEnderecoEntrega(PessoaEndereco enderecoEntrega)
        {
            _enderecoEntrega = enderecoEntrega;
        }

        public override string Bairro
        {
            get { return _enderecoEntrega.Bairro; }
        }

        public override string CidadeCodigoIBGE
        {
            get { return _enderecoEntrega.Cidade.IBGE; }
        }

        public override string CidadeNome
        {
            get { return _enderecoEntrega.Cidade.Nome; }
        }

        public override string Complemente
        {
            get { return _enderecoEntrega.Complemento; }
        }

        public override string Logradouro
        {
            get { return _enderecoEntrega.Endereco; }
        }

        public override string Numero
        {
            get { return _enderecoEntrega.Numero; }
        }

        public override string CEP
        {
            get { return _enderecoEntrega.CEP; }
        }

        public override string UFSigla
        {
            get { return _enderecoEntrega.Cidade.UF.Sigla; }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}