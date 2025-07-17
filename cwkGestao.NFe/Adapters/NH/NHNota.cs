using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkNotaFiscalEletronica.Interfaces;
using cwkGestao.Modelo.Util;

namespace cwkGestao.NFe.Adapters.NH
{
    internal class NHNota : AdapterNota
    {
        cwkGestao.Modelo.Nota nota;

        private IList<INotaItem> notaItems;
        private IList<INotaParcela> notaParcelas;
        private IList<INotaReferencia> notaReferencias;

        public NHNota(cwkGestao.Modelo.Nota _nota)
            : base()
        {
            nota = _nota;

            notaItems = new List<INotaItem>();
            foreach (NotaItem notaItem in _nota.NotaItems)
            {
                notaItems.Add(new NHNotaItem(notaItem));
            }

            notaParcelas = new List<INotaParcela>();
            foreach (NotaParcela notaParcela in _nota.NotaParcelas)
            {
                notaParcelas.Add(new NHNotaParcela(notaParcela));
            }

            notaReferencias = new List<INotaReferencia>();
            foreach (NotaReferencia notaReferencia in _nota.NotaReferencias)
            {
                notaReferencias.Add(new NHNotaReferencia(notaReferencia));
            }

        }

        public override DateTime? CancDt
        {
            get
            {
                return nota.CancDt;
            }
            set
            {
                nota.CancDt = value;
            }
        }

        public override string CancMotivo
        {
            get
            {
                return nota.CancMotivo;
            }
            set
            {
                nota.CancMotivo = value;
            }
        }

        public override string CancUsuario
        {
            get
            {
                return nota.CancUsuario;
            }
            set
            {
                nota.CancUsuario = value;
            }
        }

        public override string ChaveNota
        {
            get
            {
                return nota.ChaveNota;
            }
            set
            {
                nota.ChaveNota = value;
            }
        }

        public override cwkNotaFiscalEletronica.Interfaces.ICliente Cliente
        {
            get 
            {
                return AdapterCliente.Produce(nota.Pessoa); 
            }
        }

        public override DateTime DtDigitacao
        {
            get { return nota.DtDigitacao.Value; }
        }

        public override cwkNotaFiscalEletronica.Interfaces.IEmpresa Empresa
        {
            get { return AdapterEmpresa.Produce(nota.Filial); }
        }

        public override int Ent_Sai
        {
            get { return (int)nota.Ent_Sai; }
        }

        public override int Id
        {
            get { return nota.ID; }
        }

        public override string LogEnvio
        {
            get
            {
                return nota.LogEnvio;
            }
            set
            {
                nota.LogEnvio = value;
            }
        }

        public override string LogRecibo
        {
            get
            {
                return nota.LogRecibo;
            }
            set
            {
                nota.LogRecibo = value;
            }
        }

        public override int ModeloDocto
        {
            get
            {
                return nota.ModeloDocto;
            }
            set
            {
                nota.ModeloDocto = value;
            }
        }

        public override IList<cwkNotaFiscalEletronica.Interfaces.INotaItem> NotaItems
        {
            get { return notaItems; }
        }

        public override IList<cwkNotaFiscalEletronica.Interfaces.INotaParcela> NotaParcelas
        {
            get
            {
                //if (nota.BGeraFinanceiro)
                    return notaParcelas;
                //else
                //{
                //    return new List<INotaParcela>();
                //}
            }
        }

        public override IList<cwkNotaFiscalEletronica.Interfaces.INotaReferencia> NotaReferencias
        {
            get
            {
                return notaReferencias;
            }
        }

        public override string Numeracao
        {
            get { return nota.VolumeNumero; }
        }

        public override int Numero
        {
            get { return nota.Numero; }
        }

        public override string NumeroProtocolo
        {
            get
            {
                return nota.NumeroProtocolo;
            }
            set
            {
                nota.NumeroProtocolo = value;
            }
        }

        public override string NumeroRecibo
        {
            get
            {
                return nota.NumeroRecibo;
            }
            set
            {
                nota.NumeroRecibo = value;
            }
        }

        public override string Observacao1
        {
            get { return nota.Observacao1; }
        }

        public override string Observacao2
        {
            get { return nota.Observacao2; }
        }

        public override string Observacao3
        {
            get { return nota.Observacao3; }
        }

        public override string Observacao4
        {
            get { return nota.Observacao4; }
        }

        public override string Observacao5
        {
            get { return nota.Observacao5; }
        }

        public override string ObservacaoSistema
        {
            get { return nota.ObservacaoSistema.RemoverEspacosDuplosEQuebras(); }
        }

        public override string ObservacaoUsuario
        {
            get { return nota.ObservacaoUsuario.RemoverEspacosDuplosEQuebras(); }
        }

        public override decimal PercDesconto
        {
            get { return nota.PercDesconto; }
        }

        public override string PessoaBairro
        {
            get { return nota.PessoaBairro; }
        }

        public override string PessoaCEP
        {
            get { return nota.PessoaCEP; }
        }

        public override string PessoaCNPJCPF
        {
            get { return nota.PessoaCNPJCPF; }
        }

        public override string PessoaCidade
        {
            get { return nota.PessoaCidade; }
        }

        public override string PessoaCidadeIBGE
        {
            get { return nota.PessoaCidadeIBGE; }
        }

        public override string PessoaEmail
        {
            get
            {
                try
                {
                    return nota.Pessoa.PrimeiroEmail(i => i.BNFe);
                }
                catch
                {
                    return "";
                }
            }
        }
        public override string Complemento
        {
            get { return nota.Complemento; }
        }

        public override string PessoaEndereco
        {
            get { return nota.PessoaEndereco; }
        }

        public override string PessoaInscRG
        {
            get { return nota.PessoaInscRG; }
        }

        public override string PessoaNome
        {
            get { return nota.PessoaNome; }
        }

        public override string PessoaNumero
        {
            get { return nota.PessoaNumero; }
        }

        public override string PessoaSUFRAMA
        {
            get { return nota.Pessoa.InscSuframa; }
        }

        public override string PessoaTelefone
        {
            get { return nota.PessoaTelefone; }
        }

        public override string PessoaUF
        {
            get { return nota.PessoaUF; }
        }

        public override string Serie
        {
            get { return nota.Serie; }
        }

        public override string Status
        {
            get
            {
                return nota.Status;
            }
            set
            {
                nota.Status = value;
            }
        }

        public override string StatusMotivo
        {
            get
            {
                return nota.StatusMotivo;
            }
            set
            {
                nota.StatusMotivo = value;
            }
        }

        public override string TipoFrete
        {
            get
            {
                switch (nota.TipoFrete)
                {
                    case 0: return "0";
                    case 1: return "1";
                    case 2: return "2";
                    case 3: return "3";
                    case 4: return "4";
                    case 9: return "9";
                    default: return "0";
                }
            }
            set
            {
                switch (value)
                {
                    case "0": nota.TipoFrete = 0;
                        break;
                    case "1": nota.TipoFrete = 1;
                        break;
                    case "2": nota.TipoFrete = 2;
                        break;
                    case "3": nota.TipoFrete = 3;
                        break;
                    case "4": nota.TipoFrete = 4;
                        break;
                    case "9": nota.TipoFrete = 9;
                        break;
                    default: break;

                }
            }
        }

        public override cwkNotaFiscalEletronica.TipoNotaEntSaida TipoNota
        {
            get
            {
                if (nota.Ent_Sai == InOutType.Entrada)
                    return cwkNotaFiscalEletronica.TipoNotaEntSaida.Entrada;
                else
                    return cwkNotaFiscalEletronica.TipoNotaEntSaida.Saida;
            }
        }

        public override decimal TotalIpi
        {//campo utilizado somente na apresentação da nota no NFe
            get { return 0; }
        }

        public override decimal TotalNota
        {
            get { return nota.TotalNota; }
        }

        public override decimal TotalProduto
        {
            get { return nota.TotalProduto; }
        }

        public override string TransCNPJCPF
        {
            get { return nota.TransCNPJCPF; }
        }

        public override string TransCidade
        {
            get { return nota.TransCidade; }
        }

        public override string TransEndereco
        {
            get { return nota.TransEndereco; }
        }

        public override string TransInscricao
        {
            get { return nota.PessoaTransportadora.Inscricao; }
        }

        public override string TransNome
        {
            get { return nota.TransNome; }
        }

        public override string TransPlaca
        {
            get { return nota.TransPlaca; }
        }

        public override string TransPlacaUF
        {
            get { return nota.TransPlacaUF; }
        }

        public override string TransUF
        {
            get { return nota.TransUF; }
        }

        public override string UltimoXmlRecebido
        {
            get
            {
                return nota.UltimoXmlRecebido;
            }
            set
            {
                nota.UltimoXmlRecebido = value;
            }
        }

        public override decimal ValorAcrescimo
        {
            get { return nota.ValorAcrescimo; }
        }

        public override decimal ValorDesconto
        {
            get { return nota.ValorDesconto; }
        }

        public override decimal ValorFrete
        {
            get { return nota.ValorFrete; }
        }

        public override string VolumeEspecie
        {
            get { return nota.VolumeEspecie; }
        }

        public override string VolumeMarca
        {
            get { return nota.VolumeMarca; }
        }

        public override string VolumeNumero
        {
            get { return nota.VolumeNumero; }
        }

        public override decimal VolumePesoBruto
        {
            get { return nota.VolumePesoBruto; }
        }

        public override decimal VolumePesoLiquido
        {
            get { return nota.VolumePesoLiquido; }
        }

        public override string VolumeQuant
        {
            get { return nota.VolumeQuant > 0 ? nota.VolumeQuant.ToString() : null; }
        }

        public override bool bImpressa
        {
            get { return nota.BImpressa; }
            set
            {
                nota.BImpressa = value;
            }
        }

        public override DateTime DtSaida
        {
            get { try { return nota.DtEnvio.Value; } catch (Exception exc) { throw new Exception("Data de saída nula ou inválida.", exc); } }
        }

        public override DateTime DtEmissao
        {
            get { try { return nota.Dt; } catch (Exception exc) { throw new Exception("Data da nota nula ou inválida.", exc); } }
        }

        public override DateTime Dt
        {
            get { try { return nota.Dt; } catch (Exception exc) { throw new Exception("Data da nota nula ou inválida.", exc); } }
        }

        public override IEnderecoEntrega EntregaEndreco
        {
            get { return nota.EnderecoEntrega != null ? AdapterEnderecoEntrega.Produce(nota.EnderecoEntrega) : null; }
        }

        public override INota NotaComplementada
        {
            get
            {
                if (nota.NotaComplementada == null)
                    return null;
                return new NHNota(nota.NotaComplementada);
            }
        }

        public override INota NotaReferenciada
        {
            get
            {
                if (nota.NotaReferenciada == null)
                    return null;

                return new NHNota(nota.NotaReferenciada);
            }
        }

        public override decimal W11_vII
        {
            get { return nota.W11_vII; }
        }

        public override string ZA02_UFEmbarq
        {
            get { return nota.ZA02_UFEmbarq; }
        }

        public override string ZA03_xLocEmbarq
        {
            get { return nota.ZA03_xLocEmbarq; }
        }

        public override string XmlDestinatarioNFe
        {
            get
            {
                return nota.XmlDestinatarioNFe;
            }
            set
            {
                nota.XmlDestinatarioNFe = value;
            }
        }

        public override string XmlLogEnvNFe
        {
            get
            {
                return nota.XmlLogEnvNFe;
            }
            set
            {
                nota.XmlLogEnvNFe = value;
            }
        }

        public override string XmlLogRecNFe
        {
            get
            {
                return nota.XmlLogRecNFe;
            }
            set
            {
                nota.XmlLogRecNFe = value;
            }
        }

        public override decimal OutrasDespesas
        {
            get { return nota.OutrasDespesas; }
        }

        public override decimal ValorSeguro
        {
            get { return nota.ValorSeguro; }
        }

        public override bool EnviaTagTotalImposto
        {
            get
            {
                return nota.EnviaTagTotalImposto;
            }
        }

        public override int idDest
        {
            get { return nota.idDest; }
        }

        public override DateTime? DtHoraAuto
        {
            get
            {
                return nota.DtHoraAuto;
            }
            set
            {
                nota.DtHoraAuto = value;
            }
        }

        public override bool BConsumidorFinal
        {
            get { return nota.Pessoa.bConsumidorFinal; }
        }
    }
}