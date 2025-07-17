using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyAvisos
    {
        public pxyAvisos(int idContato, string nomePessoa, string nomeUsuario, string contato,
                                   int idContatoDetalhe, string statusContato, string assunto, DateTime data, string horaInicial,
                                   string horaFinal, string observacao)
        {
            this.IDContato = idContato;
            this.NomePessoa = nomePessoa;
            this.NomeUsuario = nomeUsuario;
            this.Contato = contato;
            this.IDContatoDetalhe = idContatoDetalhe;
            this.StatusContato = statusContato;
            this.Assunto = assunto;
            this.Data = data;
            this.HoraInicial = horaInicial;
            this.HoraFinal = horaFinal;
            this.Observacao = observacao;            
        }

        public int IDContato { get; set; }
        public string NomePessoa { get; set; }
        public string NomeUsuario { get; set; }
        public string Contato { get; set; }
        public int IDContatoDetalhe { get; set; }
        public string StatusContato { get; set; }
        public string Assunto { get; set; }
        public DateTime Data { get; set; }
        public string HoraInicial { get; set; }
        public string HoraFinal { get; set; }
        public string Observacao { get; set; }
    }
}