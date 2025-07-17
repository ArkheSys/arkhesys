using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Correios.WsCorreios;
using cwkGestao.Modelo;
using cwkGestao.Negocio;
using cwkGestao.Repositorio;
using cwkGestao.Modelo.Util;
using System.Net;
using System.IO;
using System.Xml;
using cwkGestao.Integracao.Correios.Util;
using System.Reflection;
using cwkGestao.Integracao.Correios.WsCorreiosPrecoPrazo;

namespace cwkGestao.Integracao.Correios
{
    public static class IntegracaoWsCorreios
    {
        private static Dictionary<int, string> urlPedidos = new Dictionary<int, string>();
#pragma warning disable CS0169 // O campo "IntegracaoWsCorreios.nomeNo" nunca é usado
        private static string nomeNo;
#pragma warning restore CS0169 // O campo "IntegracaoWsCorreios.nomeNo" nunca é usado
        public static string[] SolicitaEtiquetasCorreios(Filial pFilial, TipoServicoTransporte pTipoServico, int pQuantidade)
        {
            AtendeClienteService wsCorreios = new AtendeClienteService();

            try
            {
                clienteERP cliente = wsCorreios.buscaCliente(pFilial.correioContrato, pFilial.correioCartaoPostagem, pFilial.correioUsuario, pFilial.correioSenha);

                #region pega idServiço de acordo com o codigo do serviço selecionado no TipoServicoTransporte
                contratoERP c = cliente.contratos.ToList().Where(i => i.contratoPK.numero.Trim() == pFilial.correioContrato).FirstOrDefault();
                if (c == null)
                {
                    throw new Exception("O número do contrato cadastrado na empresa está incorreto ou não existe !\r\n Por favor verifique.");
                }
                cartaoPostagemERP p = c.cartoesPostagem.ToList().Where(cp => cp.numero.Trim() == pFilial.correioCartaoPostagem).FirstOrDefault();
                if (p == null)
                {
                    throw new Exception("O número do Cartão de Postagem cadastrado na empresa está incorreto ou não existe !\r\n Por favor verifique.");
                }
                if (pTipoServico.CodigoServico == null)
                {
                    throw new Exception("Não foi definido um código de serviço para o serviço \""+ pTipoServico.Descricao 
                        +"\". Corrija este serviço no cadastro de Tipo de Serviço de Transporte antes de prosseguir.");
                }
                servicoERP s = p.servicos.ToList().Where(sv => sv.codigo.Trim() == pTipoServico.CodigoServico.Substring(0,5)).FirstOrDefault();
                if (s == null)
                    throw new Exception("O código do Serviço Correios cadastrado no Tipo Serviço Transporte não está disponível para filial em questão !\r\n Por favor verifique.");
                #endregion

                string ret = wsCorreios.solicitaEtiquetas("C", pFilial.CNPJ.Replace(".", "").Replace("/", "").Replace("-", ""), s.servicoSigep.servico,
                             true, pQuantidade, true, pFilial.correioUsuario, pFilial.correioSenha);

                string[] retArray = ret.Split(',');
                string numEtiqueta1 = StringUtil.GetSomenteNumeros(retArray[0]), numEtiqueta2 = StringUtil.GetSomenteNumeros(retArray[1]);

                string header = retArray[0].Substring(0,2);
                string end = retArray[0].Substring(retArray[0].Length - 2,2);
                Int32 inicio = Convert.ToInt32(numEtiqueta1);
                Int32 fim = Convert.ToInt32(numEtiqueta2);


                List<string> l = new List<string>();

                for (int i = inicio; i <= fim; i++)
                {
                    string codigo = i.ToString();

                    while (codigo.Length < 8)
                    {
                        codigo = "0" + codigo;
                    }

                    l.Add(header + codigo + " " + end);
                }

                return GetDigitoVerificadorCorreios(l.ToArray(), pFilial.correioUsuario, pFilial.correioSenha, pQuantidade);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                wsCorreios.Dispose();
            }
        }

        private static string[] GetDigitoVerificadorCorreios(string[] pCodigosRastreamento, string pUsuario, string pSenha, int pQuantidade)
        {
            AtendeClienteService wsCorreios = new AtendeClienteService();

            try
            {
                var ret = wsCorreios.geraDigitoVerificadorEtiquetas(pCodigosRastreamento.Distinct().ToArray(), pUsuario, pSenha);

                for (int i = 0; i < pCodigosRastreamento.Count(); i++)
                {
                    pCodigosRastreamento[i] = pCodigosRastreamento[i].Replace(" ", ret[i].ToString());  
                }

                return pCodigosRastreamento;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                wsCorreios.Dispose();
            }
        }

        public static bool GetStatusCorreios(string numCartaoPostagem, string usuario, string senha, ref string statusRetorno)
        {
            AtendeClienteService wsCorreios = new AtendeClienteService();
            bool retorno;
            try
            {
                statusCartao status; 
                wsCorreios.getStatusCartaoPostagem(numCartaoPostagem, usuario, senha, out status, out retorno);
                statusRetorno = status.ToString();

                return retorno;
            }
            catch (Exception ex)
            {
                statusRetorno = ex.Message;
                retorno = false;
                return retorno;
            }

			finally
            {
                wsCorreios.Dispose();
            }
        }

        public static long fechaPlpVariosServicos(string[] listaEtiquetas, Filial pFilial, IList<ExpedicaoRastreamento> listaExpRast, clienteERP cliente)
        {
            long retorno;
            bool retornoEspecificado;
            AtendeClienteService wsCorreios = new AtendeClienteService();

            try 
	        {
                ConversorXML conversor = new ConversorXML();

                if (listaExpRast.Count == 0)
                {
                    throw new Exception("não existe expedição");
#pragma warning disable CS0162 // Código inacessível detectado
                    return 0;
#pragma warning restore CS0162 // Código inacessível detectado
                }
                else
                {
                    conversor.GerarXML(pFilial, listaExpRast, cliente);

                    System.IO.StreamReader sr = new System.IO.StreamReader((@Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\correio.xml"));
                    wsCorreios.fechaPlpVariosServicos(sr.ToString(), cliente.id, true,
                                                      pFilial.correioCartaoPostagem, listaEtiquetas, pFilial.correioUsuario, pFilial.correioSenha,
                                                      out retorno, out retornoEspecificado);
                    return retorno;
                }
	        }
	        catch (Exception ex)
	        {
        		throw new Exception(ex.Message);
	        }
            finally     
            {
                wsCorreios.Dispose();
            }
        }

        public static CorreiosPrecoPrazo CalculoPrecoPrazo(CorreiosPrecoPrazo cpp)
        {
            CalcPrecoPrazoWS WS = new CalcPrecoPrazoWS();
            try
            {
                cResultado result = WS.CalcPrecoPrazo(cpp.IDPedido.Filial.nCdEmpresa, cpp.IDPedido.Filial.sDsSenha, cpp.NCdServico,
                    cpp.CepOrigem.Replace("-", ""), cpp.CepDestino.Replace("-", ""), cpp.NVlPeso.ToString(), cpp.NCdFormato, cpp.NVlComprimento, cpp.NVlAltura,
                    cpp.NVlLargura, cpp.NVlDiametro, cpp.SCdMaoPropria, cpp.NVlValorDeclarado, cpp.SCdAvisoRecebimento);
                cpp.DtPrazoEntrega = DateTime.Now.AddDays(Convert.ToDouble(result.Servicos[0].PrazoEntrega));
                cpp.Valor = Convert.ToDecimal(result.Servicos[0].Valor);
                cpp.ValorAvisoRecebimento = Convert.ToDecimal(result.Servicos[0].ValorAvisoRecebimento);
                cpp.ValorMaoPropria = Convert.ToDecimal(result.Servicos[0].ValorMaoPropria);
                cpp.ValorValorDeclarado = Convert.ToDecimal(result.Servicos[0].ValorValorDeclarado);
                cpp.Erro = Convert.ToInt32(result.Servicos[0].Erro);
                cpp.MsgErro = result.Servicos[0].MsgErro;

                return cpp.Clone() as CorreiosPrecoPrazo;
            }
            catch (Exception x)
            {

                throw x;
            }
            finally
            {
                WS.Dispose();
            }

        }
    }
}
