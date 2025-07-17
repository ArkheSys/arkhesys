using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Util;
using cwkGestao.Modelo.ValidaPR;

namespace cwkGestao.Negocio.Utils
{
    public class ValidaPRController
    {
        private readonly NotaController notaController;
        private readonly ValidaPR objValidaPR;
        private IList<Nota> listaNotas;

        public ValidaPRController()
        {
            notaController = NotaController.Instancia;
            objValidaPR = new ValidaPR();
        }

        public void GerarArquivoMagnetico(Filial pFilial, DateTime pDataInicial, DateTime pDataFinal, InOutType pEntSai, Inventario pInventario, string Diretorio, bool geraRegistro60)
        {
            if (geraRegistro60)
            {
                listaNotas = notaController.GetNotasItensValidaParana(pFilial, pDataInicial, pDataFinal, pEntSai, true);
                if (geraRegistro60)
                    ValidaPRTipo60.GeraRegistro60R(pDataInicial, pDataFinal);
                IList<Nota> listaECF;

                listaECF = listaNotas.Where(n => n.ModeloDocto == 33 && n.CancDt == null).ToList();
                MontarRegistroTipo60(pDataInicial, pDataFinal);
                foreach (Nota objNota in listaECF)
                foreach (NotaItem objNotaItem in objNota.NotaItems)
                    if (objNotaItem.BCancelado != true)
                        MontarRegistoTipo75(pDataFinal, pDataFinal, objNotaItem);
            }

            listaNotas = notaController.GetNotasValidaParana(pFilial, pDataInicial, pDataFinal, pEntSai, geraRegistro60);
            MontarRegistroTipo10(pFilial, ref pDataInicial, ref pDataFinal);
            MontarRegistroTipo90(pFilial);
            MontarRegistroTipo11(pFilial);

            StringBuilder sb = new StringBuilder();

            // REGISTRO 50
            // 16/12 - SINTEGRA
            // Lançamentos, principalmente os de entrada estão sendo feitos de forma incorreta. O CFOP informado esta sendo o de SAIDA
            foreach (Nota objNota in listaNotas.Where(n => n.ModeloDocto != 33 && n.ModeloDocto != 0 && n.ModeloDocto != 65))
            {
                if (objNota.NotaICMSs.Count == 0)
                {
                    sb.AppendLine("Não foi possível gerar os registros \"Valida PR\" da nota nº " + objNota.Numero + ". Verificar Aba ICMS da tela de Nota com o código " + objNota.Codigo);
                    continue;
                }

                if (objNota.TipoNota.Ent_Sai == 2)
                {
                    if (objNota.Status == null || objNota.Status == "-1")
                        continue;
                }

                foreach (var objNotaIcms in objNota.NotaICMSs) MontarRegistroTipo50(objNota, objNotaIcms);
                foreach (var objNotaItem in objNota.NotaItems) MontarRegistroTipo54(pDataInicial, pDataFinal, objNota, objNotaItem);
            }

            // Registros 61
            foreach (var objNota in listaNotas.Where(x => x.ModeloDocto == 65 && x.Status != null && x.Status != "-1").OrderBy(x => x.DtEnvio))
            {
                var registro61  = new Tipo61();
                registro61.DataEmissao = objNota.DtEnvio.GetValueOrDefault();
                registro61.Modelo = 65;
                registro61.Serie = objNota.Serie.ToInt(1);
                registro61.NumeroInicialOrdem = objNota.ChaveNota.Substring(objNota.ChaveNota.Length - 6, 6);
                registro61.NumeroFinalOrdem = registro61.NumeroInicialOrdem;
                registro61.ValorTotal = objNota.TotalNota;
                registro61.BaseCalculoIcms = objNota.NotaItems.Sum(x => x.BaseICMS);
                registro61.ValorIcms = objNota.NotaItems.Sum(x => x.ValorICMS);
                registro61.ValorIsentoNaoTributado = objNota.NotaItems.Sum(x => x.ValorIsentoICMS);
                registro61.ValorOutros = objNota.NotaItems.Sum(x => x.ValorOutroICMS);
                registro61.ValorAliquota = objNota.NotaItems.FirstOrDefault().AliqICMS;
                objValidaPR.ListaRegistro61.Add(registro61);
            }

            try
            {
                if (pInventario != null)
                    MontarRegistroTipo74(pDataInicial, pDataFinal, pInventario);
            }
            catch (Exception f)
            {
                throw f;
            }

            objValidaPR.GeraArquivo(Diretorio, @"Sintegra" + 0 + pDataFinal.Month + "_" + pDataFinal.Year + ".txt");
            string erro = sb.ToString();
            if (erro != "") throw new Exception(erro);
        }

        private void MontarRegistroTipo74(DateTime pDataInicial, DateTime pDataFinal, Inventario pInventario)
        {
            foreach (var invProduto in pInventario.ListaProdutos)
            {
                Tipo74 registro74 = new Tipo74();
                registro74.DataInventario = invProduto.Inventario.Data;
                registro74.CodigoProduto = invProduto.Produto.Codigo;
                registro74.Quantidade = invProduto.Quantidade;
                registro74.ValorProduto = invProduto.Total;
                registro74.codigoPosse = Convert.ToString(invProduto.Inventario.CodigoProprietario);
                registro74.CNPJ = registro74.codigoPosse == "1" ? "" : invProduto.Inventario.Filial.CNPJ;
                registro74.InscricaoProprietario = registro74.codigoPosse == "1" ? "" : invProduto.Inventario.Filial.Inscricao;
                registro74.UF = "PR";

                objValidaPR.ListaRegistro74.Add(registro74);
                try
                {
                    if (objValidaPR.ListaRegistro75.Where(w => w.CodigoProduto == invProduto.Produto.Codigo).Count() == 0)
                        MontarRegistoTipo75(pDataInicial, pDataFinal, invProduto);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        private void MontarRegistroTipo54(DateTime pDataInicial, DateTime pDataFinal, Nota objNota, NotaItem objNotaItem)
        {
            AtribuirRegistroTipo54(objNota, objNotaItem);

            // Verificar se o item está na lista do Registro75, caso negativo incluir ele na lista
            if (objValidaPR.ListaRegistro75.Where(w => w.CodigoProduto == objNotaItem.Produto.Codigo).Count() == 0) MontarRegistoTipo75(pDataInicial, pDataFinal, objNotaItem);
        }

        private void MontarRegistroTipo60(DateTime pDataInicial, DateTime pDataFinal)
        {
            IList<RegistrosTipo60> RegistrosTipo60 = RegistrosTipo60Controller.Instancia.PegaRegistros(pDataInicial, pDataFinal);

            foreach (var item in RegistrosTipo60)
            {
                Tipo60 registro60 = new Tipo60();

                registro60.Registro = item.Registro;

                objValidaPR.ListaRegistro60.Add(registro60);
            }
        }

        private void AtribuirRegistroTipo54(Nota objNota, NotaItem objNotaItem)
        {
            Tipo54 registro54 = new Tipo54();

            registro54.CNPJ = objNota.PessoaCNPJCPF;
            registro54.Modelo = objNota.ModeloDocto;
            registro54.Serie = objNota.Serie;
            registro54.NumeroNF = objNota.Numero;
            registro54.CFOP = Convert.ToInt32(objNotaItem.CFOP.CFOP);
            registro54.CST = objNotaItem.SitTrib ?? objNotaItem.TAG_CST;
            registro54.NumeroItem = objNotaItem.Sequencia;
            registro54.CodigoProduto = objNotaItem.Produto.Codigo;
            registro54.Quantidade = objNotaItem.Quantidade;
            registro54.ValorProduto = objNotaItem.Total + objNotaItem.RAT_Acrescimo;
            registro54.ValorDesconto = objNotaItem.RAT_Desconto;
            registro54.BaseCalculoIcms = objNotaItem.BaseICMS;
            registro54.BaseCalculoIcms_ST = objNotaItem.BaseICMSSubst;
            registro54.ValorIPI = objNotaItem.VIPI_O14;
            registro54.AliquotaIcms = objNotaItem.AliqICMSNormal;

            objValidaPR.ListaRegistro54.Add(registro54);
        }

        private void MontarRegistoTipo75(DateTime pDataInicial, DateTime pDataFinal, NotaItem objNotaItem)
        {
            Tipo75 registro75 = new Tipo75();

            registro75.DataInicial = pDataInicial;
            registro75.DataFinal = pDataFinal;
            registro75.CodigoProduto = objNotaItem.Produto.Codigo == null ? "" : objNotaItem.Produto.Codigo;
            registro75.CodigoNCM = objNotaItem.Produto.NCM == null ? "" : objNotaItem.Produto.NCM;
            registro75.DescricaoProduto = objNotaItem.Produto.Nome == null ? "" : objNotaItem.Produto.Nome;
            registro75.Unidade = objNotaItem.Unidade ?? "un";
            registro75.AliquotaIPI = objNotaItem.PIPI_O13;
            registro75.AliquotaICMS = objNotaItem.AliqICMSNormal;
            registro75.ReducaoBaseCalculo = objNotaItem.PRedBC_N14;
            registro75.BaseCalculoST = objNotaItem.BaseICMSSubst;

            objValidaPR.ListaRegistro75.Add(registro75);
        }

        private void MontarRegistoTipo75(DateTime pDataInicial, DateTime pDataFinal, InventarioProduto inventario)
        {
            Tipo75 registro75 = new Tipo75();

            registro75.DataInicial = pDataInicial;
            registro75.DataFinal = pDataFinal;
            registro75.CodigoProduto = inventario.Produto.Codigo;
            registro75.CodigoNCM = inventario.Produto.NCM;
            registro75.DescricaoProduto = inventario.Produto.Nome == null ? "" : inventario.Produto.Nome;
            registro75.Unidade = inventario.Produto.Unidade.Sigla ?? "un";
            registro75.AliquotaIPI = 0;
            registro75.AliquotaICMS = 0;
            registro75.ReducaoBaseCalculo = 0;
            registro75.BaseCalculoST = 0;

            objValidaPR.ListaRegistro75.Add(registro75);
        }

        private int ValidarCFOP(string cfop, bool entrada)
        {
            var temp = cfop;
            if (entrada)
            {
                if (cfop.Substring(0, 1).Equals("5"))
                    temp = "1" + cfop.Substring(cfop.Length - 3, 3);
                else if (cfop.Substring(0, 1).Equals("6"))
                    temp = "2" + cfop.Substring(cfop.Length - 3, 3);
                else if (cfop.Substring(0, 1).Equals("7"))
                    temp = "3" + cfop.Substring(cfop.Length - 3, 3);
            }

            return temp.ToInt();
        }

        private void MontarRegistroTipo50(Nota objNota, NotaICMS objNotaIcms)
        {
            var registro50 = new Tipo50();

            registro50.CNPJ = objNota.PessoaCNPJCPF;

            if (registro50.CNPJ.Length != 14)
                registro50.InscricaoEstadual = "ISENTO";
            else
                registro50.InscricaoEstadual = objNota.PessoaInscRG.IsNullOrEmpty() ? "ISENTO" : objNota.PessoaInscRG;

            registro50.Data = objNota.Dt;
            registro50.Estado = objNota.PessoaUF;
            registro50.Modelo = objNota.ModeloDocto;
            registro50.Serie = objNota.Serie;
            registro50.NumeroNF = objNota.Numero;
            registro50.CFOP = ValidarCFOP(objNotaIcms.CFOP.CFOP, objNota.TipoNota.Ent_Sai == 1); //Convert.ToInt32(objNotaIcms.CFOP.CFOP);
            registro50.Emitente = (OrigemNFType) objNota.TipoNota.NFOrigem == OrigemNFType.Própria ? "P" : "T";
            registro50.ValorTotal = objNotaIcms.ValorTotalProdutos;
            registro50.BaseCalculoICMS = objNotaIcms.BaseICMS;
            registro50.ValorICMS = objNotaIcms.ValorICMS;
            registro50.ValorIsentoICMS = objNotaIcms.ValorIsentoICMS;
            registro50.ValorOutras = objNotaIcms.ValorOutroICMS + objNotaIcms.ValorIPI;
            registro50.Aliquota = objNotaIcms.AliqICMSNormal;
            registro50.Situacao = objNota.CancDt == null ? "N" : "S";

            objValidaPR.ListaRegistro50.Add(registro50);

            if (objNotaIcms.ValorSubst > 0)
            {
                Tipo53 registro53 = new Tipo53();

                registro53.CNPJ = objNota.PessoaCNPJCPF;
                registro53.InscricaoEstadual = objNota.PessoaInscRG;
                registro53.Data = objNota.Dt;
                registro53.Estado = objNota.PessoaUF;
                registro53.Modelo = objNota.ModeloDocto;
                registro53.Serie = objNota.Serie;
                registro53.NumeroNF = objNota.Numero;
                registro53.CFOP = Convert.ToInt32(objNotaIcms.CFOP.CFOP);
                registro53.Emitente = (OrigemNFType) objNota.TipoNota.NFOrigem == OrigemNFType.Própria ? "P" : "T";
                registro53.BaseCalculoICMS_ST = objNotaIcms.BaseSubst;
                registro53.ValorICMSRetido = objNotaIcms.ValorSubst;
                registro53.ValorDespesas = 0;
                registro53.Situacao = objNota.CancDt == null ? "N" : "S";
                registro53.CodigoAntecipacao = " ";

                objValidaPR.ListaRegistro53.Add(registro53);
            }
        }

        private void MontarRegistroTipo11(Filial pFilial)
        {
            //Atribui as informações referente ao Registro Tipo 11
            objValidaPR.Registro11.Logradouro = pFilial.Endereco;
            objValidaPR.Registro11.Numero = Convert.ToInt32(pFilial.Numero);
            objValidaPR.Registro11.Complemento = "";
            objValidaPR.Registro11.Bairro = pFilial.Bairro;
            objValidaPR.Registro11.Cep = pFilial.CEP;
            objValidaPR.Registro11.Contato = pFilial.Responsavel;
            objValidaPR.Registro11.Telefone = pFilial.Telefone;
        }

        private void MontarRegistroTipo90(Filial pFilial)
        {
            //Atribui as informações referente ao Registro Tipo 90
            objValidaPR.Registro90.CNPJ = pFilial.CNPJ;
            objValidaPR.Registro90.InscricaoEstadual = pFilial.Inscricao;
        }

        private void MontarRegistroTipo10(Filial pFilial, ref DateTime pDataInicial, ref DateTime pDataFinal)
        {
            //Atribui as informações referente ao Registro Tipo 10
            objValidaPR.Registro10.CNPJ = pFilial.CNPJ;
            objValidaPR.Registro10.InscricaoEstadual = pFilial.Inscricao;
            objValidaPR.Registro10.Contribuinte = pFilial.Nome;
            objValidaPR.Registro10.Municipio = pFilial.Cidade.Nome;
            objValidaPR.Registro10.Estado = pFilial.Cidade.UF.Sigla;
            objValidaPR.Registro10.Fax = pFilial.Fax;
            objValidaPR.Registro10.DataInicial = pDataInicial;
            objValidaPR.Registro10.DataFinal = pDataFinal;
            objValidaPR.Registro10.CodigoFinalidade = "1";
        }

        public string Conv_TAG_CST(int aIndex)
        {
            switch (aIndex)
            {
                case 0: return "00";
                case 1: return "10";
                case 2: return "20";
                case 3: return "30";
                case 4: return "40";
                case 5: return "41";
                case 6: return "50";
                case 7: return "51";
                case 8: return "60";
                case 9: return "70";
                case 10: return "90";
                case 11: return "Part";
                case 12: return "ST";
                case 13: return "101";
                case 14: return "102";
                case 15: return "103";
                case 16: return "201";
                case 17: return "202";
                case 18: return "203";
                case 19: return "300";
                case 20: return "400";
                case 21: return "500";
                case 22: return "900";
            }

            return "";
        }
    }
}