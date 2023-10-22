using Despacho.Otimizado.Domain.Abstractions;
using Despacho.Otimizado.Domain.Entities;
using Despacho.Otimizado.Infra.Data.Entity.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Despacho.Otimizado.Infra.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Veiculo> Veiculo { get; set; }
        public DbSet<VeiculoTipo> VeiculoTipo { get; set; }
        public DbSet<Maquina> Maquina { get; set; }
        public DbSet<MaquinaVeiculo> MaquinaVeiculo { get; set; }
        public DbSet<Localizacao> Localizacao { get; set; }
        public DbSet<Rota> Rota { get; set; }
        public DbSet<MaquinaVeiculoRota> MaquinaVeiculoRota { get; set; }
        public DbSet<EstradaSubTipo> EstradaSubTipo { get; set; }
        public DbSet<EstradaTipo> EstradaTipo { get; set; }
        public DbSet<Estrada> Estrada { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Abstracoes
            ConfigureAbstractions(builder);

            // Chaves
            OnModelCreatingKeys(builder);

            // Propriedades
            OnModelCreatingProperty(builder);

            // Relacionamentos
            OnModelCreatingRelationship(builder);            
                    
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        private static void ConfigureAbstractions(ModelBuilder builder)
        {
            var entityTypes = builder.Model.GetEntityTypes();

            foreach (var entityType in entityTypes)
            {
                var entityClrType = entityType.ClrType;
                if (entityClrType.BaseType?.IsGenericType == true &&
                    entityClrType.BaseType.GetGenericTypeDefinition() == typeof(EntityBaseWithName<>))
                {
                    var nomeProperty = entityType.FindProperty("Nome");
                    nomeProperty?.SetMaxLength(50);
                }
            }
        }

        private static void OnModelCreatingKeys(ModelBuilder builder)
        {
            // Por enquanto não mexeremos nas tabelas do Identity
            var entityTypes = builder.Model.GetEntityTypes().Where(e => e.ClrType.Namespace == typeof(Veiculo).Namespace);

            foreach (var entityType in entityTypes)
            {
                var entityClrType = entityType.ClrType;
                var primaryKey = entityType.FindPrimaryKey();
                if (primaryKey is not null && primaryKey.Properties.Count == 1)
                {
                    var primaryKeyProperty = primaryKey.Properties[0];
                    // Padronizado para "PK_NomeDaEntidade_Id"
                    builder.Entity(entityClrType).HasKey(primaryKeyProperty.Name).HasName($"PK_{entityType.GetTableName()}_{primaryKeyProperty.Name}");
                }
            }
        }

        private static void OnModelCreatingProperty(ModelBuilder builder)
        {
            builder.Entity<Veiculo>().Property(v => v.Capacidade).HasColumnType("numeric(6,2)");
            builder.Entity<Localizacao>().Property(l => l.Latitude).HasColumnType("numeric(6,4)");
            builder.Entity<Localizacao>().Property(l => l.Longitude).HasColumnType("numeric(7,4)");
            builder.Entity<Estrada>().Property(v => v.VelocidadeMedia).HasColumnType("numeric(4,1)");
            builder.Entity<Rota>().Property(c => c.Distancia).HasColumnType("numeric(8,2)");
            builder.Entity<Rota>().Property(t => t.Tempo).HasColumnType("numeric(8,2)");
        }

         private static void OnModelCreatingRelationship(ModelBuilder builder)
        {
            builder.Entity<Veiculo>()
                .HasOne(v => v.VeiculoTipo)
                .WithMany(t => t.Veiculos)
                .HasForeignKey(v => v.VeiculoTipoId)
                .HasConstraintName("FK_Veiculo_VeiculoTipo")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MaquinaVeiculo>()
                .HasOne(mv => mv.Maquina)
                .WithMany()
                .HasForeignKey(m => m.MaquinaId)
                .HasConstraintName("FK_MaquinaVeiculo_Maquina")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MaquinaVeiculo>()
                .HasOne(mv => mv.Veiculo)
                .WithMany()
                .HasForeignKey(v => v.VeiculoId)
                .HasConstraintName("FK_MaquinaVeiculo_Veiculo")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Rota>()
                .HasOne(o => o.Origem)
                .WithMany()
                .HasForeignKey(o => o.OrigemId)
                .HasConstraintName("FK_Rota_Origem_Localizacao")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Rota>()
                .HasOne(d => d.Destino)
                .WithMany()
                .HasForeignKey(d => d.DestinoId)
                .HasConstraintName("FK_Rota_Destino_Localizacao")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Rota>()
                .HasOne(r => r.RotaNavegation)
                .WithMany(r => r.Rotas)
                .HasForeignKey(r => r.RotaId)
                .HasConstraintName("FK_Rota_RotaId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MaquinaVeiculoRota>()
                .HasOne(m => m.MaquinaVeiculo)
                .WithMany()
                .HasForeignKey(m => m.MaquinaVeiculoId)
                .HasConstraintName("FK_MaquinaVeiculoRota_MaquinaVeiculo")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MaquinaVeiculoRota>()
                .HasOne(r => r.Rota)
                .WithMany()
                .HasForeignKey(r => r.RotaId)
                .HasConstraintName("FK_MaquinaVeiculoRota_Rota")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Estrada>()
                .HasOne(e => e.EstradaTipo)
                .WithMany()
                .HasForeignKey(e => e.EstradaTipoId)
                .HasConstraintName("FK_Estrada_EstradaTipo")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Estrada>()
                .HasOne(e => e.EstradaSubTipo)
                .WithMany()
                .HasForeignKey(e => e.EstradaSubTipoId)
                .HasConstraintName("FK_Estrada_EstradaSubTipo")
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}