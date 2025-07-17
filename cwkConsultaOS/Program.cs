using cwkConsultaOS;
using MO = Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace cwkConsultaOS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ValidarConnectionString();
            ExecutarSistema();
        }

        private static void ValidarConnectionString()
        {
            cwkControleUsuario.Facade.setBancoDados(1);

            if (!cwkControleUsuario.Facade.ValidaDAL())
            {
                Application.Exit();
            }
        }

        private static void ExecutarSistema()
        {
            try
            {
                Application.Run(new GridOSOrdemServico());
            }
            catch (Exception w)
            {
                 MessageBox.Show("Não foi possível inicializar o sistema.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
