using System.Runtime.InteropServices;
using System;
using System.Windows.Forms;


namespace cwkGestao.Negocio.Utils
{
    public static class InfoImpressora
    {
        [DllImport("winspool.drv", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool GetPrinter(IntPtr hPrinter, uint dwLevel, IntPtr pPrinter, uint dwBuf, ref uint dwNeeded);

        [DllImport("winspool.drv", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int OpenPrinter(string pPrinterName, out IntPtr phPrinter, ref PRINTER_DEFAULTS pDefault);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        struct PRINTER_DEFAULTS
        {
            public IntPtr pDatatype;
            public IntPtr pDevMode;
            public int DesiredAccess;
        }

        [DllImport("winspool.drv", SetLastError = true)]
        static extern int ClosePrinter(IntPtr hPrinter);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct PRINTER_INFO_2
        {
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pServerName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pPrinterName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pShareName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pPortName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pDriverName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pComment;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pLocation;
            public IntPtr pDevMode;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pSepFile;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pPrintProcessor;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pDatatype;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pParameters;
            public IntPtr pSecurityDescriptor;
            public uint Attributes;
            public uint Priority;
            public uint DefaultPriority;
            public uint StartTime;
            public uint UntilTime;
            public uint Status;
            public uint cJobs;
            public uint AveragePPM;
        }

        public static PRINTER_INFO_2 informacoesImpressora() 
        {
            PrintDialog pd = new PrintDialog();

            if (pd.ShowDialog() == DialogResult.OK)
            {
                IntPtr printerHandle;
                PRINTER_DEFAULTS printerDefaults = new PRINTER_DEFAULTS();
                uint tam = 0;

                // Tenta abrir a impressora cujo nome foi retornado pelo PrintDialog.
                OpenPrinter(pd.PrinterSettings.PrinterName, out printerHandle, ref printerDefaults);

                bool ret = GetPrinter(printerHandle, 2, IntPtr.Zero, 0, ref tam);
                PRINTER_INFO_2 info2 = new PRINTER_INFO_2();

                if (tam > 0)
                {
                    IntPtr ptrBuffer = Marshal.AllocHGlobal((int)tam);
                    ret = GetPrinter(printerHandle, 2, ptrBuffer, tam, ref tam);
                    if (ret)
                    {
                        
                        // Obtém estrutura PRINTER_INFO_2 com as informações da impressora.
                        info2 = (PRINTER_INFO_2)Marshal.PtrToStructure(ptrBuffer, typeof(PRINTER_INFO_2));
                    }
                    Marshal.FreeHGlobal(ptrBuffer);

                }
                // Fecha.
                ClosePrinter(printerHandle);
                // Retorna porta.
                return info2;
            } return new PRINTER_INFO_2();
        }
    }
}
