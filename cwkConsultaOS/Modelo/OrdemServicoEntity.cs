namespace cwkConsultaOS
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    public partial class OrdemServicoEntity : DbContext
    {
        public OrdemServicoEntity() 
            : base(Modelo.cwkGlobal.CONN_STRING)
        {
        }

        public virtual DbSet<Clientes> CLIENTES { get; set; }
        public virtual DbSet<Equipamento> EQUIPAMENTO { get; set; }
        public virtual DbSet<EquipamentosOS> EQUIPAMENTOOS { get; set; }
        public virtual DbSet<Loja> LOJA { get; set; }
        public virtual DbSet<OrdemServico> ORDEMSERVICO { get; set; }
        public virtual DbSet<ProdutosOS> PRODUTOOS { get; set; }
        public virtual DbSet<ServicoOS> SERVICOOS { get; set; }
        public virtual DbSet<Situacao> SITUACAO { get; set; }
        public virtual DbSet<Usuario> UCTABUSERS { get; set; }
        public virtual DbSet<Vendedor> VENDEDOR { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>()
                .Property(e => e.CPF)
                .IsUnicode(false);

            modelBuilder.Entity<Equipamento>()
                .Property(e => e.DESCRICAO)
                .IsUnicode(false);

            modelBuilder.Entity<Equipamento>()
                .Property(e => e.MARCA)
                .IsUnicode(false);

            modelBuilder.Entity<Equipamento>()
                .Property(e => e.MODELO)
                .IsUnicode(false);

            modelBuilder.Entity<Equipamento>()
                .Property(e => e.SERIE)
                .IsUnicode(false);

            modelBuilder.Entity<Equipamento>()
                .Property(e => e.PATRIMONIIO)
                .IsUnicode(false);

            modelBuilder.Entity<Equipamento>()
                .Property(e => e.REVENDA)
                .IsUnicode(false);

            modelBuilder.Entity<Equipamento>()
                .Property(e => e.NUM_NF)
                .IsUnicode(false);

            modelBuilder.Entity<Equipamento>()
                .Property(e => e.NUM_CERTGAR)
                .IsUnicode(false);

            modelBuilder.Entity<Equipamento>()
                .Property(e => e.CAMPO_BOOL)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Equipamento>()
                .Property(e => e.IPEM)
                .IsUnicode(false);

            modelBuilder.Entity<Equipamento>()
                .Property(e => e.VOLTAGEM)
                .IsUnicode(false);

            modelBuilder.Entity<Equipamento>()
                .Property(e => e.INMETRO)
                .IsUnicode(false);

            modelBuilder.Entity<Equipamento>()
                .Property(e => e.ETIQUETA_REPARO)
                .IsUnicode(false);

            modelBuilder.Entity<Equipamento>()
                .Property(e => e.CAP_MAXIMA)
                .IsUnicode(false);

            modelBuilder.Entity<Equipamento>()
                .Property(e => e.SITUACAO_EQUIP)
                .IsUnicode(false);

            modelBuilder.Entity<Equipamento>()
                .Property(e => e.ATIVO)
                .IsUnicode(false);

            modelBuilder.Entity<Equipamento>()
                .Property(e => e.OBSERVACOES)
                .IsUnicode(false);

            modelBuilder.Entity<Equipamento>()
                .Property(e => e.ACESSORIO)
                .IsUnicode(false);

            modelBuilder.Entity<EquipamentosOS>()
                .Property(e => e.DEFEITO)
                .IsUnicode(false);

            modelBuilder.Entity<EquipamentosOS>()
                .Property(e => e.REVENDA)
                .IsUnicode(false);

            modelBuilder.Entity<EquipamentosOS>()
                .Property(e => e.NUM_NF)
                .IsUnicode(false);

            modelBuilder.Entity<EquipamentosOS>()
                .Property(e => e.NUM_CERTGAR)
                .IsUnicode(false);

            modelBuilder.Entity<EquipamentosOS>()
                .Property(e => e.OS_GARANTIDOR_SN)
                .IsUnicode(false);

            modelBuilder.Entity<EquipamentosOS>()
                .Property(e => e.LAUDO)
                .IsUnicode(false);

            modelBuilder.Entity<EquipamentosOS>()
                .Property(e => e.OBS)
                .IsUnicode(false);

            modelBuilder.Entity<EquipamentosOS>()
                .Property(e => e.ACESSORIO)
                .IsUnicode(false);

            modelBuilder.Entity<Loja>()
                .Property(e => e.CNPJ)
                .IsUnicode(false);

            modelBuilder.Entity<Loja>()
                .Property(e => e.RAZAOSOCIAL)
                .IsUnicode(false);

            modelBuilder.Entity<Loja>()
                .Property(e => e.NOMEFANTASIA)
                .IsUnicode(false);

            modelBuilder.Entity<Loja>()
                .HasMany(e => e.ORDEMSERVICO)
                .WithRequired(e => e.LOJA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrdemServico>()
                .Property(e => e.FECHADAPORUSUARIO)
                .IsUnicode(false);

            modelBuilder.Entity<OrdemServico>()
                .Property(e => e.TOTALHORAREALIZADO)
                .IsUnicode(false);

            modelBuilder.Entity<OrdemServico>()
                .Property(e => e.TOTALHORAPREVISAO)
                .IsUnicode(false);

            modelBuilder.Entity<OrdemServico>()
                .Property(e => e.CANCELADOPOR)
                .IsUnicode(false);

            modelBuilder.Entity<OrdemServico>()
                .Property(e => e.OBS1)
                .IsUnicode(false);

            modelBuilder.Entity<OrdemServico>()
                .Property(e => e.OBS2)
                .IsUnicode(false);

            modelBuilder.Entity<OrdemServico>()
                .HasMany(e => e.SERVICOOS)
                .WithRequired(e => e.ORDEMSERVICO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProdutosOS>()
                .Property(e => e.PRO_CODIGO)
                .IsUnicode(false);

            modelBuilder.Entity<ProdutosOS>()
                .Property(e => e.DESCRICAO)
                .IsUnicode(false);

            modelBuilder.Entity<ProdutosOS>()
                .Property(e => e.INFPROD)
                .IsUnicode(false);

            modelBuilder.Entity<ProdutosOS>()
                .Property(e => e.USUARIO)
                .IsUnicode(false);

            modelBuilder.Entity<ServicoOS>()
                .Property(e => e.PRO_CODIGO)
                .IsUnicode(false);

            modelBuilder.Entity<ServicoOS>()
                .Property(e => e.DESCRICAO)
                .IsUnicode(false);

            modelBuilder.Entity<ServicoOS>()
                .Property(e => e.FORMACOBRANCA)
                .IsUnicode(false);

            modelBuilder.Entity<Situacao>()
                .Property(e => e.NOME)
                .IsUnicode(false);

            modelBuilder.Entity<Situacao>()
                .HasMany(e => e.ORDEMSERVICO)
                .WithRequired(e => e.SITUACAO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.UCUSERNAME)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.UCLOGIN)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.UCPASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.UCEMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.UCTYPEREC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.UCKEY)
                .IsUnicode(false);
        }

    }
}
