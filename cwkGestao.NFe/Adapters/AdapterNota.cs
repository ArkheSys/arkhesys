using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkNotaFiscalEletronica.Interfaces;
using cwkGestao.NFe.Adapters.Linq;
using cwkGestao.NFe.Adapters.NH;
using MO = Modelo;

namespace cwkGestao.NFe.Adapters
{
    public abstract class AdapterNota : INota
    {
        protected AdapterNota() { }

        public static AdapterNota Produce(object nota)
        {
            if (nota.GetType() == typeof(MO.Nota))
            {
                return new LinqNota((MO.Nota)nota);
            }
            else if (nota.GetType() == typeof(cwkGestao.Modelo.Nota))
            {
                return new NHNota((cwkGestao.Modelo.Nota)nota);
            }
            throw new ArgumentException("Parâmetro passado não é do tipo Modelo.Nota ou cwkGestao.Modelo.Nota");
        }

        public abstract  DateTime? CancDt
        {
            get;
            set;
        }

        public abstract  string CancMotivo
        {
            get;
            set;
        }

        public abstract  string CancUsuario
        {
            get;
            set;
        }

        public abstract  string ChaveNota
        {
            get;
            set;
        }

        public abstract  ICliente Cliente
        {
            get;
        }

        public abstract  DateTime DtDigitacao
        {
            get;
        }

        public abstract  IEmpresa Empresa
        {
            get;
        }

        public abstract  int Ent_Sai
        {
            get;
        }

        public abstract  int Id
        {
            get;
        }

        public abstract  string LogEnvio
        {
            get;
            set;
        }

        public abstract  string LogRecibo
        {
            get;
            set;
        }

        public abstract  int ModeloDocto
        {
            get;
            set;
        }

        public abstract  IList<INotaItem> NotaItems
        {
            get;
        }

        public abstract  IList<INotaParcela> NotaParcelas
        {
            get;
        }

        public abstract IList<INotaReferencia> NotaReferencias
        {
            get;
        }

        public abstract  string Numeracao
        {
            get;
        }

        public abstract  int Numero
        {
            get;
        }

        public abstract  string NumeroProtocolo
        {
            get;
            set;
        }

        public abstract  string NumeroRecibo
        {
            get;
            set;
        }

        public abstract  string Observacao1
        {
            get;
        }

        public abstract  string Observacao2
        {
            get;
        }

        public abstract  string Observacao3
        {
            get;
        }

        public abstract  string Observacao4
        {
            get;
        }

        public abstract  string Observacao5
        {
            get;
        }

        public abstract  string ObservacaoSistema
        {
            get;
        }

        public abstract  string ObservacaoUsuario
        {
            get;
        }

        public abstract  decimal PercDesconto
        {
            get;
        }

        public abstract  string PessoaBairro
        {
            get;
        }

        public abstract  string PessoaCEP
        {
            get;
        }

        public abstract  string PessoaCNPJCPF
        {
            get;
        }

        public abstract  string PessoaCidade
        {
            get;
        }

        public abstract  string PessoaCidadeIBGE
        {
            get;
        }

        public abstract  string PessoaEmail
        {
            get;
        }

        public abstract  string PessoaEndereco
        {
            get;
        }

        public abstract string Complemento
        {
            get;
        }

        public abstract  string PessoaInscRG
        {
            get;
        }

        public abstract  string PessoaNome
        {
            get;
        }

        public abstract  string PessoaNumero
        {
            get;
        }

        public abstract  string PessoaSUFRAMA
        {
            get;
        }

        public abstract  string PessoaTelefone
        {
            get;
        }

        public abstract  string PessoaUF
        {
            get;
        }

        public abstract  string Serie
        {
            get;
        }

        public abstract  string Status
        {
            get;
            set;
        }

        public abstract  string StatusMotivo
        {
            get;
            set;
        }

        public abstract string TipoFrete
        {
            get;
            set;
        }

        public abstract  cwkNotaFiscalEletronica.TipoNotaEntSaida TipoNota
        {
            get;
        }

        public abstract  decimal TotalIpi
        {
            get;
        }

        public abstract  decimal TotalNota
        {
            get;
        }

        public abstract  decimal TotalProduto
        {
            get;
        }

        public abstract  string TransCNPJCPF
        {
            get;
        }

        public abstract  string TransCidade
        {
            get;
        }

        public abstract  string TransEndereco
        {
            get;
        }

        public abstract  string TransInscricao
        {
            get;
        }

        public abstract  string TransNome
        {
            get;
        }

        public abstract  string TransPlaca
        {
            get;
        }

        public abstract  string TransPlacaUF
        {
            get;
        }

        public abstract  string TransUF
        {
            get;
        }

        public abstract  string UltimoXmlRecebido
        {
            get;
            set;
        }

        public abstract  decimal ValorAcrescimo
        {
            get;
        }

        public abstract  decimal ValorDesconto
        {
            get;
        }

        public abstract  decimal ValorFrete
        {
            get;
        }

        public abstract  string VolumeEspecie
        {
            get;
        }

        public abstract  string VolumeMarca
        {
            get;
        }

        public abstract  string VolumeNumero
        {
            get;
        }

        public abstract  decimal VolumePesoBruto
        {
            get;
        }

        public abstract  decimal VolumePesoLiquido
        {
            get;
        }

        //public abstract  decimal VolumeQuant
        //{
        //    get;
        //}

        public abstract  bool bImpressa
        {
            get;
            set;
        }

        public abstract string VolumeQuant
        {
            get;
        }

        public abstract DateTime DtSaida
        {
            get;
        }

        public abstract DateTime DtEmissao
        {
            get;
        }

        public abstract DateTime Dt
        {
            get; 
        }

        public abstract IEnderecoEntrega EntregaEndreco { get; }

        public abstract INota NotaComplementada { get; }

        public abstract INota NotaReferenciada { get; }

        public abstract decimal W11_vII
        {
           get;
        }

        public abstract string ZA02_UFEmbarq
        {
           get;
        }

        public abstract string ZA03_xLocEmbarq
        {
           get;
        }

        public abstract string XmlDestinatarioNFe
        {
            get;
            set;
        }

        public abstract string XmlLogEnvNFe
        {
            get;
            set;
        }

        public abstract string XmlLogRecNFe
        {
            get;
            set;
        }

        public abstract bool EnviaTagTotalImposto
        {
            get;
        }

        public abstract int idDest
        {
            get;
        }

        #region INota Members


        public abstract decimal OutrasDespesas
        {
            get;
        }

        public abstract decimal ValorSeguro
        {
            get;
        }

        #endregion


        public abstract DateTime? DtHoraAuto
        {
            get;
            set;
        }

        public abstract bool BConsumidorFinal
        {
            get;
        }
    }
}
 