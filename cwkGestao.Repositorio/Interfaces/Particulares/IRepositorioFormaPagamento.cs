using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioFormaPagamento : IRepositorio<FormaPagamento>
    {
        FormaPagamento GetCheque();
        FormaPagamento FindByCodigoSIS(string cod);
        FormaPagamento FindByCodigo(int cod);
    }
}
