using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao(0, "Telefone", 100)]
    public class PessoaTelefone : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        public virtual int IDPessoa { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual int Sequencia { get; set; }

        [InformacaoExibicao(1, "Tipo", 100)]
        public virtual string Tipo { get; set; }

        [InformacaoExibicao(0, "Número", 80)]
        public virtual string Numero { get; set; }

        [InformacaoExibicao(2, "Contato", 100)]
        public virtual string Contato { get; set; }

        public virtual bool BPrincipal { get; set; }


        private string _Operadora;
        public virtual string Operadora {
            get
            {
                return _Operadora;
            }
            set
            {
                _Operadora = value;
                switch (Operadora)
                {
                    case "0":
                        OperadoraNome = "";
                        break;
                    case "1":
                        OperadoraNome = "TIM";
                        break;
                    case "2":
                        OperadoraNome = "Claro";
                        break;
                    case "3":
                        OperadoraNome = "CTBC Telecom";
                        break;
                    case "4":
                        OperadoraNome = "Oi";
                        break;
                    case "5":
                        OperadoraNome = "Vivo";
                        break;
                    case "6":
                        OperadoraNome = "Sercomtel";
                        break;
                    case "7":
                        OperadoraNome = "Nextel";
                        break;
                }
            }
        }


        [InformacaoExibicao(3, "Operadora", 100)]
        public virtual string OperadoraNome { get; set; } 
    }
}
		