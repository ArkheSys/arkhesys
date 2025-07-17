using cwkNotaFiscalEletronica.Interfaces;
using MO = Modelo;
using cwkGestao.NFe.Adapters.NH;
using System;
using cwkGestao.NFe.Adapters.Linq;

namespace cwkGestao.NFe.Adapters
{
    public abstract class AdapterEnderecoEntrega : IEnderecoEntrega
    {
        public static AdapterEnderecoEntrega Produce(object enderecoEntrega)
        {
            if (enderecoEntrega.GetType() == typeof(MO.Endereco))
            {
                return new LinqEnderecoEntrega((MO.Endereco)enderecoEntrega);
            }
            else if (enderecoEntrega.GetType() == typeof(Modelo.PessoaEndereco) || enderecoEntrega.GetType().IsSubclassOf(typeof(Modelo.PessoaEndereco)))
            {
                return new NHEnderecoEntrega((Modelo.PessoaEndereco)enderecoEntrega);
            }

            throw new ArgumentException("Parâmetro passado não é do tipo Modelo.PessoaEndereco ou cwkGestao.Modelo.PessoaEndereco");
        }

        public abstract string Bairro { get; }

        public abstract string CidadeCodigoIBGE { get; }

        public abstract string CidadeNome { get; }

        public abstract string Complemente { get; }

        public abstract string Logradouro { get; }

        public abstract string Numero { get; }

        public abstract string CEP { get; }

        public abstract string UFSigla
        {
            get; set; 
        }
    }
}