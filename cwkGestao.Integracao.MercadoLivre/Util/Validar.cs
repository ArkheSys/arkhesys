using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using Modelo;
using Modelo.VendaAtributos;
using cwkGestao.Negocio;
using cwkGestao.Integracao.MercadoLivre.WebService;
using cwkGestao.Modelo.Util;

namespace cwkGestao.Integracao.MercadoLivre.Util
{
    public class Validar
    {
        public Pessoa ValidarCliente(Result venda , MercadoLivreConfiguracao mConf)
        {
            WS_Usuario ws_user = new WS_Usuario();
            Pessoa pessoa = PessoaController.Instancia.GetClienteMercadoLivre(venda.Buyer.Id);

            if (pessoa == null)
            {
                pessoa = new Pessoa();

                pessoa.Nome = venda.Buyer.FirstName + " " + venda.Buyer.LastName;
                pessoa.BCliente = true;
                pessoa.Apelido = venda.Buyer.Nickname;
                Classificacao Classificacao = ClassificacaoController.Instancia.LoadObjectById(1);
                if (Classificacao != null)
                {
                    pessoa.Classificacao = Classificacao;    
                }
                if (venda.Buyer.BillingInfo.DocType == "CPF")
                {
                    pessoa.TipoPessoa = "Física";
                    if (venda.Buyer.BillingInfo.DocNumber != null)
                        pessoa.CNPJ_CPF = venda.Buyer.BillingInfo.DocNumber;
                    else
                        pessoa.CNPJ_CPF = "000.000.000-00";
                }
                else if (venda.Buyer.BillingInfo.DocType == "CNPJ")
                {
                    pessoa.TipoPessoa = "Jurídica";
                    if (venda.Buyer.BillingInfo.DocNumber != null)
                        pessoa.CNPJ_CPF = venda.Buyer.BillingInfo.DocNumber;
                    else
                        pessoa.CNPJ_CPF = "00.000.000/0000-00";
                }
                else if (venda.Buyer.BillingInfo.DocType == null)
                {
                    pessoa.TipoPessoa = "Física";
                    pessoa.CNPJ_CPF = "000.000.000-00";
                }
                    
                pessoa.BAtivo = true;
                pessoa.BCliente = true;
                pessoa.DtCadastro = DateTime.Now;
                pessoa.IDMercadoLivre = venda.Buyer.Id;
                pessoa.BContribuinte = false;
                pessoa.Sexo = "0";
                pessoa.EstadoCivil = "0";
                pessoa.Inscricao = "";
                pessoa.Filial = mConf.Filial;
               
                PessoaEmail pEmail = new PessoaEmail();
                pEmail.BFinanceiro = true;
                pEmail.BNFe = true;
                pEmail.Email = venda.Buyer.Email;
                pEmail.Pessoa = pessoa;
                pessoa.PessoaEmails.Add(pEmail);

                PessoaTelefone pFone = new PessoaTelefone();
                pFone.Pessoa = pessoa;
                pFone.Numero = venda.Buyer.Phone.Number;
                pFone.BPrincipal = true;
                pessoa.PessoaTelefones.Add(pFone);

                Util util = new Util();
                PessoaEndereco pEndereco = new PessoaEndereco();
                pEndereco.Bairro = "S/B";

                if (venda.Shipping.ReceiverAddress != null)
                {
                    pEndereco.Endereco = venda.Shipping.ReceiverAddress.AddressLine.Trim();
                    
                    List<string> numero = new List<string>();
                    int continuou = 0;
                    foreach(Char c in pEndereco.Endereco.ToCharArray())
                    {
                        if (char.IsNumber(c))
                        {
                            if (continuou != 0)
                            {
                                numero.Clear();
                                continuou = 0;
                            }
                            numero.Add(c.ToString());
                        }
                        else continuou = 1;
                    }
                    pEndereco.Numero = string.Join("", numero.ToArray());

                    pEndereco.CEP = venda.Shipping.ReceiverAddress.ZipCode;
                    pEndereco.BEntrega = true;
                    pEndereco.BCobranca = true;
                    pEndereco.BPrincipal = true;
                    try
                    {
                        try
                        {
                            try
                            {
                                pEndereco.Cidade = CidadeController.Instancia.GetByLikeName(venda.Shipping.ReceiverAddress.City.Name, venda.Shipping.ReceiverAddress.State.Id.Substring(3));
                            }
                            catch (Exception)
                            {
                                
                                pEndereco.Cidade = CidadeController.Instancia.GetByLikeName(ws_user.GetNomeCidade(venda.Shipping.ReceiverAddress.City.Id), venda.Shipping.ReceiverAddress.State.Id.Substring(3));
                            }                    		
	                    }
	                    catch (Exception)
	                    {
                            try
                            {
                                pEndereco.Cidade = CidadeController.Instancia.GetByLikeName("INDEFINIDA", "PR");
                            }
                            catch (Exception)
                            {
                                if (pEndereco.Cidade == null)
                                {
                                    pEndereco.Cidade = mConf.Filial.Cidade;
                                }
                            }
	                    }
                    }
                    catch (Exception)
                    {
                        try
                        {
                            pEndereco.Cidade = CidadeController.Instancia.GetByLikeName("INDEFINIDA","PR");
                        }
                        catch (Exception)
                        {
                            if (pEndereco.Cidade == null)
                            {
                                pEndereco.Cidade = mConf.Filial.Cidade;
                            }
                        }
                    }
                   
                    pEndereco.Pessoa = pessoa;
                    pessoa.PessoaEnderecos.Add(pEndereco);
                }
                else
                {
                    try
                    {
                        pEndereco.Cidade = CidadeController.Instancia.GetByLikeName("INDEFINIDA", "PR");
                    }
                    catch (Exception)
                    {
                        if (pEndereco.Cidade == null)
                        {
                            pEndereco.Cidade = mConf.Filial.Cidade;
                        }
                    }
                    pEndereco.Endereco = "NÃO INFORMADO";
                    pEndereco.Pessoa = pessoa;
                    pEndereco.BPrincipal = true;
                    pessoa.PessoaEnderecos.Add(pEndereco);
                }
                

                PessoaCliente pCliente = new PessoaCliente();
                pCliente.Pessoa = pessoa;
                pCliente.TabelaPreco = mConf.TabelaPreco;
                pCliente.Vendedor = mConf.Vendedor;
                pCliente.BBloqueiaVendedor = true;
                pCliente.BContribuinte = true;
                pessoa.PessoaClientes.Add(pCliente);

                PessoaController.Instancia.Salvar(pessoa, Acao.Incluir);
            }
            return pessoa;
        }

        public Produto ValidarProduto(Item produto)
        {
            Produto pFinal = ProdutoMercadoLivreController.Instancia.LoadProdutoByIDMercadoLivre(Int32.Parse(produto.Id.Substring(3)));
            return pFinal;
          
        }
    }
}
