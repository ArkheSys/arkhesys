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
    public static class UtilProducao
    {
        private static SHLWs.WebserviceINLOCUSSHL ws = new SHLWs.WebserviceINLOCUSSHL();

        public static string CadastraClassificacao(string chave, int interfaceShl, string descricaoClassificacao)
        {
            try
            {
                string x = "";
                x = ws.cadastraMailing(new SHLWs.cMailingParam()
                {
                    ambiente = "slomp3",
                    chave = chave,
                    mailing = new SHLWs.cMailing()
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
                x = ws.setCancelarPedido(new SHLWs.sCancelarPedidoParam
                {
                    ambiente = "slomp3",
                    chave = chave,
                    @interface = interfaceShl,
                    destino = new SHLWs.sCancelarPedidoDestino()
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
                SHLWs.pXmlRetorno x;
                x = ws.postXmlDanfePedido(new cwkGestao.Integracao.SHL.SHLWs.sPostXmlPedidoParam()
                {
                    ambiente = "slomp3",
                    chave = chave,
                    @interface = interfaceShl,
                    destino = new cwkGestao.Integracao.SHL.SHLWs.sPostXmlPedidoDestino()
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
                x = ws.cadastraCampanha(new SHLWs.cCampanhasParam()
                {
                    ambiente = "slomp3",
                    chave = chave,
                    @interface = interfaceShl,
                    campanha = new SHLWs.cCampanhas()
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
                SHLWs.cCadastraFornecedorRetorno x = new cwkGestao.Integracao.SHL.SHLWs.cCadastraFornecedorRetorno();
                x = ws.cadastraFornecedor(new SHLWs.cFornecedorParam()
                {
                    ambiente = "slomp3",
                    chave = chave,
                    @interface = interfaceShl,
                    dados_fornecedor = new cwkGestao.Integracao.SHL.SHLWs.cFornecedor()
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
                SHLWs.cPedidoParam parms = new SHLWs.cPedidoParam()
                {
                    ambiente = "slomp3",
                    chave = chave,
                    @interface = interfaceShl,
                    materiais = new List<SHLWs.pMaterial>()
                    {
                    }.ToArray(),
                    destino = new SHLWs.pDestino()
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
                    servicos_adicionais = new List<SHLWs.pServicos_adicionais>().ToArray()
                };
                List<SHLWs.pMaterial> materiais = new List<SHLWs.pMaterial>();
                foreach (string key in Produtos.Keys)
                {
                    materiais.Add(new SHLWs.pMaterial()
                    {
                        codigo = key,
                        quantidade = Produtos[key]
                    });
                }
                List<SHLWs.pServicos_adicionais> servicosAdicionais = new List<cwkGestao.Integracao.SHL.SHLWs.pServicos_adicionais>();
                if (AR)
                {
                    servicosAdicionais.Add(new cwkGestao.Integracao.SHL.SHLWs.pServicos_adicionais()
                    {
                        codigo_servico = 1
                    });
                }
                if (MP)
                {
                    servicosAdicionais.Add(new cwkGestao.Integracao.SHL.SHLWs.pServicos_adicionais()
                    {
                        codigo_servico = 2
                    });
                }
                if (ValorDeclarado > 0)
                {
                    servicosAdicionais.Add(new cwkGestao.Integracao.SHL.SHLWs.pServicos_adicionais()
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
                string retorno = ws.cadastraDistribuicao(new SHLWs.cDistribuicaoParam()
                {
                    ambiente = "slomp3",
                    chave = chave,
                    @interface = interfaceShl,
                    distribuicao = new SHLWs.cDistribuicao()
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
                string retorno = ws.cadastraMaterial(new SHLWs.cMaterialParam()
                {
                    ambiente = "slomp3",
                    chave = chave,
                    @interface = interfaceShl,
                    material = new SHLWs.cMaterialCadastro()
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
                IList<SHLWs.gRastreio> result = ws.getRastreio(new SHLWs.gRastreioParam()
                {
                    ambiente = "slomp3",
                    chave = chaveSHL,
                    @interface = interfaceSHL,
                    destino = new SHLWs.gDestino()                   
                    {
                        numero_pedido_origem = CodPedidoGestao.ToString()
                    },
                }).retorno;
                if (result != null)
                {
                    foreach (SHLWs.gRastreio item in result)
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

        public static SHLWs.gStatusPedidoArray GetStatusPedido(string chaveSHL, int interfaceSHL, int CodPedidoGestao)
        {
            SHLWs.gStatusPedidoArray status = null;
            try
            {
                status = ws.getStatusPedido(new cwkGestao.Integracao.SHL.SHLWs.gStatusPedidoParam()
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
            try
            {
                var x = ws.cadastraServicoTransporte(new SHLWs.cServicoTransporteParam()
                {
                    ambiente = "slomp3",
                    chave = chaveSHL,
                    @interface = interfaceSHL,
                    servico_transporte = new SHLWs.cServicoTransporte()
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
