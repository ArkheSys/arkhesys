using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Auxiliares
{
    public class UsuarioProvider
    {
        public string GetLoginUsuarioLogado()
        {
            try
            {
                return cwkControleUsuario.Facade.getUsuarioLogado.Login;
            }
            catch
            {
                return "ERRO";
            }
        }
    }
}
