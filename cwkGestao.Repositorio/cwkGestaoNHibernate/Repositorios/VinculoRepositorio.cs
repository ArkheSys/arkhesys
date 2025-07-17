using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{

    partial class VinculoRepositorio : RepositorioAbstrato<Vinculo>, IRepositorioVinculo
    {
        protected override string HqlLoadById()
        {
            return "from Vinculo n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Vinculo n";
        }

        public Vinculo GetProdVinculo(string CodProd, string CFOP, int idFornecedor, ref bool temCFOP)
        {
            using (var session = GetSession())
            {
                /* Correção se o produto contém aspas na descrição */
                string[] _msg = CodProd.Split('\'');
                if (_msg.Length > 1)
                {
                    CodProd = "";
                    for (int i = 0; i < _msg.Length; i++)
                    {
                        if (i > 0 && i < _msg.Length)
                            CodProd += "\'\'";

                        CodProd += _msg[i];
                    }
                }

                string hql = "from Vinculo t where t.Pessoa.ID = " + idFornecedor + " and t.CodProduto = " + "'" + CodProd + "'" + " and t.CFOP = " + CFOP;

                var vinculo = session.CreateQuery(hql).List<Vinculo>(); 

                if (vinculo.Count == 0)
                {
                    hql = "from Vinculo t where t.Pessoa.ID = " + idFornecedor + " and t.CodProduto = " + "'" + CodProd + "'";
                    vinculo = session.CreateQuery(hql).List<Vinculo>(); 
                    temCFOP = false;
                }

                return vinculo.Count > 0 ? vinculo[0] : null;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
