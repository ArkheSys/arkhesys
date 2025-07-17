using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Repositorio;

namespace cwkGestao.Negocio.Padroes
{
    public partial class MovimentacaoChequeController : BaseController<MovimentacaoCheque>
    {
        #region Singleton
        private static MovimentacaoChequeController instance;
        private static IRepositorioMovimentacaoCheque repositorioMovCheque;

        private MovimentacaoChequeController()
        { }

        static MovimentacaoChequeController()
        {
            instance = new MovimentacaoChequeController();
            repositorioT = RepositorioFactory<MovimentacaoCheque>.GetRepositorio();
            repositorioMovCheque = (IRepositorioMovimentacaoCheque)repositorioT;
        }

        public static MovimentacaoChequeController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public override Dictionary<string, string> ValidaObjeto(MovimentacaoCheque objeto)
        {
            return new Dictionary<string, string>();
        }

        public override Dictionary<string, string> Salvar(MovimentacaoCheque objeto, Acao acao)
        {
            TrataDatas(objeto);
            Dictionary<string, string> ret = new Dictionary<string, string>();
            try
            {
                if (acao == Acao.Excluir)
                {
                    Delete(objeto);
                    return new Dictionary<string, string>();
                }
                ret = ValidaObjeto(objeto);
                if (ret.Count == 0)
                {
                    SetaCamposDateTimeEstourados(objeto, DateTime.Now.AddYears(-200));
                    SetaCamposIncAltUsuario(objeto, acao);
                    switch (acao)
                    {
                        case Modelo.Acao.Incluir:
                            objeto.Codigo = MaxCodigo();
                            Create(objeto);
                            break;
                        case Modelo.Acao.Alterar:
                            Update(objeto);
                            break;

                    }
                    return ret;
                }
                else
                {
                    return ret;
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Exception y = new Exception(ex.InnerException.Message);
                    throw y;
                }
                else
                    throw ex;
            }
        }

        public MovCaixa CriarMovimentoCaixa(Ent_SaiMovCaixa ent_SaiMovCaixa, Banco banco, MovimentacaoCheque movCheque)
        {
            MovCaixa movCaixa = new MovCaixa();

            movCaixa.Filial = movCaixa.Filial;
            movCaixa.Dt = DateTime.Now;
            movCaixa.DtDigitacao = DateTime.Now;
            movCaixa.Banco = banco;
            movCaixa.Ent_Sai = ent_SaiMovCaixa;
            movCaixa.Valor = movCheque.Valor;
            movCaixa.Historico = movCheque.Historico;
            movCaixa.FormaPagamento.Tipo = FormaPagamentoTipo.Cheque;
            //movCaixa.Movimentos.Add();

            return movCaixa;
        }
    }


}
