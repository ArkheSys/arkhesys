using System;
using System.Text;

using cwkGestao.Integracao.ACBr.Core;
using cwkGestao.Integracao.ACBr.Core.Boleto;

namespace cwkGestao.Integracao.ACBr.Boleto
{
    public sealed partial class ACBrBoleto : ACBrLibHandle
    {
        #region Constructors

        public ACBrBoleto(string eArqConfig = "", string eChaveCrypt = "") : base("ACBrBoleto32.dll")
        {

            var inicializar = GetMethod<Boleto_Inicializar>();
            var ret = ExecuteMethod(() => inicializar(ref libHandle, ToUTF8(eArqConfig), ToUTF8(eChaveCrypt)));

            CheckResult(ret);
        }

        #endregion Constructors

        #region Properties
        
        public string Nome
        {
            get
            {
                var bufferLen = BUFFER_LEN;
                var buffer = new StringBuilder(bufferLen);

                var method = GetMethod<Boleto_Nome>();
                var ret = ExecuteMethod(() => method(libHandle, buffer, ref bufferLen));

                CheckResult(ret);

                return ProcessResult(buffer, bufferLen);
            }
        }

        public string Versao
        {
            get
            {
                var bufferLen = BUFFER_LEN;
                var buffer = new StringBuilder(bufferLen);

                var method = GetMethod<Boleto_Versao>();
                var ret = ExecuteMethod(() => method(libHandle, buffer, ref bufferLen));

                CheckResult(ret);

                return ProcessResult(buffer, bufferLen);
            }
        }

        #endregion Properties

        #region Methods

        #region Ini

        public override void ConfigGravar(string eArqConfig = "")
        {
            var gravarIni = GetMethod<Boleto_ConfigGravar>();
            var ret = ExecuteMethod(() => gravarIni(libHandle, ToUTF8(eArqConfig)));

            CheckResult(ret);
        }

        public override void ConfigLer(string eArqConfig = "")
        {
            var lerIni = GetMethod<Boleto_ConfigLer>();
            var ret = ExecuteMethod(() => lerIni(libHandle, ToUTF8(eArqConfig)));

            CheckResult(ret);
        }

        public override T ConfigLerValor<T>(ACBrSessao eSessao, string eChave)
        {
            var method = GetMethod<Boleto_ConfigLerValor>();

            var bufferLen = BUFFER_LEN;
            var pValue = new StringBuilder(bufferLen);
            var ret = ExecuteMethod(() => method(libHandle, ToUTF8(eSessao.ToString()), ToUTF8(eChave), pValue, ref bufferLen));
            CheckResult(ret);

            var value = ProcessResult(pValue, bufferLen);
            return ConvertValue<T>(value);
        }

        public override void ConfigGravarValor(ACBrSessao eSessao, string eChave, object value)
        {
            if (value == null) return;

            var method = GetMethod<Boleto_ConfigGravarValor>();
            var propValue = ConvertValue(value);

            var ret = ExecuteMethod(() => method(libHandle, ToUTF8(eSessao.ToString()), ToUTF8(eChave), ToUTF8(propValue)));
            CheckResult(ret);
        }

        #endregion Ini

        public override void ImportarConfig(string eArqConfig = "")
        {
            var importarConfig = GetMethod<Boleto_ConfigImportar>();
            var ret = ExecuteMethod(() => importarConfig(libHandle, ToUTF8(eArqConfig)));

            CheckResult(ret);
        }

        public override string ExportarConfig()
        {
            var bufferLen = BUFFER_LEN;
            var buffer = new StringBuilder(bufferLen);

            var method = GetMethod<Boleto_ConfigExportar>();
            var ret = ExecuteMethod(() => method(libHandle, buffer, ref bufferLen));

            CheckResult(ret);

            return ProcessResult(buffer, bufferLen);
        }

        public string ConfigurarDados(string eArquivoIni)
        {
            var bufferLen = BUFFER_LEN;
            var buffer = new StringBuilder(bufferLen);

            var method = GetMethod<Boleto_ConfigurarDados>();
            var ret = ExecuteMethod(() => method(libHandle, ToUTF8(eArquivoIni)));

            CheckResult(ret);

            return ProcessResult(buffer, bufferLen);
        }

        public void IncluirTitulos(string eArquivoIni, BoletoTpSaida? eTpSaida = null)
        {
            var tpSaida = $"{(eTpSaida.HasValue ? (char)eTpSaida.Value : ' ')}";
            var method = GetMethod<Boleto_IncluirTitulos>();
            var ret = ExecuteMethod(() => method(libHandle, ToUTF8(eArquivoIni), ToUTF8(tpSaida)));

            CheckResult(ret);
        }

        public void LimparLista()
        {
            var method = GetMethod<Boleto_LimparLista>();
            var ret = ExecuteMethod(() => method(libHandle));
            CheckResult(ret);
        }

        public int TotalTitulosLista()
        {
            var method = GetMethod<Boleto_TotalTitulosLista>();
            var ret = ExecuteMethod(() => method(libHandle));

            CheckResult(ret);
            return ret;
        }

        public void Imprimir(string eNomeImpressora = "")
        {
            var method = GetMethod<Boleto_Imprimir>();
            var ret = ExecuteMethod(() => method(libHandle, ToUTF8(eNomeImpressora)));
            CheckResult(ret);
        }

        public void ImprimirBoleto(int indice, string eNomeImpressora = "")
        {
            var method = GetMethod<Boleto_ImprimirBoleto>();
            var ret = ExecuteMethod(() => method(libHandle, indice, ToUTF8(eNomeImpressora)));
            CheckResult(ret);
        }

        public void GerarPDF()
        {
            var method = GetMethod<Boleto_GerarPDF>();
            var ret = ExecuteMethod(() => method(libHandle));
            CheckResult(ret);
        }

        public void GerarHTML()
        {
            var method = GetMethod<Boleto_GerarHTML>();
            var ret = ExecuteMethod(() => method(libHandle));
            CheckResult(ret);
        }

        public void GerarRemessa(string eDir, int eNumArquivo, string eNomeArq)
        {
            var method = GetMethod<Boleto_GerarRemessa>();
            var ret = ExecuteMethod(() => method(libHandle, ToUTF8(eDir), eNumArquivo, ToUTF8(eNomeArq)));

            CheckResult(ret);
        }

        public void LerRetorno(string eDir, string eNomeArq)
        {
            var method = GetMethod<Boleto_LerRetorno>();
            var ret = ExecuteMethod(() => method(libHandle, ToUTF8(eDir), ToUTF8(eNomeArq)));

            CheckResult(ret);
        }

        public void EnviarEmail(string ePara, string eAssunto, string eMensagem, string eCC)
        {
            var method = GetMethod<Boleto_EnviarEmail>();
            var ret = ExecuteMethod(() => method(libHandle, ToUTF8(ePara), ToUTF8(eAssunto), ToUTF8(eMensagem), ToUTF8(eCC)));

            CheckResult(ret);
        }

        public void SetDiretorioArquivo(string eDir, string eArq)
        {
            var method = GetMethod<Boleto_SetDiretorioArquivo>();
            var ret = ExecuteMethod(() => method(libHandle, ToUTF8(eDir), ToUTF8(eArq)));

            CheckResult(ret);
        }

        public string[] ListaBancos()
        {
            var bufferLen = BUFFER_LEN;
            var buffer = new StringBuilder(bufferLen);

            var method = GetMethod<Boleto_ListaBancos>();
            var ret = ExecuteMethod(() => method(libHandle, buffer, ref bufferLen));

            CheckResult(ret);

            return ProcessResult(buffer, bufferLen).Split('|');
        }

        public string[] ListaCaractTitulo()
        {
            var bufferLen = BUFFER_LEN;
            var buffer = new StringBuilder(bufferLen);

            var method = GetMethod<Boleto_ListaCaractTitulo>();
            var ret = ExecuteMethod(() => method(libHandle, buffer, ref bufferLen));

            CheckResult(ret);

            return ProcessResult(buffer, bufferLen).Split('|');
        }

        public string[] ListaOcorrencias()
        {
            var bufferLen = BUFFER_LEN;
            var buffer = new StringBuilder(bufferLen);

            var method = GetMethod<Boleto_ListaOcorrencias>();
            var ret = ExecuteMethod(() => method(libHandle, buffer, ref bufferLen));

            CheckResult(ret);

            return ProcessResult(buffer, bufferLen).Split('|');
        }

        public string[] ListaOcorrenciasEX()
        {
            var bufferLen = BUFFER_LEN;
            var buffer = new StringBuilder(bufferLen);

            var method = GetMethod<Boleto_ListaOcorrenciasEX>();
            var ret = ExecuteMethod(() => method(libHandle, buffer, ref bufferLen));

            CheckResult(ret);

            return ProcessResult(buffer, bufferLen).Split('|');
        }

        public int TamNossoNumero(string eCarteira, string enossoNumero, string eConvenio)
        {
            var method = GetMethod<Boleto_TamNossoNumero>();
            var ret = ExecuteMethod(() => method(libHandle, ToUTF8(eCarteira), ToUTF8(enossoNumero), ToUTF8(eConvenio)));

            CheckResult(ret);

            return ret;
        }

        public string CodigosMoraAceitos()
        {
            var bufferLen = BUFFER_LEN;
            var buffer = new StringBuilder(bufferLen);

            var method = GetMethod<Boleto_CodigosMoraAceitos>();
            var ret = ExecuteMethod(() => method(libHandle, buffer, ref bufferLen));

            CheckResult(ret);

            return ProcessResult(buffer, bufferLen);
        }

        public void SelecionaBanco(string eCodBanco)
        {
            var method = GetMethod<Boleto_SelecionaBanco>();
            var ret = ExecuteMethod(() => method(libHandle, ToUTF8(eCodBanco)));

            CheckResult(ret);
        }

        public string MontarNossoNumero(int eIndex)
        {
            var bufferLen = BUFFER_LEN;
            var buffer = new StringBuilder(bufferLen);

            var method = GetMethod<Boleto_MontarNossoNumero>();
            var ret = ExecuteMethod(() => method(libHandle, eIndex, buffer, ref bufferLen));

            CheckResult(ret);

            return ProcessResult(buffer, bufferLen);
        }

        public string RetornaLinhaDigitavel(int eIndex)
        {
            var bufferLen = BUFFER_LEN;
            var buffer = new StringBuilder(bufferLen);

            var method = GetMethod<Boleto_RetornaLinhaDigitavel>();
            var ret = ExecuteMethod(() => method(libHandle, eIndex, buffer, ref bufferLen));

            CheckResult(ret);

            return ProcessResult(buffer, bufferLen);
        }

        public string RetornaCodigoBarras(int eIndex)
        {
            var bufferLen = BUFFER_LEN;
            var buffer = new StringBuilder(bufferLen);

            var method = GetMethod<Boleto_RetornaCodigoBarras>();
            var ret = ExecuteMethod(() => method(libHandle, eIndex, buffer, ref bufferLen));

            CheckResult(ret);

            return ProcessResult(buffer, bufferLen);
        }

        #region Private Methods

        protected override void InitializeMethods()
        {
            AddMethod<Boleto_Inicializar>("Boleto_Inicializar");
            AddMethod<Boleto_Finalizar>("Boleto_Finalizar");
            AddMethod<Boleto_Nome>("Boleto_Nome");
            AddMethod<Boleto_Versao>("Boleto_Versao");
            AddMethod<Boleto_UltimoRetorno>("Boleto_UltimoRetorno");
            AddMethod<Boleto_ConfigImportar>("Boleto_ConfigImportar");
            AddMethod<Boleto_ConfigExportar>("Boleto_ConfigExportar");
            AddMethod<Boleto_ConfigLer>("Boleto_ConfigLer");
            AddMethod<Boleto_ConfigGravar>("Boleto_ConfigGravar");
            AddMethod<Boleto_ConfigLerValor>("Boleto_ConfigLerValor");
            AddMethod<Boleto_ConfigGravarValor>("Boleto_ConfigGravarValor");
            AddMethod<Boleto_ConfigurarDados>("Boleto_ConfigurarDados");
            AddMethod<Boleto_IncluirTitulos>("Boleto_IncluirTitulos");
            AddMethod<Boleto_LimparLista>("Boleto_LimparLista");
            AddMethod<Boleto_TotalTitulosLista>("Boleto_TotalTitulosLista");
            AddMethod<Boleto_Imprimir>("Boleto_Imprimir");
            AddMethod<Boleto_ImprimirBoleto>("Boleto_ImprimirBoleto");
            AddMethod<Boleto_GerarPDF>("Boleto_GerarPDF");
            AddMethod<Boleto_GerarHTML>("Boleto_GerarHTML");
            AddMethod<Boleto_GerarRemessa>("Boleto_GerarRemessa");
            AddMethod<Boleto_LerRetorno>("Boleto_LerRetorno");
            AddMethod<Boleto_EnviarEmail>("Boleto_EnviarEmail");
            AddMethod<Boleto_SetDiretorioArquivo>("Boleto_SetDiretorioArquivo");
            AddMethod<Boleto_ListaBancos>("Boleto_ListaBancos");
            AddMethod<Boleto_ListaCaractTitulo>("Boleto_ListaCaractTitulo");
            AddMethod<Boleto_ListaOcorrencias>("Boleto_ListaOcorrencias");
            AddMethod<Boleto_ListaOcorrenciasEX>("Boleto_ListaOcorrenciasEX");
            AddMethod<Boleto_TamNossoNumero>("Boleto_TamNossoNumero");
            AddMethod<Boleto_CodigosMoraAceitos>("Boleto_CodigosMoraAceitos");
            AddMethod<Boleto_SelecionaBanco>("Boleto_SelecionaBanco");
            AddMethod<Boleto_MontarNossoNumero>("Boleto_MontarNossoNumero");
            AddMethod<Boleto_RetornaLinhaDigitavel>("Boleto_RetornaLinhaDigitavel");
            AddMethod<Boleto_RetornaCodigoBarras>("Boleto_RetornaCodigoBarras");
        }

        protected override void FinalizeLib()
        {
            var finalizar = GetMethod<Boleto_Finalizar>();
            var ret = ExecuteMethod(() => finalizar(libHandle));
            CheckResult(ret);
        }

        protected override string GetUltimoRetorno(int iniBufferLen = 0)
        {
            var bufferLen = iniBufferLen < 1 ? BUFFER_LEN : iniBufferLen;
            var buffer = new StringBuilder(bufferLen);
            var ultimoRetorno = GetMethod<Boleto_UltimoRetorno>();

            if (iniBufferLen < 1)
            {
                ExecuteMethod(() => ultimoRetorno(libHandle, buffer, ref bufferLen));
                if (bufferLen <= BUFFER_LEN) return FromUTF8(buffer);

                buffer.Capacity = bufferLen;
            }

            ExecuteMethod(() => ultimoRetorno(libHandle, buffer, ref bufferLen));
            return FromUTF8(buffer);
        }

        #endregion Private Methods

        #endregion Methods

        protected override void Dispose(bool disposing)
        {
        }
    }
}