using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;
using NHibernate.Transform;
using System.Reflection;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{

    partial class ImeiRepositorio : RepositorioAbstrato<Imei>, IRepositorioImei
    {
        protected override string HqlLoadById()
        {
            return @" from Imei i 
                      inner join fetch i.Produto p
                      left join fetch i.NotaItemEntrada nie
                      left join fetch i.NotaItemSaida nis
                      left join fetch nie.Nota ne
                      left join fetch nis.Nota ns
                      where i.ID = :id ";
        }

        protected override string hqlGetAll()
        {
            return @" from Imei i 
                      inner join fetch i.Produto p
                      left join fetch i.NotaItemEntrada nie
                      left join fetch i.NotaItemSaida nis
                      left join fetch nie.Nota ne
                      left join fetch nis.Nota ns";
        }

        protected string hqlGetAllAtivos()
        {
            return hqlGetAll() + " where nis is null";
        }

        protected string hqlGetAllInativos()
        {
            return hqlGetAll() + " where nis is not null";
        }


        public IList<Imei> GetAllAtivos()
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAllAtivos();

                IList<Imei> imeis = session.CreateQuery(hql).List<Imei>();
                return imeis;
            }
        }

        public IList<Imei> GetAllInativos()
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAllInativos();

                IList<Imei> imeis = session.CreateQuery(hql).List<Imei>();
                return imeis;
            }
        }
        public IList<Imei> GetAllByIDProduto(int IDProduto, bool bAtivo)
        {
            using (var session = GetSession())
            {
                string hql;

                if (bAtivo)
                    hql = hqlGetAllAtivos();
                else
                    hql = hqlGetAllInativos();

                hql += " and i.Produto.ID = :IDProduto";

                IList<Imei> imeis = session.CreateQuery(hql).SetInt32("IDProduto", IDProduto).List<Imei>();

                return imeis;
            }
        }

        public IList<Imei> GetAllByIDProdutoByIDNotaItem(int IDProduto, int IDNotaItem)
        {
            using (var session = GetSession())
            {
                string hql;

                hql = hqlGetAll();

                hql += @" where i.Produto.ID = :IDProduto 
                          and ((i.NotaItemSaida.ID = :IDNotaItem) or nis is null)";

                IList<Imei> imeis = session.CreateQuery(hql).SetInt32("IDProduto", IDProduto).SetInt32("IDNotaItem", IDNotaItem).List<Imei>();

                return imeis;
            }
        }

        public IList<Imei> GetAllByIDNotaItemEntrada(int IDNotaItemEntrada, bool bAtivo)
        {
            using (var session = GetSession())
            {
                string hql;

                if (bAtivo)
                    hql = hqlGetAllAtivos();
                else
                    hql = hqlGetAllInativos();

                hql += " and nie.ID = :IDNotaItemEntrada";

                IList<Imei> imeis = session.CreateQuery(hql).SetInt32("IDNotaItemEntrada", IDNotaItemEntrada).List<Imei>();
                return imeis;
            }
        }

        public IList<Imei> GetAllByIDNotaItemSaida(int IDNotaItemSaida, bool bAtivo)
        {
            using (var session = GetSession())
            {
                string hql;

                if (bAtivo)
                    hql = hqlGetAllAtivos();
                else
                    hql = hqlGetAllInativos();

                hql += " and nis.ID = :IDNotaItemSaida";
                               

                IList<Imei> imeis = session.CreateQuery(hql).SetInt32("IDNotaItemSaida", IDNotaItemSaida).List<Imei>();
                return imeis;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
