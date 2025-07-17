using System;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Fatura D8")]
    public class Tel_FaturaD8 : ModeloBase
    {
        #region Propriedades

        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 85)]
        public virtual int Codigo { get; set; }

        public virtual Filial Filial { get; set; }

        [InformacaoExibicao(2, "Data", 80)]
        public virtual DateTime DataCriacao { get; set; }

        [InformacaoExibicao(4, "Vencimento", 80)]
        public virtual DateTime DataVencimento { get; set; }

        public virtual string NumeroDocumento { get; set; }

        public virtual string EspecieDocumento { get; set; }

        [InformacaoExibicao(8, "Valor", 85, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Valor { get; set; }

        public virtual Tel_Cliente Cliente { get; set; }

        [InformacaoExibicao(6, "Cliente", 165)]
        public virtual string NomeCliente { get { return Cliente.Pessoa.Nome; } }

        public virtual string Fantasia { get { return Cliente.Pessoa.Fantasia; } }

        public virtual Tel_Servico Servico { get; set; }

        public virtual string Observacao { get; set; }

        public virtual string LinhaDigitavel { get; set; }
        public virtual string LinhaDigitavelComDv { get; set; }

        //public virtual Documento Documento { get; set; }

        //[InformacaoExibicao(9, "Documento", 85)]
        //public virtual String DescDocumento { get { return Documento == null ? null : Documento.TipoDocumento.Nome; } }

        public virtual string PixCopiaECola { get; set; }
        public virtual string Location { get; set; }
        public virtual string JsonPix { get; set; }

        public virtual string Base64Image { get; set; }

        #endregion
    }
}
