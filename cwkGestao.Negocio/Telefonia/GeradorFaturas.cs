using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using System.Collections;

namespace cwkGestao.Negocio.Telefonia
{
    public class GeradorFaturas
    {
        private readonly Tel_GrupoCliente grupoCliente;
        private readonly DateTime referencia;
        private readonly DateTime dataInicial;
        private readonly DateTime dataFinal;
        private readonly DateTime vencimento;
        private readonly Tel_Configuracao configuracao;
        private int codigoFaturaDisponivel;
        private IList<Tel_TipoLigacaoPreco> precosGerais;
        private IList<Tel_Cliente> clientes;
        private IList<Tel_ClienteServico> servicos;
        private IList<Documento> documentosAtrasados;
        private IList<Tel_Fatura> faturasExistentes;
        private IList<pxyTel_LigacaoCliente> ligacoesReferencia;
        private List<Tel_Fatura> faturasGeradas;

        public GeradorFaturas(Tel_GrupoCliente grupoCliente, string referencia, DateTime dataInicial, DateTime dataFinal, DateTime vencimento)
        {
            this.grupoCliente = grupoCliente;
            this.referencia = Convert.ToDateTime("01/" + referencia);
            this.dataInicial = dataInicial;
            this.dataFinal = dataFinal;
            this.vencimento = vencimento;
            this.configuracao = Tel_ConfiguracaoController.Instancia.GetConfiguracao();
        }

        public void GerarESalvarFaturas()
        {
            PrepararDados();
            GerarFaturaParaCadaCliente();
            SalvarFaturasGeradas();
        }

        private void SalvarFaturasGeradas()
        {
            Negocio.Tel_FaturaController.Instancia.RegisterNewIntoTransaction(faturasGeradas);
            Negocio.Tel_FaturaController.Instancia.CommitUnitOfWorkTransaction();
        }

        private void GerarFaturaParaCadaCliente()
        {
            codigoFaturaDisponivel = Tel_FaturaController.Instancia.MaxCodigo();
            foreach (var cliente in clientes)
            {
                var fatura = InicializarFatura(cliente);
                IncluirServicosFatura(fatura);
                IncluirJurosEMulta(fatura);
                IncluirLigacoesFatura(fatura);
                if (fatura.ID == 0 && fatura.Servicos.Count == 0 && fatura.Ligacoes.Count == 0)
                    continue;

                CalcularTotalFatura(fatura);
                faturasGeradas.Add(fatura);
            }
        }

        private void IncluirJurosEMulta(Tel_Fatura fatura)
        {
            var docsCliente = documentosAtrasados.Where(d => d.Pessoa.ID == fatura.Cliente.Pessoa.ID);
            foreach (var doc in docsCliente)
            {
                var valorJuros = CalcularJuros(doc);
                var valorMulta = CalcularMulta(doc);
                if (valorJuros > 0)
                {
                    var servicoJuros = GerarServicoJuros(fatura);
                    servicoJuros.Documento = doc;
                    servicoJuros.Total = servicoJuros.ValorUnitario += valorJuros;
                }
                if (valorMulta > 0)
                {
                    var servicoMulta = GerarServicoMulta(fatura);
                    servicoMulta.Documento = doc;
                    servicoMulta.Total = servicoMulta.ValorUnitario += CalcularMulta(doc);
                }
            }
        }

        private Tel_FaturaServico GerarServicoJuros(Tel_Fatura fatura)
        {
            var servicoJuros = fatura.Servicos.Where(s => s.Servico.ID == configuracao.ServicoJuros.ID).FirstOrDefault();
            if (servicoJuros == null)
            {
                servicoJuros = new Tel_FaturaServico();
                servicoJuros.Sequencia = fatura.Servicos.Count + 1;
                servicoJuros.Quantidade = 1;
                servicoJuros.Operacao = 1;
                servicoJuros.Servico = configuracao.ServicoJuros;
                servicoJuros.Fatura = fatura;
                fatura.Servicos.Add(servicoJuros);
            }
            return servicoJuros;
        }

        private Tel_FaturaServico GerarServicoMulta(Tel_Fatura fatura)
        {
            var servicoMulta = fatura.Servicos.Where(s => s.Servico.ID == configuracao.ServicoMulta.ID).FirstOrDefault();
            if (servicoMulta == null)
            {
                servicoMulta = new Tel_FaturaServico();
                servicoMulta.Sequencia = fatura.Servicos.Count + 1;
                servicoMulta.Quantidade = 1;
                servicoMulta.Operacao = 1;
                servicoMulta.Servico = configuracao.ServicoMulta;
                servicoMulta.Fatura = fatura;
                fatura.Servicos.Add(servicoMulta);
            }
            return servicoMulta;
        }

        private decimal CalcularMulta(Documento doc)
        {
            return AcrescimoController.Instancia.CalculaMulta(doc.Acrescimo, doc.ValorTotal, doc.Dt, doc.DtVencimento);
        }

        private decimal CalcularJuros(Documento doc)
        {
            return AcrescimoController.Instancia.CalculaJuros(doc.Acrescimo, doc.ValorTotal, doc.Dt, doc.DtVencimento);
        }

        private void CalcularTotalFatura(Tel_Fatura fatura)
        {
            fatura.Valor = fatura.Servicos.Sum(s => s.Total) + fatura.Ligacoes.Sum(l => l.Total);
        }

        private Tel_Fatura InicializarFatura(Tel_Cliente cliente)
        {
            Tel_Fatura fatura = faturasExistentes.Where(f => f.Cliente.ID == cliente.ID).FirstOrDefault();
            if (fatura == null)
            {
                fatura = new Tel_Fatura();
                fatura.Cliente = cliente;
                fatura.Codigo = codigoFaturaDisponivel++;
                fatura.Data = DateTime.Today.Date;
                fatura.Referencia = referencia;
                fatura.DataInicial = dataInicial;
                fatura.DataFinal = dataFinal;
                fatura.Vencimento = vencimento;
            }
            else
                fatura = Tel_FaturaController.Instancia.GetByClienteEReferencia(cliente, referencia);
            return fatura;
        }

        private void IncluirServicosFatura(Tel_Fatura fatura)
        {
            var servicosCliente = servicos.Where(s => s.Cliente.ID == fatura.Cliente.ID).ToList();
            foreach (var sc in servicosCliente)
            {
                var itemServico = fatura.Servicos.Where(s => s.Servico.ID == sc.Servico.ID).FirstOrDefault();
                if (itemServico == null)
                    itemServico = CriarItemServico(fatura, sc);
                itemServico.Total = itemServico.Quantidade * itemServico.ValorUnitario;
            }
        }

        private static Tel_FaturaServico CriarItemServico(Tel_Fatura fatura, Tel_ClienteServico sc)
        {
            var itemServico = new Tel_FaturaServico();
            itemServico.Sequencia = fatura.Servicos.Count + 1;
            itemServico.Quantidade = 1;
            itemServico.Operacao = sc.Operacao;
            itemServico.Servico = sc.Servico;
            itemServico.ValorUnitario = (sc.Operacao == 0 ? -1 : 1) * sc.Valor;
            itemServico.Fatura = fatura;
            fatura.Servicos.Add(itemServico);
            return itemServico;
        }

        private void IncluirLigacoesFatura(Tel_Fatura fatura)
        {
            var ligacoes = ligacoesReferencia.Where(l => l.IDCliente == fatura.Cliente.ID);

            foreach (var lig in ligacoes)
            {
                var itemLigacao = new Tel_FaturaLigacao();
                itemLigacao.Sequencia = fatura.Ligacoes.Count + 1;
                itemLigacao.Ligacao = new Tel_Ligacao { ID = lig.IDLigacao };
                itemLigacao.Quantidade = CalcularMinutos(lig.TempoSegundos);

                var precoBase = BuscarPrecoBase(fatura, lig);
                itemLigacao.ValorMinuto = precoBase.Valor;
                itemLigacao.TipoLigacao = precoBase.TipoLigacao;

                itemLigacao.Total = itemLigacao.Quantidade * itemLigacao.ValorMinuto;
                itemLigacao.Fatura = fatura;

                if (fatura.Cliente.ID == 3333)
                {

                    if (lig.TipoUso == 996 || lig.TipoUso == 998)
                    {
                    }

                    else
                    {
                        fatura.Ligacoes.Add(itemLigacao);
                    }
                }
                else
                {
                    fatura.Ligacoes.Add(itemLigacao);
                }
            }
        }

        private int CalcularMinutos(int segundos)
        {
            if (segundos <= 0)
                return 0;

            return Convert.ToInt32(Math.Ceiling(segundos / 60m));
        }

        private Tel_PrecoBase BuscarPrecoBase(Tel_Fatura fatura, pxyTel_LigacaoCliente ligacao)
        {
            var area = Convert.ToInt16(ligacao.Origem.Substring(0, 2));
            var prefixo = Convert.ToInt16(ligacao.Origem.Substring(2, 4));
            var range = Convert.ToInt16(ligacao.Origem.Substring(6, 4));

            var dest = string.IsNullOrEmpty(ligacao.Destino) ? "0" : ligacao.Destino.Substring(0, 2);
            int.TryParse(dest, out var areaDestino);

            var selecionador = new SelecionadorPrecos(ligacao.TipoUso, area, prefixo, range, (short)areaDestino);

            Tel_PrecoBase preco = selecionador.GetPreco(fatura.Cliente.Precos.Select(p => (Tel_PrecoBase)p).ToList());

            if (preco == null)
                preco = selecionador.GetPreco(grupoCliente.Precos.Select(p => (Tel_PrecoBase)p).ToList());

            if (preco == null)
                preco = selecionador.GetPreco(precosGerais.Select(p => (Tel_PrecoBase)p).ToList());

            if (preco == null)
                throw new Exception(String.Format("Não foi encontrado um preço para a seguinte ligação: Tipo Uso: {0} - Numero: {1}", ligacao.TipoUso, ligacao.Origem));

            return preco;
        }

        private void PrepararDados()
        {
            clientes = Negocio.Tel_ClienteController.Instancia.GetByGrupo(grupoCliente);
            servicos = Negocio.Tel_ClienteServicoController.Instancia.GetByGrupoDataLimiteEReferencia(grupoCliente, dataInicial, referencia);
            precosGerais = Negocio.Tel_TipoLigacaoPrecoController.Instancia.GetAll();
            documentosAtrasados = Tel_FaturaController.Instancia.GetDocumentosAtrasados(grupoCliente);
            faturasExistentes = Tel_FaturaController.Instancia.GetByGrupoEReferencia(grupoCliente, referencia);
            ligacoesReferencia = LigacaoController.Instancia.GetByReferencia(referencia);
            faturasGeradas = new List<Tel_Fatura>();
        }
    }
}
