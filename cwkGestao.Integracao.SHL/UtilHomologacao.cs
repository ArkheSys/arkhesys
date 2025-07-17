using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using cwkGestao.Negocio;

namespace cwkGestao.Integracao.SHL
{
    public static class UtilHomologacao
    {
        private static SHLWsHomologacao.WebserviceHOMOLOGAÇÃOSHL ws = new SHLWsHomologacao.WebserviceHOMOLOGAÇÃOSHL();

        public static string CadastraClassificacao(string chave, int interfaceShl, string descricaoClassificacao)
        {
            try
            {
                string x = "";
                x = ws.cadastraMailing(new SHLWsHomologacao.cMailingParam()
                {
                    ambiente = "slomp2",
                    chave = chave,
                    mailing = new SHLWsHomologacao.cMailing()
                    {
                        mail_descricao = descricaoClassificacao
                    },
                    @interface = interfaceShl
                });
                return x;
            }
            catch (Exception e)
            {
                string erro = "Ocorreu um Erro na comunicação com o Webservice SHL: " + e.Message + " " + e.InnerException.Message;
                return erro;
            }
        }

        public static string EnviarCancelamentoPedidoSHL(string chave, int interfaceShl, string origem)
        {
            try
            {
                string x = "";
                x = ws.setCancelarPedido(new SHLWsHomologacao.sCancelarPedidoParam
                {
                    ambiente = "slomp2",
                    chave = chave,
                    @interface = interfaceShl,
                    destino = new SHLWsHomologacao.sCancelarPedidoDestino()
                    {
                        numero_pedido_origem = origem
                    }
                });
                return x;
            }
            catch (Exception e)
            {
                string erro = "Ocorreu um Erro na comunicação com o Webservice SHL: " + e.Message + " " + e.InnerException.Message;
                return erro;
            }
        }

        public static string EnviarXMLPedidoSHL(string chave, int interfaceShl, string xml, string codPedido, int codNota)
        {
            try
            {
                SHLWsHomologacao.pXmlRetorno x;
                x = ws.postXmlDanfePedido(new cwkGestao.Integracao.SHL.SHLWsHomologacao.sPostXmlPedidoParam()
                {
                    ambiente = "slomp2",
                    chave = chave,
                    @interface = interfaceShl,
                    destino = new cwkGestao.Integracao.SHL.SHLWsHomologacao.sPostXmlPedidoDestino()
                    {
                        numero_pedido_origem = codPedido,
                        numero_nota_fiscal = codNota,
                        arquivo_xml = xml
                    }
                });
                return x.retorno;
            }
            catch (Exception e)
            {
                string erro = "Ocorreu um Erro na comunicação com o Webservice SHL: " + e.Message + " " + e.InnerException.Message;
                return erro;
            }
        }

        public static string CadastraGrupoCampanhas(string chave, int interfaceShl, string descricao)
        {
            try
            {
                string x = "";
                x = ws.cadastraCampanha(new SHLWsHomologacao.cCampanhasParam()
                {
                    ambiente = "slomp2",
                    chave = chave,
                    @interface = interfaceShl,
                    campanha = new SHLWsHomologacao.cCampanhas()
                    {
                        camp_descricao = descricao
                    }
                });
                return x;
            }
            catch (Exception e)
            {
                string erro = "Ocorreu um Erro na comunicação com o Webservice SHL: " + e.Message + " " + e.InnerException.Message;
                return erro;
            }
        }

        public static string CadastraFornecedor(string chave, int interfaceShl, string nomeFantasia, string razaoSocial, string endereco, string numeroEndereco,
            string bairro, string complementoEndereco, string cep, string cidade, string Uf, string Pais, string Telefone, string Cnpj, string Ie)
        {
            try
            {
                SHLWsHomologacao.cCadastraFornecedorRetorno x = new cwkGestao.Integracao.SHL.SHLWsHomologacao.cCadastraFornecedorRetorno();
                x = ws.cadastraFornecedor(new SHLWsHomologacao.cFornecedorParam()
                {
                    ambiente = "slomp2",
                    chave = chave,
                    @interface = interfaceShl,
                    dados_fornecedor = new cwkGestao.Integracao.SHL.SHLWsHomologacao.cFornecedor()
                    {
                        nome_fantasia = nomeFantasia,
                        razao_social = razaoSocial,
                        logradouro = endereco,
                        numero = numeroEndereco,
                        bairro = bairro,
                        cep = cep,
                        cidade = cidade,
                        uf = Uf,
                        pais = Pais,
                        telefone = Telefone,
                        cnpj = Cnpj,
                        complemento = complementoEndereco,
                        ie = Ie
                    }
                });
                if (x.retorno != null)
                {
                    return Regex.Replace(x.retorno, @"[^\d]", String.Empty);
                }
                else
                {
                    return x.falha;
                }
            }
            catch (Exception e)
            {
                string erro = "Ocorreu um Erro na comunicação com o Webservice SHL: " + e.Message + " " + e.InnerException.Message;
                return erro;
            }
        }

        public static string CadastraPedido(string chave, int interfaceShl, Dictionary<string, int> Produtos, bool AR, bool MP, decimal ValorDeclarado, int codTransportadora, string codDestino, int IDClassificacao,
           int IDTipoOrcamentoDistribuicao, string IDNumPedidoOrigem, string bairro, string cep, string cidade, string SiglaEstado, string endereco, string numero, string complemento,
           string telefone, string cpf, string cnpj, string email, string nomeCliente, string observacao, string pais, string razao_social, string StatusPedido)
        {
            try
            {
                SHLWsHomologacao.cPedidoParam parms = new SHLWsHomologacao.cPedidoParam()
                {
                    ambiente = "slomp2",
                    chave = chave,
                    @interface = interfaceShl,
                    materiais = new List<SHLWsHomologacao.pMaterial>()
                    {
                    }.ToArray(),
                    destino = new SHLWsHomologacao.pDestino()
                    {
                        codigo_transporte = codTransportadora, //código informado no gestao.
                        codigo_destino = codDestino,
                        id_mailing = IDClassificacao,
                        id_distribuicao = IDTipoOrcamentoDistribuicao,
                        tipo_pedido = StatusPedido,
                        numero_pedido_origem = IDNumPedidoOrigem,
                        bairro = bairro,
                        cep = cep,
                        cidade = cidade,
                        cnpj = cnpj,
                        cpf = cpf,
                        complemento = complemento,
                        email = email,
                        logradouro = endereco,
                        nome = nomeCliente,
                        numero = numero,
                        observacao = observacao,
                        pais = pais,
                        razao_social = razao_social,
                        sigla_estado = SiglaEstado,
                        telefone = telefone
                    },
                    servicos_adicionais = new List<SHLWsHomologacao.pServicos_adicionais>().ToArray()
                };
                List<SHLWsHomologacao.pMaterial> materiais = new List<SHLWsHomologacao.pMaterial>();
                foreach (string key in Produtos.Keys)
                {
                    materiais.Add(new SHLWsHomologacao.pMaterial()
                    {
                        codigo = key,
                        quantidade = Produtos[key]
                    });
                }
                List<SHLWsHomologacao.pServicos_adicionais> servicosAdicionais = new List<cwkGestao.Integracao.SHL.SHLWsHomologacao.pServicos_adicionais>();
                if (AR)
                {
                    servicosAdicionais.Add(new cwkGestao.Integracao.SHL.SHLWsHomologacao.pServicos_adicionais()
                    {
                        codigo_servico = 1
                    });
                }
                if (MP)
                {
                    servicosAdicionais.Add(new cwkGestao.Integracao.SHL.SHLWsHomologacao.pServicos_adicionais()
                    {
                        codigo_servico = 2
                    });
                }
                if (ValorDeclarado > 0)
                {
                    servicosAdicionais.Add(new cwkGestao.Integracao.SHL.SHLWsHomologacao.pServicos_adicionais()
                    {
                        codigo_servico = 3
                    });
                }
                parms.materiais = materiais.ToArray();
                string retorno = ws.cadastraPedido(parms);
                return retorno;
            }
            catch (Exception e)
            {
                string erro = "Ocorreu um Erro na comunicação com o Webservice SHL: " + e.Message + " " + e.InnerException.Message;
                return erro;
            }

        }

        public static string CadastraTipoOrcamentoDistribuicao(string chave, int interfaceShl, string descricao)
        {

            try
            {
                string retorno = ws.cadastraDistribuicao(new SHLWsHomologacao.cDistribuicaoParam()
                {
                    ambiente = "slomp2",
                    chave = chave,
                    @interface = interfaceShl,
                    distribuicao = new SHLWsHomologacao.cDistribuicao()
                    {
                        dist_descricao = descricao
                    }
                });
                return retorno;
            }
            catch (Exception e)
            {
                string erro = "Ocorreu um Erro na comunicação com o Webservice SHL: " + e.Message + " " + e.InnerException.Message;
                return erro;
            }
        }

        public static string CadastraProduto(string chave, int interfaceShl,
            string codProduto, int codGrupoCampanha, int largura, int altura,
            int comprimento, int peso, string descricaoMaterial)
        {
            try
            {
                string retorno = ws.cadastraMaterial(new SHLWsHomologacao.cMaterialParam()
                {
                    ambiente = "slomp2",
                    chave = chave,
                    @interface = interfaceShl,
                    material = new SHLWsHomologacao.cMaterialCadastro()
                    {
                        codigo = codProduto,
                        camp_id = codGrupoCampanha,
                        altura = altura,
                        comprimento = comprimento,
                        largura = largura,
                        peso = peso,
                        descricao_material = descricaoMaterial
                    }
                });
                return retorno;
            }
            catch (Exception e)
            {
                string erro = "Ocorreu um Erro na comunicação com o Webservice SHL: " + e.Message + " " + e.InnerException.Message;
                return erro;
            }
        }

        public static Dictionary<string, DateTime> GetCodigosRastreamentoSHL(string chaveSHL, int interfaceSHL, int CodPedidoGestao)
        {
            try
            {
                Dictionary<string, DateTime> DatasCodsRastreamento = new Dictionary<string, DateTime>();
                IList<SHLWsHomologacao.gRastreio> result = ws.getRastreio(new SHLWsHomologacao.gRastreioParam()
                {
                    ambiente = "slomp2",
                    chave = chaveSHL,
                    @interface = interfaceSHL,
                    destino = new SHLWsHomologacao.gDestino()
                    {
                        numero_pedido_origem = CodPedidoGestao.ToString()
                    }
                }).retorno;
                if (result != null)
                {
                    foreach (SHLWsHomologacao.gRastreio item in result)
                    {
                        if (!DatasCodsRastreamento.ContainsKey(item.reg_registro))
                        {
                            DatasCodsRastreamento.Add(item.reg_registro, item.reg_data_envio);
                        }
                    }
                }
                return DatasCodsRastreamento;
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um Erro na comunicação com o Webservice SHL: " + e.Message + " " + e.InnerException.Message);
            }
        }

        public static SHLWsHomologacao.gStatusPedidoArray GetStatusPedido(string chaveSHL, int interfaceSHL, int CodPedidoGestao)
        {
            SHLWsHomologacao.gStatusPedidoArray status = null;
            try
            {
                status = ws.getStatusPedido(new cwkGestao.Integracao.SHL.SHLWsHomologacao.gStatusPedidoParam()
                {
                    ambiente = "slomp2",
                    chave = chaveSHL,
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

        public static string CadastraServicoTransporte(string chaveSHL, int interfaceSHL, string CnpjFornecedor, string CodServico, string DescricaoServico, string CodContrato)
        {
            try
            {
                var x = ws.cadastraServicoTransporte(new SHLWsHomologacao.cServicoTransporteParam()
                {
                    ambiente = "slomp2",
                    chave = chaveSHL,
                    @interface = interfaceSHL,
                    servico_transporte = new SHLWsHomologacao.cServicoTransporte()
                    {
                        cnpj_fornecedor = CnpjFornecedor,
                        codigo_servico = CodServico,
                        descricao_servico = DescricaoServico,
                        numero_contrato_servico = CodContrato
                    }
                });
                if (x.retorno != null)
                {
                    return Regex.Replace(x.retorno, @"[^\d]", String.Empty);
                }
                return x.falha;
            }
            catch (Exception e)
            {
                string erro = "Ocorreu um Erro na comunicação com o Webservice SHL: " + e.Message + " " + e.InnerException.Message;
                return erro;
            }

        }
    }
}
