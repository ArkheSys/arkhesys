using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Integracao.Correios;
using cwkGestao.Negocio;

namespace cwkGestao.Integracao.Correios
{
    public class CorreiosPrecoPrazoTotalHolder : ICloneable
    {
        public bool HolderSelecionado { get; set; }
        public IList<CorreiosPrecoPrazo> RequisicoesCorreios { get; set; }
        public string Servico { get; set; }
        public decimal PrecoTotal { get; set; }
        public decimal MaoPropria { get; set; }
        public decimal AvisoRecebimento { get; set; }
        public DateTime DtEntrega { get; set; }
        public DateTime DtPedido { get; set; }
        public string PrazoEntrega { get; set; }
        public decimal ValorDeclarado { get; set; }
        public decimal Margem { get; set; }
        public string Erro { get; set; }

        public CorreiosPrecoPrazoTotalHolder()
        {
            HolderSelecionado = false;
            RequisicoesCorreios = new List<CorreiosPrecoPrazo>();
            PrecoTotal = 0;
            MaoPropria = 0;
            AvisoRecebimento = 0;
            DtEntrega = DateTime.Now;
            ValorDeclarado = 0;
            Margem = 0;
            Erro = "";
            Servico = "";
        }

        public CorreiosPrecoPrazoTotalHolder(IList<CorreiosPrecoPrazo> lista)
        {
            HolderSelecionado = false;
            if (lista != null)
            {
                RequisicoesCorreios = lista;
                if (lista.Count > 0)
                {
                    if (lista.FirstOrDefault().TipoServicoTransporte != null)
                    {
                        TipoServicoTransporte svc = lista.FirstOrDefault().TipoServicoTransporte;
                        Servico = lista.FirstOrDefault().TipoServicoTransporte.CodigoServico + " - " +
                        lista.FirstOrDefault().TipoServicoTransporte.Descricao;
                    }

                    PrecoTotal = Math.Round((RequisicoesCorreios.Sum(s => s.Valor)), 2);
                    MaoPropria = Math.Round(RequisicoesCorreios.Sum(s => s.ValorMaoPropria), 2);
                    AvisoRecebimento = Math.Round(RequisicoesCorreios.Sum(s => s.ValorAvisoRecebimento), 2);
                    ValorDeclarado = Math.Round(RequisicoesCorreios.Sum(s => s.ValorValorDeclarado), 2);

                    DtEntrega = RequisicoesCorreios.Max(m => m.DtPrazoEntrega);
                    if (RequisicoesCorreios.FirstOrDefault().IDPedido == null)
                    {
                        DtPedido = DateTime.Now;
                    }
                    else
                    {
                        DtPedido = RequisicoesCorreios.FirstOrDefault().IDPedido.DtDigitacao == null ?
                            DateTime.Now : RequisicoesCorreios.FirstOrDefault().IDPedido.DtDigitacao;
                    }
                    TimeSpan periodo = DtEntrega - DtPedido;
                    PrazoEntrega = periodo.Days.ToString() + " dias";
                }
            }
        }

        public void SetTotais()
        {
            if (RequisicoesCorreios.Count == 0)
            {
                return;
            }
            CorreiosPrecoPrazo aux = RequisicoesCorreios.FirstOrDefault();
            if (aux.TipoServicoTransporte != null)
            {
                Margem = aux.TipoServicoTransporte.AcrescimoFrete ;
            }
            foreach (CorreiosPrecoPrazo item in RequisicoesCorreios)
            {
                if (item.ID == 0)
                {
                    item.Valor = Math.Round(item.Valor * (1 + (Margem / 100)), 2);
                }
                if (item.Erro != 0)
                {
                    Erro = aux.Erro.ToString() + " - " + aux.MsgErro;
                }
            }
            PrecoTotal = Math.Round((RequisicoesCorreios.Sum(s => s.Valor)), 2);
            MaoPropria = Math.Round(RequisicoesCorreios.Sum(s => s.ValorMaoPropria), 2);
            AvisoRecebimento = Math.Round(RequisicoesCorreios.Sum(s => s.ValorAvisoRecebimento), 2);
            ValorDeclarado = Math.Round(RequisicoesCorreios.Sum(s => s.ValorValorDeclarado), 2);
            
            DtEntrega = RequisicoesCorreios.Max(m => m.DtPrazoEntrega);
            if (RequisicoesCorreios.FirstOrDefault().IDPedido == null)
	        {
                DtPedido = DateTime.Now;
	        }
            else
            {
                DtPedido = RequisicoesCorreios.FirstOrDefault().IDPedido.DtDigitacao == null ? 
                    DateTime.Now : RequisicoesCorreios.FirstOrDefault().IDPedido.DtDigitacao;
            }
            TimeSpan periodo = DtEntrega - DtPedido;
            PrazoEntrega = periodo.Days.ToString() + " dias";
        }

        public IList<CorreiosPrecoPrazoTotalHolder> CalculaPrecosPrazos()
        {
            IList<CorreiosPrecoPrazoTotalHolder> ListaHolders = new List<CorreiosPrecoPrazoTotalHolder>();
            IList<TipoServicoTransporte> servicos = TipoServicoTransporteController.Instancia.GetAll().Where(w => w.IDServicoEnvioCorreio != null).ToList();
            foreach (TipoServicoTransporte servico in servicos)
            {
                string nomesvc = servico.IDServicoEnvioCorreio.ToString();
                CorreiosPrecoPrazoTotalHolder cppt = CalculaPrecosPrazosIndividual(servico);
                ListaHolders.Add(cppt);
            }
            return ListaHolders;
        }

        public CorreiosPrecoPrazoTotalHolder CalculaPrecosPrazosIndividual(TipoServicoTransporte servico)
        {
            string nomesvc = servico.IDServicoEnvioCorreio.ToString();
            CorreiosPrecoPrazoTotalHolder cppt = new CorreiosPrecoPrazoTotalHolder();
            foreach (CorreiosPrecoPrazo item in RequisicoesCorreios)
            {
                if (item.FormatoPacoteTransporte != null)
                {
                    CorreiosPrecoPrazo clone = (CorreiosPrecoPrazo)item.Clone();
                    clone.NCdFormato = item.FormatoPacoteTransporte.Codigo;
                    clone.NCdServico = servico.IDServicoEnvioCorreio.Codigo;
                    clone.TipoServicoTransporte = servico;
                    try
                    {
                        cppt.RequisicoesCorreios.Add(IntegracaoWsCorreios.CalculoPrecoPrazo(clone));
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                else
                {
                    cppt.RequisicoesCorreios.Add(item);
                }

            }
            try
            {
                cppt.SetTotais();
                cppt.Servico = nomesvc;
            }
            catch (Exception)
            {

                throw;
            }
            return cppt;
        }

        public static IList<CorreiosPrecoPrazoTotalHolder> GetHoldersPorPedido(Pedido p)
        {
            IList<CorreiosPrecoPrazoTotalHolder> Orcamentos = new List<CorreiosPrecoPrazoTotalHolder>();
            if (p == null)
            {
                return Orcamentos;
            }
            if (p.ID > 0)
            {
                try
                {
                    IList<CorreiosPrecoPrazo> temp = CorreiosPrecoPrazoController.Instancia.GetPorIdPedido(p.ID);
                    Dictionary<string, IList<CorreiosPrecoPrazo>> separador = new Dictionary<string, IList<CorreiosPrecoPrazo>>();
                    foreach (CorreiosPrecoPrazo item in temp)
                    {
                        if (item.TipoServicoTransporte.IDServicoEnvioCorreio != null)
                        {
                            string cod = item.TipoServicoTransporte.IDServicoEnvioCorreio.Codigo.Trim();
                            if (separador.ContainsKey(cod))
                            {
                                separador[cod].Add(item);
                            }
                            else
                            {
                                IList<CorreiosPrecoPrazo> aux = new List<CorreiosPrecoPrazo>();
                                aux.Add(item);
                                separador.Add(cod, aux);
                            }
                        }
                    }
                    foreach (string item in separador.Keys)
                    {
                        Orcamentos.Add(new CorreiosPrecoPrazoTotalHolder(separador[item]));
                    }
                    foreach (CorreiosPrecoPrazoTotalHolder item in Orcamentos)
                    {
                        if (item.RequisicoesCorreios.FirstOrDefault().bSelecionado == true)
                        {
                            item.HolderSelecionado = true;
                        }
                        else
                        {
                            item.HolderSelecionado = false;
                        }
                    }
                }
                catch (Exception v)
                {
                    throw v;
                }
            }
            return Orcamentos;
        }

        public static CorreiosPrecoPrazoTotalHolder SetCodigosRastreamento(CorreiosPrecoPrazoTotalHolder Cppt, TipoServicoTransporte tst)
        {
            TipoServicoTransporte tipo;
            if (tst.IDServicoEnvioCorreio != null && Cppt.RequisicoesCorreios.Count > 0)
            {
                if (tst.IDServicoEnvioCorreio.ID != Cppt.RequisicoesCorreios.
                    FirstOrDefault().TipoServicoTransporte.IDServicoEnvioCorreio.ID)
                {
                    foreach (CorreiosPrecoPrazo item in Cppt.RequisicoesCorreios)
                    {
                        item.TipoServicoTransporte = tst;
                        item.NCdServico = tst.IDServicoEnvioCorreio.Codigo;
                    }
                }
                tipo = tst;
            }
            else
            {
                tipo = Cppt.RequisicoesCorreios.Count > 0 ? Cppt.RequisicoesCorreios.FirstOrDefault().TipoServicoTransporte : tst;
            }
            Cppt.SetTotais();
            IList<RastreamentoCorreio> CodsRastreamento = RastreamentoCorreioController.Instancia.
                        GetCodigosDisponiveisPorTipoServico(tipo);
            int CPPSemRastreamento = 0;
            foreach (CorreiosPrecoPrazo item in Cppt.RequisicoesCorreios)
            {
                if (item.IDRastreamento == null)
                {
                    CPPSemRastreamento++;
                }
                else
                {
                    if (item.IDRastreamento.TipoServico.ID != tipo.ID)
                    {
                        CPPSemRastreamento++;
                    }
                }
            }
            if (CodsRastreamento.Count < CPPSemRastreamento)
            {
                throw new Exception("Não há códigos de rastreamento do serviço " + tipo.IDServicoEnvioCorreio.ToString() + 
                    " suficientes para os volumes informados." + "\r\n\r\n" + "Códigos sem rastreamento restantes: " + CPPSemRastreamento.ToString() +
                    "\r\n"+ "Códigos restantes: " + CodsRastreamento.Count.ToString());
            }
            else
            {
                int j = 0;
                for (int i = 0; i < Cppt.RequisicoesCorreios.Count; i++)
                {
                    if (Cppt.RequisicoesCorreios[i].IDRastreamento != null)
                    {
                        if (CodsRastreamento.Count > 0)
                        {
                            if (Cppt.RequisicoesCorreios[i].IDRastreamento.TipoServico.ID != CodsRastreamento[j].TipoServico.ID)
                            {
                                Cppt.RequisicoesCorreios[i].IDRastreamento = CodsRastreamento[j];
                                j++;
                            }
                        }
                    }
                    else
                    {
                        Cppt.RequisicoesCorreios[i].IDRastreamento = CodsRastreamento[j];
                        j++;
                    }
                    
                }
            }
            return Cppt;
        }

        #region ICloneable Members

        public object Clone()
        {
            CorreiosPrecoPrazoTotalHolder clone = new CorreiosPrecoPrazoTotalHolder();
            clone = this.MemberwiseClone() as CorreiosPrecoPrazoTotalHolder;
            return clone;
        }

        #endregion
    }
}
