using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Ofd_MedicaoRepositorio : RepositorioAbstrato<Ofd_Medicao>, IRepositorioOfd_Medicao
    {
        protected override string HqlLoadById()
        {
            return @"from Ofd_Medicao n 
                    join fetch n.Proposta p 
                    left join fetch n.MedicaoServicos ms
                    where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return @"select new 
                    Ofd_Medicao(n.ID, n.Data, n.Numero, n.Status, p.NomeObra
                    , n.ValorServicos, n.ValorMobilizacao, n.ValorProjeto
                    , n.ValorHoraParada, n.TotalMedicao, n.IncData, n.IncUsuario
                    , n.AltData, n.AltUsuario)
                    from Ofd_Medicao n join n.Proposta p";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
