using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao(0, "Email", 100)]
    public class PessoaEmail : ModeloBase
    {
        public override int ID { get; set; }
        [InformacaoExibicao(0, "Email", 150)]
        public virtual string Email { get; set; }
        [InformacaoExibicao(1, "Financeiro", 100)]
        public virtual bool BFinanceiro { get; set; }
        [InformacaoExibicao(2, "Venda", 90)]
        public virtual bool BVenda { get; set; }
        [InformacaoExibicao(3, "Compra", 90)]
        public virtual bool BCompra { get; set; }
        [InformacaoExibicao(4, "Administrativo", 100)]
        public virtual bool BAdministrativo { get; set; }
        [InformacaoExibicao(5, "Contato", 80)]
        public virtual bool BContato { get; set; }
        [InformacaoExibicao(6, "Sugestão", 90)]
        public virtual bool BSugestao { get; set; }
        [InformacaoExibicao(7, "MSN", 70)]
        public virtual bool BMsn { get; set; }
        [InformacaoExibicao(8, "NFe", 70)]
        public virtual bool BNFe { get; set; }
        [InformacaoExibicao(9, "NFSe", 70)]
        public virtual bool BNFSe { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}
