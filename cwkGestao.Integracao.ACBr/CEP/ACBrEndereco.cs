using System.IO;

using ACBrLib.CEP;
using ACBrLib.Core.CEP;

namespace cwkGestao.Integracao.ACBr.CEP
{
    public class ACBrEndereco
    {
        private ACBrCEP ACBrCEP;

        public ACBrEndereco()
        {
            ACBrCEP = new ACBrCEP();
            LoadConfig();
        }

        private void LoadConfig(string file = "ACBrLib.ini")
        {
            //if (!File.Exists(file))
            //{
                SalvarConfig();
                //return;
            //}
        }

        private void SalvarConfig()
        {
            ACBrCEP.Config.WebService = WebService.wsViaCep;
            ACBrCEP.Config.ChaveAcesso = string.Empty;
            ACBrCEP.Config.Usuario = string.Empty;
            ACBrCEP.Config.Senha = string.Empty;
            ACBrCEP.Config.PesquisarIBGE = false;

            ACBrCEP.Config.Proxy.Servidor = string.Empty;
            ACBrCEP.Config.Proxy.Porta = string.Empty;
            ACBrCEP.Config.Proxy.Usuario = string.Empty;
            ACBrCEP.Config.Proxy.Senha = string.Empty;

            ACBrCEP.ConfigGravar();
        }

        public string BuscarCep(string CEP)
        {
            return ACBrCEP.buscarPorCep(CEP);
        }
    }
}
