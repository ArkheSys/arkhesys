namespace cwkGestao.Modelo.Proxy
{
    public class pxResponsavelServicoCliente
    {
        public int IDPessoa { get; set; }
        public string Nome { get; set; }

        public int IDResponsavel { get; set; }
        public string Responsavel { get; set; }

        public int IDServico { get; set; }
        public string Servico { get; set; }


        public pxResponsavelServicoCliente() { }

        public pxResponsavelServicoCliente(int IDPessoa, string Nome, int IDResponsavel, string Responsavel, int IDServico, string Servico)
        {
            this.IDPessoa = IDPessoa;
            this.Nome = Nome;
            this.IDResponsavel = IDResponsavel;
            this.Responsavel = Responsavel;
            this.IDServico = IDServico;
            this.Servico = Servico;
        }
    }
}