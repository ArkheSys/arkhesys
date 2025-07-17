using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace cwkGestao.Importacao.Nutricenter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            cwkControleUsuario.Facade.setBancoDados(1);
            if (!cwkControleUsuario.Facade.ValidaDAL())
            {
                Application.Exit();
            }
            else
            {
                BLL.Cw_Usuario bllUsuario = BLL.Cw_Usuario.getInstance();
                if (!bllUsuario.getPossuiUsuario())
                {
                    global::Modelo.cwkGlobal.objUsuarioLogado = new global::Modelo.Cw_Usuario();
                    global::Modelo.cwkGlobal.objUsuarioLogado.Tipo = 0;
                    cwkControleUsuario.Facade.ChamaGridUsuario();
                }
            }

            //if (cwkControleUsuario.Facade.ChamaAutenticacao(LicenceLibrary.Sistema.Gestão, "1.44.056", true))
            //{
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            //}
        }
    }
}
