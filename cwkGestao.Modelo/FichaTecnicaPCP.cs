using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class FichaTecnicaPCP : ModeloBase, ICloneable
	{
        public override int ID { get; set; }
        [InformacaoExibicao(0, "Código", 90)]
		public virtual string Codigo { get; set; }
        
        public virtual bool Selecionado { get; set; }
        
        [InformacaoExibicao(2,"Versão",200)]
		public virtual string Nome { get; set; }
		public virtual string Barra { get; set; }
		public virtual DateTime Data { get; set; }
		public virtual int NumeroRevisao { get; set; }
		public virtual int PecasOP { get; set; }
		public virtual decimal ComprimentoBruto { get; set; }
		public virtual decimal ComprimentoLiquido { get; set; }
		public virtual decimal LarguraBruta { get; set; }
		public virtual decimal LarguraLiquida { get; set; }
		public virtual decimal EspessuraBruta { get; set; }
		public virtual decimal EspessuraLiquida { get; set; }
		public virtual decimal AreaBruta { get; set; }
		public virtual decimal AreaLiquida { get; set; }
		public virtual decimal VolumeBruto { get; set; }
		public virtual decimal VolumeLiquido { get; set; }
		public virtual decimal PesoBruto { get; set; }
		public virtual decimal PesoLiquido { get; set; }
		public virtual byte[] Imagem { get; set; }
		public virtual string Formula { get; set; }
		public virtual string Observacao { get; set; }
        [InformacaoExibicao(1, "Descrição", 200)]
		public virtual Produto Produto { get; set; }
        public virtual IList<FichaTecnicaPCPItem> FichaTecnicaPCPItens { get; set; }
        public virtual IList<FichaTecnicaPCPRegra> FichaTecnicaPCPRegras { get; set; }

        public FichaTecnicaPCP() { }

        public FichaTecnicaPCP(Produto Prod, String Versao)
        {
            this.Codigo = "";
            this.Nome = Versao;
            this.Barra = "";
            this.Data = DateTime.Now;
            this.NumeroRevisao = 0;
            this.PecasOP = 0;
            this.ComprimentoBruto = 0;
            this.ComprimentoLiquido = 0;
            this.LarguraBruta = 0;
            this.LarguraLiquida = 0;
            this.EspessuraBruta = 0;
            this.EspessuraLiquida = 0;
            this.AreaBruta = 0;
            this.AreaLiquida = 0;
            this.VolumeBruto = 0;
            this.VolumeLiquido = 0;
            this.PesoBruto = 0;
            this.PesoLiquido = 0;
            this.Imagem = new byte[0];
            this.Formula = "";
            this.Observacao = "";
            this.Produto = Prod;
            this.FichaTecnicaPCPItens = new List<FichaTecnicaPCPItem>();
            this.FichaTecnicaPCPRegras = new List<FichaTecnicaPCPRegra>();
        }


        #region ICloneable Members

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion


        public static FichaTecnicaPCP CloneNewObject(FichaTecnicaPCP objEstoque)
        {
            AutoMapper.Mapper.CreateMap<FichaTecnicaPCP, FichaTecnicaPCP>()
                    .ForMember(dest => dest.ID, o => o.Ignore());

            return AutoMapper.Mapper.Map<FichaTecnicaPCP, FichaTecnicaPCP>(objEstoque);
        }

        public static void GetClones(ref Produto objProduto)
        {
            IList<FichaTecnicaPCP> lstFichaTecnicaPCP = new List<FichaTecnicaPCP>(objProduto.FichaTecnicaPCP);
            objProduto.FichaTecnicaPCP = new List<FichaTecnicaPCP>();
            foreach (var FichaTecnicaPCP in lstFichaTecnicaPCP)
            {
                FichaTecnicaPCP Item = FichaTecnicaPCP.CloneNewObject(FichaTecnicaPCP);
                Item.Produto = objProduto;
                objProduto.FichaTecnicaPCP.Add(Item);
            }
        }
    }


}
