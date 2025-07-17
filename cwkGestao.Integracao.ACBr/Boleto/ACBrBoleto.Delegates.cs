using System;
using System.Runtime.InteropServices;
using System.Text;

namespace cwkGestao.Integracao.ACBr.Boleto
{
    public sealed partial class ACBrBoleto
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_Inicializar(ref IntPtr handle, string eArqConfig, string eChaveCrypt);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_Finalizar(IntPtr handle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_Nome(IntPtr handle, StringBuilder buffer, ref int bufferSize);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_Versao(IntPtr handle, StringBuilder buffer, ref int bufferSize);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_UltimoRetorno(IntPtr handle, StringBuilder buffer, ref int bufferSize);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_ConfigImportar(IntPtr handle, string eArqConfig);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_ConfigExportar(IntPtr handle, StringBuilder buffer, ref int bufferSize);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_ConfigLer(IntPtr handle, string eArqConfig);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_ConfigGravar(IntPtr handle, string eArqConfig);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_ConfigLerValor(IntPtr handle, string eSessao, string eChave, StringBuilder buffer, ref int bufferSize);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_ConfigGravarValor(IntPtr handle, string eSessao, string eChave, string valor);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_ConfigurarDados(IntPtr handle, string eArquivoIni);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_IncluirTitulos(IntPtr handle, string eArquivoIni, string eTpSaida);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_LimparLista(IntPtr handle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_TotalTitulosLista(IntPtr handle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_Imprimir(IntPtr handle, string eNomeImpressora);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_ImprimirBoleto(IntPtr handle, int eIndice, string eNomeImpressora);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_GerarPDF(IntPtr handle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_GerarHTML(IntPtr handle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_GerarRemessa(IntPtr handle, string eDir, int eNumArquivo, string eNomeArq);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_LerRetorno(IntPtr handle, string eDir, string eNomeArq);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_ObterRetorno(IntPtr handle, string eDir, string eNomeArq, StringBuilder buffer, ref int bufferSize);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_EnviarEmail(IntPtr handle, string ePara, string eAssunto, string eMensagem, string eCC);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_EnviarEmailBoleto(IntPtr handle, int eIndice, string ePara, string eAssunto, string eMensagem, string eCC);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_SetDiretorioArquivo(IntPtr handle, string eDir, string eArq);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_ListaBancos(IntPtr handle, StringBuilder buffer, ref int bufferSize);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_ListaCaractTitulo(IntPtr handle, StringBuilder buffer, ref int bufferSize);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_ListaOcorrencias(IntPtr handle, StringBuilder buffer, ref int bufferSize);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_ListaOcorrenciasEX(IntPtr handle, StringBuilder buffer, ref int bufferSize);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_TamNossoNumero(IntPtr handle, string eCarteira, string enossoNumero, string eConvenio);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_CodigosMoraAceitos(IntPtr handle, StringBuilder buffer, ref int bufferSize);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_SelecionaBanco(IntPtr handle, string eCodBanco);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_MontarNossoNumero(IntPtr handle, int eIndice, StringBuilder buffer, ref int bufferSize);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_RetornaLinhaDigitavel(IntPtr handle, int eIndice, StringBuilder buffer, ref int bufferSize);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Boleto_RetornaCodigoBarras(IntPtr handle, int eIndice, StringBuilder buffer, ref int bufferSize);
    }
}