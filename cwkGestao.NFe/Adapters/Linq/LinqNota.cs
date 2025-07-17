using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MO = Modelo;
using cwkNotaFiscalEletronica.Interfaces;

namespace cwkGestao.NFe.Adapters.Linq
{
    internal class LinqNota : AdapterNota
    {
        MO.Nota nota;
        IList<INotaItem> notaItens;
        IList<INotaParcela> notaParcelas;
        IList<INotaReferencia> notaReferencias;
        public LinqNota(MO.Nota _nota)
            : base()
        {
            nota = _nota;

            notaItens = new List<INotaItem>();
            foreach (var item in _nota.NotaItems)
            {
                notaItens.Add(AdapterNotaItem.Produce(item));
            }

            notaParcelas = new List<INotaParcela>();
            foreach (var item in _nota.NotaParcelas)
            {
                notaParcelas.Add(AdapterNotaParcela.Produce(item));
            }

            notaReferencias = new List<INotaReferencia>();
            foreach (var item in _nota.NotaReferencias)
            {
                notaReferencias.Add(AdapterNotaReferencia.Produce(item));
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
            get { return AdapterCliente.Produce(nota.Pessoa); }
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
                return nota.ModeloDocto.Value;
            }
            set
            {
                nota.ModeloDocto = value;
            }
        }

        public override IList<cwkNotaFiscalEletronica.Interfaces.INotaItem> NotaItems
        {
            get { return notaItens; }
        }

        public override IList<cwkNotaFiscalEletronica.Interfaces.INotaParcela> NotaParcelas
        {
            get
            {
                bool geraFinanceiro = nota.bGeraFinanceiro.HasValue ? nota.bGeraFinanceiro.Value == 1 : false;
                if (geraFinanceiro)
                    return notaParcelas;
                else
                {
                    return new List<INotaParcela>();
                }
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
            get { return nota.Numero.Value; }
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
            get { return nota.ObservacaoSistema; }
        }

        public override string ObservacaoUsuario
        {
            get { return nota.ObservacaoUsuario; }
        }

        public override decimal PercDesconto
        {
            get { return nota.PercDesconto.Value; }
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
                    return nota.Pessoa.PrimeiroEmailNFe();
                }
                catch
                {
                    return "";
                }
            }
        }

        public override string PessoaEndereco
        {
            get { return nota.PessoaEndereco; }
        }

        public override string Complemento
        {
            get { return nota.Complemento; }
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
            get { return ""; }
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
                if (nota.TipoFrete.HasValue)
                {
                    switch (nota.TipoFrete.Value)
                    {
                        case 0: return "0";
                        case 1: return "1";
                        case 2: return "2";
                        case 3: return "9";
                        default: return "0";
                    }
                }
                else
                    return "0";
            }
            set
            {
                TipoFrete = value.ToString();
            }
        }

        public override cwkNotaFiscalEletronica.TipoNotaEntSaida TipoNota
        {
            get
            {
                if (nota.Ent_Sai == global::Modelo.InOutType.Entrada)
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
            get { return nota.TotalNota.Value; }
        }

        public override decimal TotalProduto
        {
            get { return nota.TotalProduto.Value; }
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
            get { return nota.Transportadora.Inscricao; }
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
            get { return nota.ValorAcrescimo.Value; }
        }

        public override decimal ValorDesconto
        {
            get { return nota.ValorDesconto.Value; }
        }

        public override decimal ValorFrete
        {
            get { return nota.ValorFrete.Value; }
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
            get { return nota.VolumePesoBruto.Value; }
        }

        public override decimal VolumePesoLiquido
        {
            get { return nota.VolumePesoLiquido.Value; }
        }

        public override string VolumeQuant
        {
            get { return nota.VolumeQuant.Value > 0 ? nota.VolumeQuant.Value.ToString() : null; }
        }

        public override bool bImpressa
        {
            get
            {
                if (nota.bImpressa.HasValue)
                    return Convert.ToBoolean(nota.bImpressa.Value);
                else
                    return false;
            }
            set
            {
                nota.bImpressa = Convert.ToByte(value);
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
            get { return nota.Endereco != null ? AdapterEnderecoEntrega.Produce(nota.Endereco) : null; }
        }

        public override INota NotaComplementada
        {
            get
            {
                if (nota.NotaComplementada == null)
                    return null;
                return new LinqNota(nota.NotaComplementada);
            }
        }

        public override INota NotaReferenciada
        {
            get { throw new NotImplementedException(); }
        }

        public override decimal W11_vII
        {
            get { return nota.W11_vII == null ? 0 : (decimal)nota.W11_vII; }
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
                return nota.XmlDestinatarioNFe.ToString();
            }
            set
            {
                nota.XmlDestinatarioNFe = System.Xml.Linq.XElement.Parse(value);
            }
        }

        public override string XmlLogEnvNFe
        {
            get
            {
                return nota.XmlLogEnvNFe.ToString();
            }
            set
            {
                nota.XmlLogEnvNFe = System.Xml.Linq.XElement.Parse(value);
            }
        }

        public override string XmlLogRecNFe
        {
            get
            {
                return nota.XmlLogRecNFe.ToString();
            }
            set
            {
                nota.XmlLogRecNFe = System.Xml.Linq.XElement.Parse(value);
            }
        }

        public override decimal OutrasDespesas
        {
            get { return nota.OutrasDespesas == null ? 0 : (decimal)nota.OutrasDespesas; }
        }

        public override decimal ValorSeguro
        {
            get { return nota.ValorSeguro == null ? 0 : (decimal)nota.ValorSeguro; }
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
            get { throw new NotImplementedException(); }
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
            get { throw new NotImplementedException(); }
        }
    }
}