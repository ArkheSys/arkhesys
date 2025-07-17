using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Controle de Rotas")]
    public class Rota : ModeloBase , ICloneable
	{
        public override int ID { get; set; }
        [InformacaoExibicao(1, "Código", 80)]
		public virtual int Codigo { get; set; }
        [InformacaoExibicao(2, "Data", 80)]
        public virtual DateTime Data { get; set; }
		public virtual Filial Filial { get; set; }
        [InformacaoExibicao(3, "Descrição", 250)]
		public virtual string Descricao { get; set; }
		public virtual Pessoa Responsavel { get; set; }
		
        [InformacaoExibicao(11, "Observação", 250)]
		public virtual string Observacao { get; set; }

        [InformacaoExibicao(5, "Filial", 250)]
        public virtual string EmpresaVisivel
        {
            get { return this.Filial.Nome; }
        }

        [InformacaoExibicao(4, "Responsável", 250)]
        public virtual string ResponsavelVisivel
        {
            get
            {
                return this.Responsavel == null ? "" : this.Responsavel.Nome;
            }
        }

        [InformacaoExibicao(6, "Leituras", 70)]
        public virtual int Leituras
        {
            get { return RotaDetalhes.Count(); }
        }

        [InformacaoExibicao(8, "Efetuadas", 70)]
        public virtual int Efetuadas
        {
            get { return RotaDetalhes.Where(x => x.Situacao == 1).Count(); }
        }

        [InformacaoExibicao(9, "Canceladas", 70)]
        public virtual int Canceladas
        {
            get { return  RotaDetalhes.Where(x => x.Situacao == 2).Count(); }
        }

        [InformacaoExibicao(10, "Pendentes", 70)]
        public virtual int Pendentes
        {
            get { return RotaDetalhes.Where(x => x.Situacao == 0).Count(); }
        }

        public virtual IList<RotaDetalhe> RotaDetalhes { get; set; }

        #region ICloneable Members

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
