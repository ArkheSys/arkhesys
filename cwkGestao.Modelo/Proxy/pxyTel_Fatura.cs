using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    [InformacaoExibicao("Fatura")]
    public class pxyTel_Fatura
    {
        public pxyTel_Fatura(int id, DateTime vencimento, decimal valorPagar, int codigoFatura
                            , string numeroPiloto, string nome, string contato, string endereco
                            , string numeroEndereco, string bairro, string complemento
                            , string cidade, string cep, int idDocumento, bool imprimirLigacoesLocais, string fantasia, string estado)
        {
            ID = id;
            Vencimento = vencimento;
            ValorPagar = valorPagar;
            CodigoFatura = codigoFatura;
            NumeroPiloto = numeroPiloto;
            Fantasia = fantasia;
            NomeCliente = nome;
            ContatoCliente = contato;
            EnderecoCliente = endereco;
            NumeroEnderecoCliente = numeroEndereco;
            BairroCliente = bairro;
            ComplementoCliente = complemento;
            CidadeCliente = cidade;
            CepCliente = cep;
            IDDocumento = idDocumento;
            ImprimirLigacoesLocais = imprimirLigacoesLocais;
            EstadoCidadeCliente = estado;
        }

        public virtual int ID { get; set; }
        public virtual DateTime Vencimento { get; set; }
        public virtual string NumeroPiloto { get; set; }
        public virtual decimal ValorPagar { get; set; }
        public virtual int CodigoFatura { get; set; }

        public virtual string Fantasia { get; set; }
        public virtual string NomeCliente { get; set; }
        public virtual string ContatoCliente { get; set; }
        public virtual string EnderecoCliente { get; set; }
        public virtual string NumeroEnderecoCliente { get; set; }
        public virtual string BairroCliente { get; set; }
        public virtual string ComplementoCliente { get; set; }
        public virtual string CidadeCliente { get; set; }
        public virtual string CepCliente { get; set; }
        public virtual int IDDocumento { get; set; }
        public virtual bool ImprimirLigacoesLocais { get; set; }
        public virtual string EstadoCidadeCliente { get; set; }
    }
}
