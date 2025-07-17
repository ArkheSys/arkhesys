using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Carta de Correção")]
    public class CartaCorrecao : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private Nota _Nota;
        public virtual Nota Nota { get { return _Nota; } set { _Nota = value; } }

        private int _Sequencia;
        [InformacaoExibicao(1, "Sequencia", 100)]
        public virtual int Sequencia { get { return _Sequencia; } set { _Sequencia = value; } }

        private DateTime _Data;
        [InformacaoExibicao(2, "Data", 100, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual DateTime Data { get { return _Data; } set { _Data = value; } }

        private string _TextoCorrecao;
        public virtual string TextoCorrecao { get { return _TextoCorrecao; } set { _TextoCorrecao = value; } }

        [InformacaoExibicao(3, "Correção", 250, InformacaoExibicao.HAlinhamento.Esquerda)]
        public virtual string TextoCorrecaoParte { get { return TextoCorrecao.Length > 100 ? TextoCorrecao.Substring(0, 99) : TextoCorrecao; } }

        private string _LogEnvio;
        public virtual string LogEnvio { get { return _LogEnvio; } set { _LogEnvio = value; } }

        private string _LogRecibo;
        public virtual string LogRecibo { get { return _LogRecibo; } set { _LogRecibo = value; } }

        private string _XmlLogEnvNFe;
        public virtual string XmlLogEnvNFe { get { return _XmlLogEnvNFe; } set { _XmlLogEnvNFe = value; } }

        private string _XmlLogRecNFe;
        public virtual string XmlLogRecNFe { get { return _XmlLogRecNFe; } set { _XmlLogRecNFe = value; } }

        private string _XmlDestinatarioNFe;
        public virtual string XmlDestinatarioNFe { get { return _XmlDestinatarioNFe; } set { _XmlDestinatarioNFe = value; } }

        private string _UltimoXmlRecebido;
        public virtual string UltimoXmlRecebido { get { return _UltimoXmlRecebido; } set { _UltimoXmlRecebido = value; } }

        private DateTime _DtEnvio;
        public virtual DateTime DtEnvio { get { return _DtEnvio; } set { _DtEnvio = value; } }

        private string _Status;
        public virtual string Status { get { return _Status; } set { _Status = value; } }

        private string _cStat;
        public virtual string cStat { get { return _cStat; } set { _cStat = value; } }

        private string _Protocolo;
        public virtual string Protocolo { get { return _Protocolo; } set { _Protocolo = value; } }

        [InformacaoExibicao(4, "Status", 100, InformacaoExibicao.HAlinhamento.Esquerda)]
        public virtual string StatusLegivel
        {
            get
            {
                switch (this.Status)
                {
                    case "-1":
                        return "Não enviada";
                    case "0":
                        return "Negada";
                    case "1":
                        return "Aguardando";
                    case "2":
                        return "Autorizada";
                    case "3":
                        return "Cancelada";
                    case "4":
                        return "Denegada";
                    default:
                        return "";

                }

            }
        }

        public static CartaCorrecao CloneNewObject(CartaCorrecao objCartaCorrecao)
        {
            AutoMapper.Mapper.CreateMap<Modelo.CartaCorrecao, Modelo.CartaCorrecao>().ForMember(dest => dest.ID, o => o.Ignore());
            return AutoMapper.Mapper.Map<CartaCorrecao, CartaCorrecao>(objCartaCorrecao);
        }

        public static void GetClones(ref Nota objNota)
        {
            IList<CartaCorrecao> lstCartaCorrecao = new List<CartaCorrecao>(objNota.Cartas);
            objNota.Cartas = new List<CartaCorrecao>();
            foreach (var cartaCorrecao in lstCartaCorrecao)
            {
                CartaCorrecao objCartaCorrecao = CartaCorrecao.CloneNewObject(cartaCorrecao);
                objCartaCorrecao.Nota = objNota;

                objNota.Cartas.Add(objCartaCorrecao);
            }
        }
    }
}