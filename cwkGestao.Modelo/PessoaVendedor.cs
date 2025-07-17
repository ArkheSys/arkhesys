using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
	public class PessoaVendedor : ModeloBase
	{
        public PessoaVendedor()
        {
            ProdutoVendedorComissoes = new List<ProdutoVendedorComissao>();
        }

        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _IDPessoa;
        public virtual int IDPessoa { get { return _IDPessoa; } set { _IDPessoa = value; } }
        
        private Pessoa _Pessoa;
        public virtual Pessoa Pessoa { get { return _Pessoa; } set { _Pessoa = value; } }

        private decimal? _ComissaoVendedor;
        public virtual decimal? ComissaoVendedor { get { return _ComissaoVendedor; } set { _ComissaoVendedor = value; } }

        private decimal? _ComissaoServicoVendedor;
        public virtual decimal? ComissaoServicoVendedor { get { return _ComissaoServicoVendedor; } set { _ComissaoServicoVendedor = value; } }

        private string _Senha;
        public virtual string Senha { get { return _Senha; } set { _Senha = value; } }

        public virtual IList<ProdutoVendedorComissao> ProdutoVendedorComissoes { get; set; }

        public override string ToString()
        {
            try { return _Pessoa.ToString(); }
            catch { return base.ToString(); }
        }

        public virtual bool GetComissaoProduto(Produto produto, out byte tipoComissao, out decimal comissaoPct)
        {
            tipoComissao = 0;
            comissaoPct = 0;
            IList<ProdutoVendedorComissao> comissoes = this.ProdutoVendedorComissoes.Where(p => (p.Produto != null) && (p.Produto.ID == produto.ID)).ToList();

            if (comissoes.Count() > 0)
            {
                var comissaoProduto = comissoes.First();
                tipoComissao = comissaoProduto.TipoComissao;
                comissaoPct = comissaoProduto.ComissaoPct;
                return true;
            }
            return false;
        }
	}
}
		