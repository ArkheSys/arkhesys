using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class StatusOS : ModeloBase
    {
        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 80, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "Descrição", 200)]
        public virtual string Descricao { get; set; }

        [InformacaoExibicao(3, "Fechado", 200)]
        public virtual bool BFechado { get; set; }

        [InformacaoExibicao(3, "Faturar", 200)]
        public virtual bool BFaturar { get; set; }

        [InformacaoExibicao(3, "Agendar", 200)]
        public virtual bool BAgendar { get; set; }

        [InformacaoExibicao(4, "Permissão de Gerente", 200)]
        public virtual bool BPermissaoGerente { get; set; }

        [InformacaoExibicao(5, "Padrão", 200)]
        public virtual bool BPadrao { get; set; }

        [InformacaoExibicao(5, "E-mail Automático", 200)]
        public virtual bool EmailAutomatico { get; set; }

        
        private string _EmailAutomaticoAssunto;
        [InformacaoExibicao(5, "Assunto E-mail Automático", 200)]
        public virtual string EmailAutomaticoAssunto { 
            get{ return _EmailAutomaticoAssunto;}
            set{ _EmailAutomaticoAssunto = value;} }

        private string _EmailAutomaticoTexto;
        public virtual string EmailAutomaticoTexto {
            get { return _EmailAutomaticoTexto; }
            set { _EmailAutomaticoTexto = value; } 
        
        }


    }
}
