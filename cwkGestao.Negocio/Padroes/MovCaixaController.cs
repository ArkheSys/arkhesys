using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Negocio
{
	public partial class MovCaixaController : BaseController<MovCaixa>
	{
		#region Singleton
        private static MovCaixaController instance;
        private static IRepositorioMovCaixa repositorioMovCaixa;

        private MovCaixaController()
        { }
		
		static MovCaixaController()
        {
            instance = new MovCaixaController();
            repositorioT = RepositorioFactory<MovCaixa>.GetRepositorio();
            repositorioMovCaixa = (IRepositorioMovCaixa)repositorioT;
        }
			
		public static MovCaixaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(MovCaixa objeto)
        {
            return new Dictionary<string, string>();
        }

        public override Dictionary<string, string> Salvar(MovCaixa objeto, Acao acao)
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
                            objeto.Codigo = GetNovoCodigoMovimento();
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
                if (ex.InnerException  != null)
                {
                    Exception y = new Exception(ex.InnerException.Message);
                    throw y;
                }
                else
                    throw ex;
            }
        }

        public IList<pxySaldoBanco> GetSaldoBancos(IList<Modelo.Banco> bancos, DateTime data)
        {
            return repositorioMovCaixa.GetSaldoBancos(bancos, data);
        }

        public IList<MovCaixa> GetPorBancoEData(Modelo.Banco banco, DateTime dataInicial, DateTime dataFinal)
        {
            return repositorioMovCaixa.GetPorBancoEData(banco, dataInicial, dataFinal);
        }

        public IList<pxyMovimentoCaixa> GetRelatorioDeMovimentoCaixa(Modelo.Banco banco, DateTime dataInicial, DateTime dataFinal)
        {
            return repositorioMovCaixa.GetRelatorioDeMovimentoCaixa(banco, dataInicial, dataFinal);
        }

        public int GetNovoCodigoMovimento()
        {
            return repositorioMovCaixa.GetNovoCodigoMovimento();
        }

        public IList<pxyMovCaixaPorFluxoCaixa> GetMovimentosPorFluxoCaixa(int IDFluxoCaixa)
        {
            return repositorioMovCaixa.GetMovimentosPorFluxoCaixa(IDFluxoCaixa);
        }
    }
}
