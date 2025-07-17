using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MO = Modelo;
using System.Reflection;
using System.IO;

namespace cwkGestao.Importacao.Cwork
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string arquivoExe = Assembly.GetEntryAssembly().Location;
            MO.cwkGlobal.DirApp = Path.Combine(Path.GetDirectoryName(arquivoExe), "");
            cwkControleUsuario.Facade.setBancoDados(1);
            if (!cwkControleUsuario.Facade.ValidaDAL())
            {
                Application.Exit();
            }

            Application.Run(new Form2());
        }
    }
}
