using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Projeto")]
    public class ProjetoOP : ModeloBase
    {
        public ProjetoOP()
        {
        }

        public enum ProjetoStatus { Nenhum = 0, Aberta = 2 }

        public ProjetoOP(String nome, String clienteNOme, int? numOp, int status, int codigo, DateTime? data)
        {
            Nome = nome;
            Cliente = clienteNOme;
            Data = data.HasValue ? data : null;
            NumOp = numOp;
            Status = (ProjetoStatus)status;
            Codigo = codigo;
        }

        [InformacaoExibicao(1, "Data", 50)]
        public DateTime? Data { get; set; }
        [InformacaoExibicao(2, "Nome", 270)]
        public String Nome { get; set; }
        [InformacaoExibicao(3, "Cliente", 250)]
        public String Cliente { get; set; }
        [InformacaoExibicao(4, "NºOP", 50, cwkGestao.Modelo.InformacaoExibicao.HAlinhamento.Direita)]
        public int? NumOp { get; set; }
        [InformacaoExibicao(5, "Status", 70)]
        public ProjetoStatus Status { get; set; }
        [InformacaoExibicao(0, "Cód", 60, cwkGestao.Modelo.InformacaoExibicao.HAlinhamento.Direita)]
        public int Codigo { get; set; }


        public override int ID
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }
    }
}
