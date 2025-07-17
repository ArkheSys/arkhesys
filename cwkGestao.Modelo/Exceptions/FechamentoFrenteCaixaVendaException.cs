using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Exceptions
{
    public class FechamentoFrenteCaixaException : Exception
    {
        private string mensagemAdicional;

        public FechamentoFrenteCaixaException(string mensagem) : base(mensagem)
        {
            mensagemAdicional = mensagem;
        }

        public FechamentoFrenteCaixaException(string mensagem, Exception innerException)
            : base(mensagem, innerException)
        {
            mensagemAdicional = mensagem;
        }

        public override string Message
        {
            get
            {
                return "Erro ao finalizar venda: " + mensagemAdicional;
            }
        }
    }
}
