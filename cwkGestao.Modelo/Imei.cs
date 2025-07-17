using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Imei")]
    public class Imei : ModeloBase
	{
        public Imei(Produto produto)
        {
            this.Produto = produto;
        }

        public Imei()
        {
        }

        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 80)]
		public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "IMEI", 30)]
        public virtual String CodigoIMEI { get; set; }

        [InformacaoExibicao(3, "Produto", 270)]
        public virtual Produto Produto { get; set; }

        private NotaItem notaItemEntrada;
        public virtual NotaItem NotaItemEntrada
        {
            get
            {
                notaEntrada = AtribuiNotaEntrada();
                return notaItemEntrada;
            }
            set
            {
                notaItemEntrada = value;
                notaEntrada = AtribuiNotaEntrada();
            }
        }

        private NotaItem notaItemSaida;
        public virtual NotaItem NotaItemSaida
        {
            get
            {
                notaSaida = AtribuiNotaSaida();
                return notaItemSaida;
            }
            set
            {
                notaItemSaida = value;
                notaSaida = AtribuiNotaSaida();
            }
        }

        private Nota notaEntrada;
        public virtual Nota NotaEntrada
        {
            get
            {
                DataNotaEntrada = AtribuiDataNotaEntrada();
                NumNotaEntrada = AtribuiNumNotaEntrada();
                return notaEntrada;
            }
        }

        private Nota notaSaida;
        public virtual Nota NotaSaida
        {
            get
            {
                DataNotaSaida = AtribuiDataNotaSaida();
                NumNotaSaida = AtribuiNumNotaSaida();
                return notaSaida;
            }
        }

        [InformacaoExibicao(4, "Dt. Nota Entrada", 80)]
        public virtual DateTime? DataNotaEntrada { get; private set; }

        [InformacaoExibicao(5, "Dt. Nota Saída", 80)]
        public virtual DateTime? DataNotaSaida { get; private set; }

        [InformacaoExibicao(4, "Nota Entrada", 80)]
        public virtual Int32? NumNotaEntrada { get; private set; }

        [InformacaoExibicao(5, "Nota Saída", 80)]
        public virtual Int32? NumNotaSaida { get; private set; }
        
        public virtual Boolean BAtivo 
        {
            get
            {
                return IsNullOrEmpty(notaItemSaida); 
            }
            set { }
        }

        private static bool IsNullOrEmpty(NotaItem notaItem)
        {
            return (notaItem == null) || (notaItem == new NotaItem());
        }

        private Nota AtribuiNotaEntrada()
        {
            Nota notaEntrada = notaItemEntrada == null ? null : notaItemEntrada.Nota;
            return notaEntrada;
        }

        private Nota AtribuiNotaSaida()
        {
            Nota notaSaida = notaItemSaida == null ? null : notaItemSaida.Nota;
            return notaSaida;
        }

        private DateTime? AtribuiDataNotaEntrada()
        {
            return notaEntrada == null ? new Nullable<DateTime>() : notaEntrada.Dt;
        }

        private DateTime? AtribuiDataNotaSaida()
        {
            return notaSaida == null ? new Nullable<DateTime>() : notaSaida.Dt;
        }

        private int? AtribuiNumNotaEntrada()
        {
            return notaEntrada == null ? new Nullable<int>() : notaEntrada.Numero;
        }

        private int? AtribuiNumNotaSaida()
        {
            return notaSaida == null ? new Nullable<int>() : notaSaida.Numero;
        }
        public virtual bool Selecionado { get; set; }


        public static Imei CloneNewObject(Imei objEstoque)
        {
            AutoMapper.Mapper.CreateMap<Imei, Imei>()
                    .ForMember(dest => dest.ID, o => o.Ignore());

            return AutoMapper.Mapper.Map<Imei, Imei>(objEstoque);
        }

        public static void GetClones(ref Produto objProduto)
        {
            IList<Imei> lstImei = new List<Imei>(objProduto.Imeis);
            objProduto.Imeis = new List<Imei>();
            foreach (var Imei in lstImei)
            {
                Imei Item = Imei.CloneNewObject(Imei);
                Item.Produto = objProduto;
                objProduto.Imeis.Add(Item);
            }
        }
    }
}
