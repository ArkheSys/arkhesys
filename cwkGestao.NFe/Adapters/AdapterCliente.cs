using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkNotaFiscalEletronica.Interfaces;
using MO = Modelo;
using cwkGestao.NFe.Adapters.NH;
using cwkGestao.NFe.Adapters.Linq;

namespace cwkGestao.NFe.Adapters
{
    abstract class AdapterCliente : ICliente
    {
        public static AdapterCliente Produce(object cliente)
        {
            if (cliente.GetType() == typeof(MO.Pessoa))
            {
                return new LinqCliente((MO.Pessoa)cliente);
            }
            else if (cliente.GetType() == typeof(cwkGestao.Modelo.Pessoa))
            {
                return new NHCliente((cwkGestao.Modelo.Pessoa)cliente);
            }
            throw new ArgumentException("Parâmetro passado não é do tipo Modelo.Cliente ou cwkGestao.Modelo.Cliente");
        }

        #region ICliente Members

        public abstract  string Bairro
        {
            get;
        }

        public abstract  string Cep
        {
            get;
        }

        public abstract  string Cidade
        {
            get;
        }

        public abstract  string CidadeIBGE
        {
            get;
        }

        public abstract  string Cnpj
        {
            get;
        }

        public abstract  string Complemento
        {
            get;
        }

        public abstract  string Cpf
        {
            get;
        }

        public abstract  string Email
        {
            get;
        }

        public abstract  string Endereco
        {
            get;
        }

        public abstract  string Inscricao
        {
            get;
        }

        public abstract  string InscricaoSuframa
        {
            get;
        }

        public abstract  string Numero
        {
            get;
        }

        public abstract  string Observacao
        {
            get;
        }

        public abstract  string Pais
        {
            get;
        }

        public abstract  string PaisIBGE
        {
            get;
        }

        public abstract  string RazaoSocial
        {
            get;
        }

        public abstract  string Telefone
        {
            get;
        }

        public abstract  bool Tipo
        {
            get;
        }

        public abstract  string UF
        {
            get;
        }

        public abstract  string UFIBGE
        {
            get;
        }

        public abstract  bool bContribuinte
        {
            get;
        }

        #endregion
    }
}
