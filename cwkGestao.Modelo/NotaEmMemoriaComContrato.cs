using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class NotaEmMemoriaComContrato
    {
        public NotaEmMemoriaComContrato()
        {

        }

        public NotaEmMemoriaComContrato(int idNota, int idDocumento, int idContrato,int idPessoa, string status)
        {
            this.idNota = idNota;
            this.idDocumento = idDocumento;
            this.idContrato = idContrato;
            this.idPessoa = idPessoa;
            this.status = status;
        }

        public bool selecionado { get; set; }

        public DateTime dataDeVencimento { get; set; }

        public string razaoSocial { get; set; }

        public string fantasia { get; set; }

        public decimal valor { get; set; }

        public string numDocFinanceiro { get; set; }

        public IList<string> emailsVinculado { get; set; }

        public string emailExibicao
        {
            get 
            {
                return emailsVinculado.FirstOrDefault();
            }
        }

        public DateTime? dataEmailEnviado { get; set; }

        public int idNota { get; set; }

        public int idDocumento { get; set; }

        public int idContrato { get; set; }

        public int idPessoa { get; set; }

        public string status { get; set; }
    }
}
