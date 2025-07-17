using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FiscalPrinterBematech;
using cwkGestao.Negocio.Interfaces;
using System.Threading;
using cwkGestao.Modelo;
using System.Globalization;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Negocio
{
    public class ValidaPRTipo60
    {
        ICwkAviso i;     

        public ValidaPRTipo60(ICwkAviso _i) 
        {
            i = _i;
        }

        private void TiraGravaRegistrosTipo60()
        {
            var Controller = cwkGestao.Negocio.RegistrosTipo60Controller.Instancia;
            int IRetorno = 0;

            IRetorno = BemaFI32.Bematech_FI_RegistrosTipo60();
            BemaFI32.Analisa_iRetorno(IRetorno);

            if (IRetorno == 1)
                Controller.LeRegistrosTipo60();

            //IRetorno = BemaFI32.Bematech_FI_RelatorioTipo60Mestre();
            //BemaFI32.Analisa_iRetorno(IRetorno);

            //if (IRetorno == 1)
            //    Controller.LeRegistroTipo60M();

            //IRetorno = BemaFI32.Bematech_FI_RelatorioTipo60Analitico();
            //BemaFI32.Analisa_iRetorno(IRetorno);

            //if (IRetorno == 1)
            //    Controller.LeRegistroTipo60A();

            i.Fechar();
            Thread.CurrentThread.Abort();
        }
        public static void GeraRegistro60R(DateTime dtInicial, DateTime dtFinal)
        {
            DateTime ultimoDia = new DateTime(dtFinal.Year, dtFinal.Month, DateTime.DaysInMonth(dtFinal.Year, dtFinal.Month));
            //Deleta Registros Do tipo R que são totalizadores mensais
            IList<RegistrosTipo60> registrosTipo60R = RegistrosTipo60Controller.Instancia.PegaRegistros(dtInicial, ultimoDia);
            registrosTipo60R = registrosTipo60R.Where(x => x.TipoRegistro == "R").ToList();
            RegistrosTipo60Controller.Instancia.RegisterDeleteIntoTransaction(registrosTipo60R);
            RegistrosTipo60Controller.Instancia.CommitUnitOfWorkTransaction();
            IList<pxyRegistro60R> reg60R = RegistrosTipo60Controller.Instancia.GetPxyRegistro60R(dtInicial.Date, dtFinal.Date);
            if (reg60R.Count > 0)
	        {
                foreach (pxyRegistro60R itemReg60R in reg60R)
	            {
            		RegistrosTipo60 gera60R = new RegistrosTipo60();
                    gera60R.Dt = dtFinal.Date;
                    gera60R.TipoRegistro = "R";
                    gera60R.Registro = itemReg60R.Tipo + itemReg60R.SubTipo + itemReg60R.Referencia + itemReg60R.CodigoProduto + itemReg60R.Quantidade
                                     + itemReg60R.ValorProduto + itemReg60R.BaseCalculoIcms + itemReg60R.AliquitaProduto + itemReg60R.Brancos;
                    registrosTipo60R.Add(gera60R);	
	            }
                RegistrosTipo60Controller.Instancia.RegisterNewIntoTransaction(registrosTipo60R);
                RegistrosTipo60Controller.Instancia.CommitUnitOfWorkTransaction();
	        }
        }

        public static void GeraRegistros60MFD(CupomFiscal _cupomFiscal, string caminho, DateTime dtInicial, DateTime dtFinal, Filial filial)
        {
            caminho = caminho + @"\Sintegra.txt";
            _cupomFiscal.Bematech_FI_RelatorioSintegraMFD(63, caminho, dtFinal.Month.ToString(), dtFinal.Year.ToString(),
                            filial.Nome, filial.Endereco, filial.Numero,
                            filial.Bairro, filial.Bairro, Convert.ToString(filial.Cidade),
                            filial.CEP, filial.Telefone, filial.Fax,
                            filial.Email
                            );

            string registro;
            System.IO.StreamReader arquivoRegistro60 = new System.IO.StreamReader(caminho);
            IList<RegistrosTipo60> registrosTipo60 = RegistrosTipo60Controller.Instancia.PegaRegistros(dtInicial, dtFinal);
            if (registrosTipo60.Count > 0)
            {
                RegistrosTipo60Controller.Instancia.RegisterDeleteIntoTransaction(registrosTipo60);
                RegistrosTipo60Controller.Instancia.CommitUnitOfWorkTransaction();
            }

            while ((registro = arquivoRegistro60.ReadLine()) != null)
            {
                if (!String.IsNullOrEmpty(registro))
                {
                    if (registro.Length > 2 && registro.Substring(0, 2) == "60")
                    {
                        string tipoRegistro = registro.Substring(2, 1);
                        if (tipoRegistro == "M" || tipoRegistro == "A" /*|| tipoRegistro == "R"*/)
                        {
                            int valor = 0;
                            if (tipoRegistro == "A")
	                        {
                        		string svalor =  registro.Substring(36, 12);
                                try
                                {
                                    valor = Convert.ToInt32(svalor);
                                }
                                catch (Exception)
                                {
                                    valor = 0;
                                }
	                        }
                            if (tipoRegistro != "A" || (tipoRegistro == "A") && (valor > 0))
                            {
                                RegistrosTipo60 registroTipo60 = new RegistrosTipo60();
                                DateTime dt;
                                //if (tipoRegistro != "R")
                                //{
                                    string dts = registro.Substring(3, 8);
                                    dt = DateTime.ParseExact(dts, "yyyyMMdd", CultureInfo.InvariantCulture);
                                //}
                                //else
                                //{
                                //    dt = dtFinal;
                                //}
                                registroTipo60.Dt = dt;
                                registroTipo60.TipoRegistro = tipoRegistro;
                                registroTipo60.Registro = registro;
                                RegistrosTipo60Controller.Instancia.Salvar(registroTipo60, Acao.Incluir);   
                            }
                        }
                    }
                }
            }
            arquivoRegistro60.Close();
            System.Console.ReadLine();
        }

        public void ReducaoZ()
        {
            int IRetorno;

            IRetorno = BemaFI32.Bematech_FI_ReducaoZ("", "");
            BemaFI32.Analisa_iRetorno(IRetorno);

            TiraGravaRegistrosTipo60();
        }
    }
}
