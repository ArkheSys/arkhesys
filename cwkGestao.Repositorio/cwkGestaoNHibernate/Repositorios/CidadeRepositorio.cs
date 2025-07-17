using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.Collections;
using NHibernate.Linq;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CidadeRepositorio : RepositorioAbstrato<Cidade>, IRepositorioCidade
    {
        protected override string HqlLoadById()
        {
            return "from Cidade c left join fetch c.UF where c.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Cidade c left join fetch c.UF";
        }

        public override int MaxCodigo()
        {
            using (var session = GetSession())
            {
                try
                {
                    return (from t in new NHibernate.Linq.NhQueryable<Cidade>(session) select t.Codigo).Max() + 1;
                }
                catch (Exception)
                {
                    return 1;
                }
            }
        }

        public Hashtable GetHashNomeCidade()
        {
            using (var session = GetSession())
            {
                string hql = "from Cidade c left join fetch c.UF";

                var resultado = session.CreateQuery(hql).List<Cidade>();
                Hashtable ret = new Hashtable();
                foreach (var item in resultado)
                {
                    if (!ret.ContainsKey(item.Nome.ToUpper()))
                        ret.Add(item.Nome.ToUpper(), item);
                }
                return ret;
            }
        }

        public Cidade GetCidadePorIbge(string CodigoIbge)
        {
            using (var session = GetSession())
            {
                var hql = "from Cidade c left join fetch c.UF uf where c.IBGE = :codigoIBGE";
                return session.CreateQuery(hql).SetString("codigoIBGE", CodigoIbge).List<Cidade>().FirstOrDefault();
            }
        }

        public Cidade GetByLikeName(string nome, string UF)
        {
            using (var session = GetSession())
            {
                try
                {
                    string hql = "from Cidade c left join fetch c.UF uf where uf.Sigla = :sigla";
                    var CidadesUF = session.CreateQuery(hql).SetString("sigla", UF).List<Cidade>();
                    CidadesUF.ForEach(cidade => { cidade.Nome = RemoverAcentos(cidade.Nome).ToLower(); });

                    Cidade cid = CidadesUF.FirstOrDefault(c => c.Nome.Contains(RemoverAcentos(nome).ToLower()));
                    if (cid != null)
                    {
                        cid = LoadObjectById(cid.ID);
                        return cid;
                    }

                    return null;
                }
                catch (Exception z)
                {
                    throw z;
                }
            }
        }

        private string RemoverAcentos(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";
            else
            {
                byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(input);
                return System.Text.Encoding.UTF8.GetString(bytes);
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
