using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Exceptions
{
    public class ConfiguracaoIncorretaException : Exception
    {
        private string mensagemAdicional;

        public ConfiguracaoIncorretaException()
        { }

        public ConfiguracaoIncorretaException(string mensagem)
        {
            mensagemAdicional = mensagem;
        }

        public override string Message
        {
            get
            {
                return "O sistema não está configurado corretamente.\n" + mensagemAdicional;
            }
        }
    }
}
