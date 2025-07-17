using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using cwkGestao.Repositorio;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Repositorio.Interfaces;

namespace cwkGestao.Integracao.SHL
{
    public static class Util
    {
        private static IRepositorio<Configuracao> iRepositorioConf;
        private static IRepositorio<SHLConfiguracao> iRepositorioShl;
        private static IRepositorioConfiguracao repositorioConfiguracao;
        private static IRepositorioSHLConfiguracao repositorioSHLConfiguracao;

        private static void InitRepositorios()
        {
            iRepositorioConf = RepositorioFactory<Configuracao>.GetRepositorio();
            repositorioConfiguracao = (IRepositorioConfiguracao)iRepositorioConf;
            iRepositorioShl = RepositorioFactory<SHLConfiguracao>.GetRepositorio();
            repositorioSHLConfiguracao = (IRepositorioSHLConfiguracao)iRepositorioShl;
        }

        public static string CadastraClassificacao(string chave, int interfaceShl, string descricaoClassificacao)
        {
            InitRepositorios();
            var config = repositorioConfiguracao.GetAll().FirstOrDefault();
            var shlconfig = repositorioSHLConfiguracao.GetAll();
            if (config.ServidorSHL == 0)
	        {
                chave = shlconfig.FirstOrDefault().ChaveSHLHomologacao;
                return UtilHomologacao.CadastraClassificacao(chave, interfaceShl, descricaoClassificacao);
	        }
            else
            {
                return UtilProducao.CadastraClassificacao(chave, interfaceShl, descricaoClassificacao);
            }
        }

        public static string EnviarCancelamentoPedidoSHL(string chave, int interfaceShl, string origem)
        {
            InitRepositorios();
            var config = repositorioConfiguracao.GetAll().FirstOrDefault();
            var shlconfig = repositorioSHLConfiguracao.GetAll();
            if (config.ServidorSHL == 0)
            {
                chave = shlconfig.FirstOrDefault().ChaveSHLHomologacao;
                return UtilHomologacao.EnviarCancelamentoPedidoSHL(chave, interfaceShl, origem);
            }
            else
            {
                return UtilProducao.EnviarCancelamentoPedidoSHL(chave, interfaceShl, origem);
            }
        }

        public static string EnviarXMLPedidoSHL(string chave, int interfaceShl, string xml, string codPedido, int codNota)
        {
            InitRepositorios();
            var config = repositorioConfiguracao.GetAll().FirstOrDefault();
            var shlconfig = repositorioSHLConfiguracao.GetAll();
            if (config.ServidorSHL == 0)
            {
                chave = shlconfig.FirstOrDefault().ChaveSHLHomologacao;
                return UtilHomologacao.EnviarXMLPedidoSHL(chave, interfaceShl, xml, codPedido, codNota);
            }
            else
            {
                return UtilProducao.EnviarXMLPedidoSHL(chave, interfaceShl, xml, codPedido, codNota);
            }
        }

        public static string CadastraGrupoCampanhas(string chave, int interfaceShl, string descricao)
        {
            InitRepositorios();
            var config = repositorioConfiguracao.GetAll().FirstOrDefault();
            var shlconfig = repositorioSHLConfiguracao.GetAll();
            if (config.ServidorSHL == 0)
            {
                chave = shlconfig.FirstOrDefault().ChaveSHLHomologacao;
                return UtilHomologacao.CadastraGrupoCampanhas(chave, interfaceShl, descricao);
            }
            else
            {
                return UtilProducao.CadastraGrupoCampanhas(chave, interfaceShl, descricao);
            }
        }

        public static string CadastraFornecedor(string chave, int interfaceShl, string nomeFantasia, string razaoSocial, string endereco, string numeroEndereco,
            string bairro, string complementoEndereco, string cep, string cidade, string Uf, string Pais, string Telefone, string Cnpj, string Ie)
        {
            InitRepositorios();
            var config = repositorioConfiguracao.GetAll().FirstOrDefault();
            var shlconfig = repositorioSHLConfiguracao.GetAll();
            if (config.ServidorSHL == 0)
            {
                chave = shlconfig.FirstOrDefault().ChaveSHLHomologacao;
                return UtilHomologacao.CadastraFornecedor(chave, interfaceShl, nomeFantasia, razaoSocial, endereco, numeroEndereco,
            bairro, complementoEndereco, cep, cidade, Uf, Pais, Telefone, Cnpj, Ie);
            }
            else
            {
                return UtilProducao.CadastraFornecedor(chave, interfaceShl, nomeFantasia, razaoSocial, endereco, numeroEndereco,
            bairro, complementoEndereco, cep, cidade, Uf, Pais, Telefone, Cnpj, Ie);
            }
        }

        public static string CadastraPedido(string chave, int interfaceShl, Dictionary<string, int> Produtos, bool AR, bool MP, decimal ValorDeclarado, int codTransportadora, string codDestino, int IDClassificacao,
           int IDTipoOrcamentoDistribuicao, string IDNumPedidoOrigem, string bairro, string cep, string cidade, string SiglaEstado, string endereco, string numero, string complemento,
           string telefone, string cpf, string cnpj, string email, string nomeCliente, string observacao, string pais, string razao_social, string StatusPedido)
        {
            InitRepositorios();
            var config = repositorioConfiguracao.GetAll().FirstOrDefault();
            var shlconfig = repositorioSHLConfiguracao.GetAll();
            if (config.ServidorSHL == 0)
            {
                chave = shlconfig.FirstOrDefault().ChaveSHLHomologacao;
                return UtilHomologacao.CadastraPedido(chave, interfaceShl, Produtos, AR, MP, ValorDeclarado, codTransportadora, codDestino, IDClassificacao,
            IDTipoOrcamentoDistribuicao, IDNumPedidoOrigem, bairro, cep, cidade, SiglaEstado, endereco, numero, complemento,
            telefone, cpf, cnpj, email, nomeCliente, observacao, pais, razao_social, StatusPedido);
            }
            else
            {
                return UtilProducao.CadastraPedido( chave,  interfaceShl, Produtos,  AR,  MP,  ValorDeclarado,  codTransportadora,  codDestino,  IDClassificacao,
            IDTipoOrcamentoDistribuicao,  IDNumPedidoOrigem,  bairro,  cep,  cidade,  SiglaEstado,  endereco,  numero,  complemento,
            telefone,  cpf,  cnpj,  email,  nomeCliente,  observacao,  pais,  razao_social,  StatusPedido);
            }
        }

        public static string CadastraTipoOrcamentoDistribuicao(string chave, int interfaceShl, string descricao)
        {
            InitRepositorios();
            var config = repositorioConfiguracao.GetAll().FirstOrDefault();
            var shlconfig = repositorioSHLConfiguracao.GetAll();
            if (config.ServidorSHL == 0)
            {
                chave = shlconfig.FirstOrDefault().ChaveSHLHomologacao;
                return UtilHomologacao.CadastraTipoOrcamentoDistribuicao( chave,  interfaceShl,  descricao);
            }
            else
            {
                return UtilProducao.CadastraTipoOrcamentoDistribuicao(chave, interfaceShl, descricao);
            }  
        }

        public static string CadastraProduto(string chave, int interfaceShl,
            string codProduto, int codGrupoCampanha, int largura, int altura,
            int comprimento, int peso, string descricaoMaterial)
        {
            InitRepositorios();
            var config = repositorioConfiguracao.GetAll().FirstOrDefault();
            var shlconfig = repositorioSHLConfiguracao.GetAll();
            if (config.ServidorSHL == 0)
            {
                chave = shlconfig.FirstOrDefault().ChaveSHLHomologacao;
                return UtilHomologacao.CadastraProduto( chave,  interfaceShl, codProduto,  codGrupoCampanha,  largura,  altura, comprimento,  peso,  descricaoMaterial);
            }
            else
            {
                return UtilProducao.CadastraProduto(chave, interfaceShl, codProduto, codGrupoCampanha, largura, altura, comprimento, peso, descricaoMaterial);
            }  
        }

        public static Dictionary<string, DateTime> GetCodigosRastreamentoSHL(string chaveSHL, int interfaceSHL, int CodPedidoGestao)
        {
            InitRepositorios();
            var config = repositorioConfiguracao.GetAll().FirstOrDefault();
            var shlconfig = repositorioSHLConfiguracao.GetAll();
            if (config.ServidorSHL == 0)
            {
                chaveSHL = shlconfig.FirstOrDefault().ChaveSHLHomologacao;
                return UtilHomologacao.GetCodigosRastreamentoSHL(chaveSHL, interfaceSHL, CodPedidoGestao);
            }
            else
            {
                return UtilProducao.GetCodigosRastreamentoSHL(chaveSHL, interfaceSHL, CodPedidoGestao);
            }
        }

        private static SHLWsHomologacao.WebserviceHOMOLOGAÇÃOSHL wsh = new SHLWsHomologacao.WebserviceHOMOLOGAÇÃOSHL();
        private static SHLWs.WebserviceINLOCUSSHL wsp = new SHLWs.WebserviceINLOCUSSHL();

        public static SHLWsHomologacao.gStatusPedidoArray GetStatusPedidoHom(string chaveSHL, int interfaceSHL, int CodPedidoGestao)
        {
            InitRepositorios();
            var shlconf = repositorioSHLConfiguracao.GetAll().FirstOrDefault();
            SHLWsHomologacao.gStatusPedidoArray status = null;
            try
            {
                status = wsh.getStatusPedido(new cwkGestao.Integracao.SHL.SHLWsHomologacao.gStatusPedidoParam()
                {
                    ambiente = "slomp2",
                    chave = shlconf.ChaveSHLHomologacao,
                    @interface = interfaceSHL,
                    destino = new cwkGestao.Integracao.SHL.SHLWsHomologacao.gStatusPedidoDestino()
                    {
                        numero_pedido_origem = CodPedidoGestao.ToString()
                    }
                }).retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return status;
        }

        public static SHLWs.gStatusPedidoArray GetStatusPedido(string chaveSHL, int interfaceSHL, int CodPedidoGestao)
        {
            InitRepositorios();
            SHLWs.gStatusPedidoArray status = null;
            try
            {
                status = wsp.getStatusPedido(new cwkGestao.Integracao.SHL.SHLWs.gStatusPedidoParam()
                {
                    ambiente = "slomp3",
                    chave = chaveSHL,
                    @interface = interfaceSHL,
                    destino = new cwkGestao.Integracao.SHL.SHLWs.gStatusPedidoDestino()
                    {
                        numero_pedido_origem = CodPedidoGestao.ToString()
                    }
                }).retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return status;
        }

        public static string CadastraServicoTransporte(string chaveSHL, int interfaceSHL, string CnpjFornecedor, string CodServico, string DescricaoServico, string CodContrato)
        {
            InitRepositorios();
            var config = repositorioConfiguracao.GetAll().FirstOrDefault();
            var shlconfig = repositorioSHLConfiguracao.GetAll();
            if (config.ServidorSHL == 0)
            {
                chaveSHL = shlconfig.FirstOrDefault().ChaveSHLHomologacao;
                return UtilHomologacao.CadastraServicoTransporte(chaveSHL, interfaceSHL, CnpjFornecedor, CodServico, DescricaoServico, CodContrato);
            }
            else
            {
                return UtilProducao.CadastraServicoTransporte(chaveSHL, interfaceSHL, CnpjFornecedor, CodServico, DescricaoServico, CodContrato);
            }
        }
    }
}
