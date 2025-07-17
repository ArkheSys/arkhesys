using System;
using System.Collections;

namespace Modelo
{
    public partial class DataClassesDataContext
    {
        public DateTime GetServerDateTime()
        {
            //return DateTime.Now;
            IEnumerator enu =
                ExecuteQuery(typeof(DateTime), "Select getdate()").GetEnumerator();
            enu.MoveNext();
            DateTime bla = (DateTime) enu.Current;

            return bla;
        }

        partial void UpdateNota(Nota instance)
        {
            if (instance.ID == 0) instance.IncData = GetServerDateTime();
            instance.AltData = GetServerDateTime();
            ExecuteDynamicUpdate(instance);
        }

        partial void UpdateDocumento(Documento instance)
        {
            if (instance.ID == 0) instance.IncData = GetServerDateTime();
            instance.AltData = GetServerDateTime();
            ExecuteDynamicUpdate(instance);
        }

        partial void UpdateBalanco(Balanco instance)
        {
            if (instance.ID == 0) instance.IncData = GetServerDateTime();
            instance.AltData = GetServerDateTime();
            ExecuteDynamicUpdate(instance);
        }

        partial void UpdateBalancoItem(BalancoItem instance)
        {
            if (instance.ID == 0) instance.IncData = GetServerDateTime();
            instance.AltData = GetServerDateTime();
            ExecuteDynamicUpdate(instance);
        }

        partial void UpdateProjetoHistorico(ProjetoHistorico instance)
        {
            if (instance.ID == 0) instance.IncData = GetServerDateTime();
            instance.AltData = GetServerDateTime();
            ExecuteDynamicUpdate(instance);
        }

        partial void UpdateProjeto_Material(Projeto_Material instance)
        {
            if (instance.ID == 0) instance.IncData = GetServerDateTime();
            instance.AltData = GetServerDateTime();
            ExecuteDynamicUpdate(instance);
        }

        partial void UpdateClienteRevenda(ClienteRevenda instance)
        {
            if (instance.ID == 0) instance.Incdata = GetServerDateTime();
            instance.Altdata = GetServerDateTime();
            ExecuteDynamicUpdate(instance);
        }

        partial void UpdatePedido(Pedido instance)
        {
            if (instance.ID == 0) instance.IncData = GetServerDateTime();
            instance.AltData = GetServerDateTime();
            ExecuteDynamicUpdate(instance);
        }

        partial void UpdateMovCaixa(MovCaixa instance)
        {
            if (instance.ID == 0) instance.IncData = GetServerDateTime();
            instance.AltData = GetServerDateTime();
            ExecuteDynamicUpdate(instance);
        }

        partial void UpdateServico(Servico instance)
        {
            if (instance.id == 0) instance.incdata = GetServerDateTime();
            instance.altdata = GetServerDateTime();
            ExecuteDynamicUpdate(instance);
        }

        partial void UpdateProjeto(Projeto instance)
        {
            if (instance.ID == 0) instance.IncData = GetServerDateTime();
            instance.AltData = GetServerDateTime();
            ExecuteDynamicUpdate(instance);
        }

        partial void UpdatePessoa(Pessoa instance)
        {
            if (instance.ID == 0) instance.IncData = GetServerDateTime();
            instance.AltData = GetServerDateTime();
            ExecuteDynamicUpdate(instance);
        }

        partial void UpdateMovimento(Movimento instance)
        {
            if (instance.ID == 0) instance.IncData = GetServerDateTime();
            instance.AltData = GetServerDateTime();
            ExecuteDynamicUpdate(instance);
        }

        partial void InsertNota(Nota instance)
        {
            instance.AltData = GetServerDateTime();
            instance.IncData = GetServerDateTime();
            ExecuteDynamicInsert(instance);
        }

        partial void InsertDocumento(Documento instance)
        {
            instance.AltData = GetServerDateTime();
            instance.IncData = GetServerDateTime();
            ExecuteDynamicInsert(instance);
        }

        partial void InsertBalanco(Balanco instance)
        {
            instance.AltData = GetServerDateTime();
            instance.IncData = GetServerDateTime();
            ExecuteDynamicInsert(instance);
        }

        partial void InsertBalancoItem(BalancoItem instance)
        {
            instance.AltData = GetServerDateTime();
            instance.IncData = GetServerDateTime();
            ExecuteDynamicInsert(instance);
        }

        partial void InsertProjetoHistorico(ProjetoHistorico instance)
        {
            instance.AltData = GetServerDateTime();
            instance.IncData = GetServerDateTime();
            ExecuteDynamicInsert(instance);
        }

        partial void InsertProjeto_Material(Projeto_Material instance)
        {
            instance.AltData = GetServerDateTime();
            instance.IncData = GetServerDateTime();
            ExecuteDynamicInsert(instance);
        }

        partial void InsertClienteRevenda(ClienteRevenda instance)
        {
            instance.Altdata = GetServerDateTime();
            instance.Incdata = GetServerDateTime();
            ExecuteDynamicInsert(instance);
        }

        partial void InsertPedido(Pedido instance)
        {
            instance.AltData = GetServerDateTime();
            instance.IncData = GetServerDateTime();
            ExecuteDynamicInsert(instance);
        }

        partial void InsertMovCaixa(MovCaixa instance)
        {
            instance.AltData = GetServerDateTime();
            instance.IncData = GetServerDateTime();
            ExecuteDynamicInsert(instance);
        }

        partial void InsertServico(Servico instance)
        {
            instance.altdata = GetServerDateTime();
            instance.incdata = GetServerDateTime();
            ExecuteDynamicInsert(instance);
        }

        partial void InsertProjeto(Projeto instance)
        {
            instance.AltData = GetServerDateTime();
            instance.IncData = GetServerDateTime();
            ExecuteDynamicInsert(instance);
        }

        partial void InsertPessoa(Pessoa instance)
        {
            instance.AltData = GetServerDateTime();
            instance.IncData = GetServerDateTime();
            ExecuteDynamicInsert(instance);
        }

        partial void InsertMovimento(Movimento instance)
        {
            instance.AltData = GetServerDateTime();
            instance.IncData = GetServerDateTime();
            ExecuteDynamicInsert(instance);
        }
    }
}