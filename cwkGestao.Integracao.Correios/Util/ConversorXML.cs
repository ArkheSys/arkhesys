using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using cwkGestao.Modelo;
using cwkGestao.Integracao.Correios.WsCorreios;
using cwkGestao.Negocio;
using System.Reflection;
using System.IO;

namespace cwkGestao.Integracao.Correios.Util
{
    public class ConversorXML
    {
        public void GerarXML(Filial filial, IList<ExpedicaoRastreamento> ListobjPostal, clienteERP remetente)
        {
            XmlTextWriter writer = new XmlTextWriter(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\correio.xml", null);

            //inicia o documento xml
            //writer.WriteStartDocument();
            writer.WriteProcessingInstruction("xml", "version'=1.0' encoding='ISO-8859-1'");

            try
            {
                //define a indentação do arquivo
                writer.Formatting = Formatting.Indented;
                
                //escreve um comentário
                writer.WriteComment("XML dos correios");

                //escreve o elmento raiz
                writer.WriteStartElement("correioslog");

                        writer.WriteStartElement("tipo_arquivo");
                        writer.WriteString("Postagem");
                        writer.WriteEndElement();

                        writer.WriteStartElement("versao_arquivo");
                        writer.WriteString("2.3");
                        writer.WriteEndElement();

                        //Inicia o elemento PLP
                        writer.WriteStartElement("plp");

                                writer.WriteStartElement("id_plp");
                                writer.WriteString("");
                                writer.WriteEndElement();

                                writer.WriteStartElement("valor_global");
                                writer.WriteString("");
                                writer.WriteEndElement();

                                writer.WriteStartElement("mcu_unidade_postagem");
                                writer.WriteString("");
                                writer.WriteEndElement();

                                writer.WriteStartElement("nome_unidade_postagem");
                                writer.WriteString("");
                                writer.WriteEndElement();

                                writer.WriteStartElement("cartao_postagem");
                                writer.WriteString(filial.correioCartaoPostagem);
                                writer.WriteEndElement();

                        //encerra o elemento PLP
                        writer.WriteEndElement();

                        //Inicia o elemento remetente
                        writer.WriteStartElement("remetente");

                                writer.WriteStartElement("numero_contrato");
                                writer.WriteString(filial.correioContrato);
                                writer.WriteEndElement();

                                writer.WriteStartElement("numero_diretoria");
                                writer.WriteString(remetente.contratos[0].codigoDiretoria.Trim());
                                writer.WriteEndElement();

                                writer.WriteStartElement("codigo_administrativo");
                                writer.WriteString(filial.correioCodigoAdministrativo);
                                writer.WriteEndElement();

                                 //Inicia o elemento nome_remetente
                                writer.WriteStartElement("nome_remetente");
                                //escrever o CData nome_remetente
                                writer.WriteCData(filial.Nome);
                                //encerra o elemento nome_remetente
                                writer.WriteEndElement();

                                //Inicia o elemento logradouro_remetente
                                writer.WriteStartElement("logradouro_remetente");
                                //escrever o CData logradouro_remetente
                                writer.WriteCData(filial.Endereco);
                                //encerra o elemento logradouro_remetente
                                writer.WriteEndElement();

                                //Inicia o elemento numero_remetente
                                writer.WriteStartElement("numero_remetente");
                                //escrever o CData numero_remetente
                                writer.WriteString(filial.Numero);
                                //encerra o elemento numero_remetente
                                writer.WriteEndElement();

                                //Inicia o elemento complemento_remetente
                                writer.WriteStartElement("complemento_remetente");
                                //escrever o CData complemento_remetente
                                writer.WriteCData("complemento_remetente");
                                //encerra o elemento complemento_remetente
                                writer.WriteEndElement();

                                //Inicia o elemento bairro_remetente
                                writer.WriteStartElement("bairro_remetente");
                                //escrever o CData bairro_remetente
                                writer.WriteCData(filial.Bairro);
                                //encerra o elemento complemento_remetente
                                writer.WriteEndElement();

                                //Inicia o elemento cep_remetente
                                writer.WriteStartElement("cep_remetente");
                                //escrever o CData cep_remetente
                                writer.WriteCData(filial.CEP);
                                //encerra o elemento cep_remetente
                                writer.WriteEndElement();

                                //Inicia o elemento cidade_remetente
                                writer.WriteStartElement("cidade_remetente");
                                //escrever o CData cidade_remetente
                                writer.WriteCData(filial.Cidade.Nome);
                                //encerra o elemento cidade_remetente
                                writer.WriteEndElement();

                                //Inicia o elemento uf_remetente
                                writer.WriteStartElement("uf_remetente");
                                //escrever o CData uf_remetente
                                writer.WriteString(filial.Cidade.UF.Sigla);
                                //encerra o elemento uf_remetente
                                writer.WriteEndElement();

                                //Inicia o elemento telefone_remetente
                                writer.WriteStartElement("telefone_remetente");
                                //escrever o CData telefone_remetente
                                writer.WriteCData(filial.Telefone);
                                //encerra o elemento telefone_remetente
                                writer.WriteEndElement();

                                //Inicia o elemento fax_remetente
                                writer.WriteStartElement("fax_remetente");
                                //escrever o CData fax_remetente
                                writer.WriteCData(filial.Fax);
                                //encerra o elemento fax_remetente
                                writer.WriteEndElement();

                                //Inicia o elemento email_remetente
                                writer.WriteStartElement("email_remetente");
                                //escrever o CData email_remetente
                                writer.WriteCData(filial.Email);
                                //encerra o elemento email_remetente
                                writer.WriteEndElement();

                        //encerra o elemento remetente
                        writer.WriteEndElement();

                        //Inicia o elemento forma_pagamento
                        writer.WriteStartElement("forma_pagamento");
                        //escrever o CData forma_pagamento
                        writer.WriteString("");
                        //encerra o elemento forma_pagamento
                        writer.WriteEndElement();

                        foreach (ExpedicaoRastreamento item in ListobjPostal)
                        {

                            int sequencia = 0;

                            if (sequencia == item.Pedido.CorreiosPrecoPrazo.Count-1)
                                sequencia = 0;

                            //Inicia o elemento objeto_postal
                            writer.WriteStartElement("objeto_postal");

                            writer.WriteStartElement("numero_etiqueta");
                            writer.WriteString(item.IDRastreamento.Numero);
                            writer.WriteEndElement();

                            writer.WriteStartElement("codigo_objeto_cliente");
                            writer.WriteString("");
                            writer.WriteEndElement();

                            writer.WriteStartElement("codigo_servico_postagem");
                            try
                            {
                                writer.WriteString(item.ServicoEnvioCorreio.Codigo);
                            }
                            catch (Exception)
                            {
                                writer.WriteString("");
                            }
                            writer.WriteEndElement();

                            writer.WriteStartElement("cubagem");
                            writer.WriteString("0");
                            writer.WriteEndElement();

                            writer.WriteStartElement("peso");
                            writer.WriteString(item.PesoBruto.ToString());
                            writer.WriteEndElement();

                            writer.WriteStartElement("rt1");
                            writer.WriteString("");
                            writer.WriteEndElement();

                            writer.WriteStartElement("rt2");
                            writer.WriteString("");
                            writer.WriteEndElement();

                            //Inicia o elemento destinatario
                            writer.WriteStartElement("destinatario");

                            //Inicia o elemento nome_destinatario
                            writer.WriteStartElement("nome_destinatario");
                            //escrever o CData nome_destinatario
                            writer.WriteCData(item.Pedido.PessoaNome);
                            //encerra o elemento nome_destinatario
                            writer.WriteEndElement();

                            //Inicia o elemento telefone_destinatario
                            writer.WriteStartElement("telefone_destinatario");
                            //escrever o CData telefone_destinatario
                            writer.WriteCData(item.Pedido.PessoaTelefone);
                            //encerra o elemento telefone_destinatario
                            writer.WriteEndElement();

                            //Inicia o elemento celular_destinatario
                            writer.WriteStartElement("celular_destinatario");
                            //escrever o CData celular_destinatario
                            try
                            {
                                writer.WriteCData(item.Pedido.Pessoa.PessoaTelefones.Where(x => x.Tipo == "Celular").FirstOrDefault().Numero);
                            }
                            catch (Exception)
                            {
                                writer.WriteCData("");
                            }
                                                        
                            writer.WriteEndElement();

                            //Inicia o elemento email_destinatario
                            writer.WriteStartElement("email_destinatario");
                            //escrever o CData email_destinatario
                            try
                            {
                                writer.WriteCData(item.Pedido.Pessoa.PessoaEmails.FirstOrDefault().Email);
                            }
                            catch (Exception)
                            {
                                writer.WriteCData("");
                            }
                            
                            //encerra o elemento email_destinatario
                            writer.WriteEndElement();

                            PessoaEndereco pEndereco = item.Pedido.Pessoa.PessoaEnderecos.Where(x => x.BPrincipal == true).FirstOrDefault();

                            //Inicia o elemento logradouro_destinatario
                            writer.WriteStartElement("logradouro_destinatario");
                            //escrever o CData logradouro_destinatario
                            writer.WriteCData(pEndereco.Endereco);
                            //encerra o elemento logradouro_destinatario
                            writer.WriteEndElement();

                            //Inicia o elemento complemento_destinatario
                            writer.WriteStartElement("complemento_destinatario");
                            //escrever o CData complemento_destinatario
                            writer.WriteCData(pEndereco.Complemento);
                            //encerra o elemento complemento_destinatario
                            writer.WriteEndElement();

                            //Inicia o elemento numero_end_destinatario
                            writer.WriteStartElement("numero_end_destinatario");
                            //escrever o CData numero_end_destinatario
                            writer.WriteString(pEndereco.Numero);
                            //encerra o elemento numero_end_destinatario
                            writer.WriteEndElement();

                            //encerra o elemento destinatario
                            writer.WriteEndElement();

                            //Inicia o elemento nacional
                            writer.WriteStartElement("nacional");

                            //Inicia o elemento bairro_destinatario
                            writer.WriteStartElement("bairro_destinatario");
                            writer.WriteCData(pEndereco.Bairro);
                            writer.WriteEndElement();

                            //Inicia o elemento cidade_destinatario
                            writer.WriteStartElement("cidade_destinatario");
                            //escrever o CData cidade_destinatario
                            writer.WriteCData(pEndereco.Cidade.Nome);
                            //encerra o elemento cidade_destinatario
                            writer.WriteEndElement();

                            //Inicia o elemento uf_destinatario
                            writer.WriteStartElement("uf_destinatario");
                            //escrever o CData uf_destinatario
                            writer.WriteCData(pEndereco.Cidade.UF.Sigla);
                            //encerra o elemento uf_destinatario
                            writer.WriteEndElement();

                            //Inicia o elemento cep_destinatario
                            writer.WriteStartElement("cep_destinatario");
                            //escrever o CData cep_destinatario
                            writer.WriteCData(pEndereco.CEP);
                            //encerra o elemento cep_destinatario
                            writer.WriteEndElement();

                            Nota nota;
                            try
                            {
                                nota = NotaController.Instancia.GetByPedido(item.Pedido);
                                Console.WriteLine(nota.Numero);
                            }
                            catch (Exception)
                            {
                                throw new Exception("Não existe nota para o pedido " + item.Pedido.Codigo);
                            }
                            

                            //Inicia o elemento codigo_usuario_postal
                            writer.WriteStartElement("codigo_usuario_postal");
                            //escrever o CData codigo_usuario_postal
                            writer.WriteString("");
                            //encerra o elemento codigo_usuario_postal
                            writer.WriteEndElement();

                            //Inicia o elemento centro_custo_cliente
                            writer.WriteStartElement("centro_custo_cliente");
                            //escrever o CData centro_custo_cliente
                            writer.WriteString("");
                            //encerra o elemento centro_custo_cliente
                            writer.WriteEndElement();

                            //Inicia o elemento numero_nota_fiscal
                            writer.WriteStartElement("numero_nota_fiscal");
                            //escrever o CData numero_nota_fiscal
                            writer.WriteString(nota.Numero.ToString());
                            //encerra o elemento numero_nota_fiscal
                            writer.WriteEndElement();

                            //Inicia o elemento serie_nota_fiscal
                            writer.WriteStartElement("serie_nota_fiscal");
                            //escrever o CData serie_nota_fiscal
                            writer.WriteString(nota.Serie);
                            //encerra o elemento serie_nota_fiscal
                            writer.WriteEndElement();

                            //Inicia o elemento valor_nota_fiscal
                            writer.WriteStartElement("valor_nota_fiscal");
                            //escrever o CData valor_nota_fiscal
                            writer.WriteString(nota.TotalNota.ToString());
                            //encerra o elemento valor_nota_fiscal
                            writer.WriteEndElement();

                            //Inicia o elemento natureza_nota_fiscal
                            writer.WriteStartElement("natureza_nota_fiscal");
                            //escrever o CData natureza_nota_fiscal
                            writer.WriteString("");
                            //encerra o elemento natureza_nota_fiscal
                            writer.WriteEndElement();

                            //Inicia o elemento descricao_objeto
                            writer.WriteStartElement("descricao_objeto");
                            //escrever o CData descricao_objeto
                            writer.WriteCData("");
                            //encerra o elemento descricao_objeto
                            writer.WriteEndElement();
                            
                            //Inicia o elemento valor_a_cobrar
                            writer.WriteStartElement("valor_a_cobrar");
                            //escrever o CData valor_a_cobrar
                            if (item.ServicoEnvioCorreio.Codigo == "40045" || item.ServicoEnvioCorreio.Codigo == "40126")
                                writer.WriteCData(nota.TotalNota.ToString());
                            else
                                writer.WriteCData("");
                            //encerra o elemento valor_a_cobrar
                            writer.WriteEndElement();

                            //encerra o elemento nacional
                            writer.WriteEndElement();

                            CorreiosPrecoPrazo aux;
                            try
                            {
                                aux = item.Pedido.CorreiosPrecoPrazo.Where(x => x.bSelecionado == true).ElementAt(sequencia);
                                sequencia++;
                                Console.WriteLine(aux.CepDestino);
                            }
#pragma warning disable CS0168 // A variável "ex" está declarada, mas nunca é usada
                            catch (Exception ex)
#pragma warning restore CS0168 // A variável "ex" está declarada, mas nunca é usada
                            {
                                throw new Exception("Não foi realizada a rotina de Correio Preço e Prazo para o pedido " + item.Pedido.Codigo);
                            }

                            //Inicia o elemento servico_adicional
                            writer.WriteStartElement("servico_adicional");

                            if (aux.SCdAvisoRecebimento == "S")
                            {
                                writer.WriteStartElement("codigo_servico_adicional");
                                writer.WriteString("001");
                                writer.WriteEndElement();
                            }

                            if (aux.SCdMaoPropria == "S")
                            {
                                writer.WriteStartElement("codigo_servico_adicional");
                                writer.WriteString("002");
                                writer.WriteEndElement();
                            }                            

                            if (aux.NVlValorDeclarado > 0)
                            {
                                writer.WriteStartElement("codigo_servico_adicional");
                                writer.WriteString("019");
                                writer.WriteEndElement();
                            }

                            writer.WriteStartElement("codigo_servico_adicional");
                            writer.WriteString("025");
                            writer.WriteEndElement();

                            writer.WriteStartElement("valor_declarado");
                            if (aux.NVlValorDeclarado > 0)
                                writer.WriteString(aux.NVlValorDeclarado.ToString());
                            else
                                writer.WriteString("");
                            writer.WriteEndElement();

                            //encerra o elemento descricao_objeto
                            writer.WriteEndElement();

                            //Inicia o elemento dimensao_objeto
                            writer.WriteStartElement("dimensao_objeto");
                            
                            //Inicia o elemento tipo_objeto
                            writer.WriteStartElement("tipo_objeto");
                            //escrever o CData tipo_objeto
                            try
                            {
                                switch (aux.FormatoPacoteTransporte.Codigo)
                                {
                                    case 1: writer.WriteString("002");
                                        break;
                                    case 2: writer.WriteString("003");
                                        break;
                                    case 3: writer.WriteString("001");
                                        break;
                                }
                            }
                            catch (Exception)
                            {
                                throw new Exception("Não foi definido um formato de pacote para transporte do pedido " + item.Pedido.Codigo);
                            }
                            
                            //encerra o elemento tipo_objeto
                            writer.WriteEndElement();

                            //Inicia o elemento dimensao_altura
                            writer.WriteStartElement("dimensao_altura");
                            //escrever o CData dimensao_altura
                            writer.WriteString(aux.NVlAltura.ToString());
                            //encerra o elemento dimensao_altura
                            writer.WriteEndElement();

                            //Inicia o elemento dimensao_largura
                            writer.WriteStartElement("dimensao_largura");
                            //escrever o CData dimensao_largura
                            writer.WriteString(aux.NVlLargura.ToString());
                            //encerra o elemento dimensao_largura
                            writer.WriteEndElement();

                            //Inicia o elemento dimensao_comprimento
                            writer.WriteStartElement("dimensao_comprimento");
                            //escrever o CData dimensao_comprimento
                            writer.WriteString(aux.NVlComprimento.ToString());
                            //encerra o elemento dimensao_comprimento
                            writer.WriteEndElement();

                            //Inicia o elemento dimensao_diametro
                            writer.WriteStartElement("dimensao_diametro");
                            //escrever o CData dimensao_diametro
                            writer.WriteString(aux.NVlDiametro.ToString());
                            //encerra o elemento dimensao_diametro
                            writer.WriteEndElement();
                            
                            //encerra o elemento dimensao_objeto
                            writer.WriteEndElement();

                            //Inicia o elemento data_postagem_sara
                            writer.WriteStartElement("data_postagem_sara");
                            //escrever o CData data_postagem_sara
                            writer.WriteString("");
                            //encerra o elemento data_postagem_sara
                            writer.WriteEndElement();

                            //Inicia o elemento status_processamento
                            writer.WriteStartElement("status_processamento");
                            //escrever o CData status_processamento
                            writer.WriteString("0");
                            //encerra o elemento status_processamento
                            writer.WriteEndElement();

                            //Inicia o elemento numero_comprovante_postagem
                            writer.WriteStartElement("numero_comprovante_postagem");
                            //escrever o CData numero_comprovante_postagem
                            writer.WriteString("");
                            //encerra o elemento numero_comprovante_postagem
                            writer.WriteEndElement();

                            //Inicia o elemento valor_cobrado
                            writer.WriteStartElement("valor_cobrado");
                            //escrever o CData valor_cobrado
                            writer.WriteString("");
                            //encerra o elemento valor_cobrado
                            writer.WriteEndElement();

                            //encerra o elemento objeto_postal
                            writer.WriteEndElement();
                        }

                writer.WriteEndElement();   
                  
                //Escreve o XML para o arquivo e fecha o objeto escritor
                writer.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
