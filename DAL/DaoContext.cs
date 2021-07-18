using Authentic.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentic.DAL
{
    public class DaoContext : IdentityDbContext
    {
        public virtual DbSet<Usuario> Usuarios { get; set;}
        public DbSet<PostLog> PostLogs { get; set;}
        public DbSet<Filial> Filiais { get; set;}
        public DbSet<FormaPagamento> FormasPagamento { get; set;}
        public DbSet<TipoPagamento> TiposPagamento { get; set;}
        public DbSet<Marca> Marcas { get; set;}
        public DbSet<Cor> Cores { get; set;}
        public DbSet<Tamanho> Tamanhos { get; set;}
        public DbSet<Produto> Produtos { get; set;}
        public DbSet<RamoAtividade> RamosAtividades { get; set;}   
        public DbSet<Segmento> Segmentos { get; set;}
        public DbSet<Secao> Secoes { get; set;}
        public DbSet<Especie> Especies { get; set;}
        public DbSet<UnidadeMedida> UnidadeMedidas { get; set;}
        public DbSet<Promocao> Promocoes { get; set;}
        public DbSet<PromocaoFilial> PromocaoFiliais { get; set;}
        public DbSet<PromocaoRegra> PromocaoRegras { get; set;}
        public DbSet<PromocaoPagamento> PromocaoPagamentos { get; set;}
        public DbSet<PromocaoVenda> PromocaoVendas { get; set;}
        public DbSet<PromocaoCliente> PromocaoClientes { get; set;}
        public DbSet<PromocaoProduto> PromocaoProdutos { get; set;}
        public DbSet<PromocaoProdutoMarca> PromocaoProdutoMarcas { get; set;}


        public DaoContext()
        {
        } 
        public DaoContext(DbContextOptions<DaoContext> options) : base(options)
        {
        } 

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source=ECOMINA-DELL\\SQLEXPRESS;Initial Catalog=NexttPromo;User ID=sa;Password=nexttsol");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Usuario>().HasKey(pk => pk.Id);
            // modelBuilder.Entity<Usuario>().Property(p => p.Id).HasColumnName("ID").HasColumnType("uniqueidentifier");

            modelBuilder.Entity<PostLog>().ToTable("PostLog");
            modelBuilder.Entity<PostLog>().Property(p => p.Id)
                .HasColumnName("ID")
                .HasColumnType("int")
                .UseIdentityColumn(1, 1);

            modelBuilder.Entity<Filial>().ToTable("Filial");
            modelBuilder.Entity<Filial>().Property(p => p.Id).HasColumnName("ID").HasColumnType("smallint");
            // modelBuilder.Entity<Filial>().Ignore(x => x.Promocoes);

            modelBuilder.Entity<FormaPagamento>().ToTable("FormaPagamento");
            modelBuilder.Entity<FormaPagamento>().Property(p => p.Id).HasColumnName("ID").HasColumnType("smallint");

            modelBuilder.Entity<TipoPagamento>().ToTable("TipoPagamento");
            modelBuilder.Entity<TipoPagamento>().Property(p => p.Id).HasColumnName("ID").HasColumnType("smallint");
            modelBuilder.Entity<TipoPagamento>()
                .HasOne(p => p.FormaPagamento)
                .WithMany(f => f.Tipos)
                .HasForeignKey("IDFormaPagamento");

            modelBuilder.Entity<UnidadeMedida>().ToTable("UnidadeMedida");
            modelBuilder.Entity<UnidadeMedida>().Property(p => p.Id).HasColumnName("ID").HasColumnType("smallint");                

            modelBuilder.Entity<Marca>().ToTable("Marca");
            modelBuilder.Entity<Marca>().Property(p => p.Id).HasColumnName("ID").HasColumnType("smallint");

            modelBuilder.Entity<Cor>().ToTable("Cor");
            modelBuilder.Entity<Cor>().Property(p => p.Id).HasColumnName("ID").HasColumnType("smallint");

            modelBuilder.Entity<Tamanho>().ToTable("Tamanho");
            modelBuilder.Entity<Tamanho>().Property(p => p.Id).HasColumnName("ID").HasColumnType("smallint");

            modelBuilder.Entity<RamoAtividade>().ToTable("RamoAtividade");
            modelBuilder.Entity<RamoAtividade>().Property(p => p.Id).HasColumnName("ID").HasColumnType("smallint");

            modelBuilder.Entity<Segmento>().ToTable("Segmento");
            modelBuilder.Entity<Segmento>().Property(p => p.Id).HasColumnName("ID").HasColumnType("smallint");
            modelBuilder.Entity<Segmento>()
                .HasOne(o => o.RamoAtividade)
                .WithMany(m => m.Segmentos )
                .HasForeignKey("IDRamoAtividade"); 

            modelBuilder.Entity<Secao>().ToTable("Secao");
            modelBuilder.Entity<Secao>().Property(p => p.Id).HasColumnName("ID").HasColumnType("smallint");
            modelBuilder.Entity<Secao>()
                .HasOne(o => o.Segmento)
                .WithMany(m => m.Secoes )
                .HasForeignKey("IDSegmento");

            modelBuilder.Entity<Especie>().ToTable("Especie");
            modelBuilder.Entity<Especie>().Property(p => p.Id).HasColumnName("ID").HasColumnType("smallint");
            modelBuilder.Entity<Especie>().HasKey(pk => new {pk.Id, pk.IDSecao});
            modelBuilder.Entity<Especie>()
                .HasOne(o => o.Secao)
                .WithMany(m => m.Especies)
                .HasForeignKey("IDSecao");            

            modelBuilder.Entity<Produto>().ToTable("Produto");
            modelBuilder.Entity<Produto>().Property(p => p.Id).HasColumnName("ID").HasColumnType("Int");
            modelBuilder.Entity<Produto>()
                .HasOne(o => o.Secao)
                .WithMany(m => m.Produtos)
                .HasForeignKey("IDSecao");  
            modelBuilder.Entity<Produto>()
                .HasOne(o => o.Especie)
                .WithMany(m => m.Produtos)
                .HasForeignKey(fk => new {fk.IDSecao, fk.IDEspecie});  
            modelBuilder.Entity<Produto>()
                .HasOne(o => o.Marca)
                .WithMany(m => m.Produtos)
                .HasForeignKey("IDMarca");  
            modelBuilder.Entity<Produto>()
                .HasOne(o => o.UnidadeMedida)
                .WithMany(m => m.Produtos)
                .HasForeignKey("IDUnidadeMedida");                  


            modelBuilder.Entity<Promocao>().ToTable("Promocao");
            modelBuilder.Entity<Promocao>().Property(p => p.Id).HasColumnName("ID").HasColumnType("Int").UseIdentityColumn(1,1);
            modelBuilder.Entity<Promocao>().Property(p => p.Status).HasColumnName("IDStatusPromocao").HasColumnType("char(1)");

            /* PromocaoFilial */
            modelBuilder.Entity<PromocaoFilial>().ToTable("PromocaoFilial");
            modelBuilder.Entity<PromocaoFilial>().Property(p => p.Id).HasColumnName("ID").HasColumnType("Int").UseIdentityColumn(1,1);
            modelBuilder.Entity<PromocaoFilial>().Property(p => p.IDFilial).HasColumnName("IDFilial").HasColumnType("smallint");
            modelBuilder.Entity<PromocaoFilial>()
                .HasOne(p => p.Promocao)
                .WithMany(f => f.Filiais)
                .HasForeignKey("IDPromocao");

            modelBuilder.Entity<PromocaoFilial>()
                .HasOne(p => p.Filial);
                // .WithMany(f => f.Promocoes)
                // .HasForeignKey("IDFilial");                
            
            /* PromocaoRegra */
            modelBuilder.Entity<PromocaoRegra>().ToTable("PromocaoRegra");
            modelBuilder.Entity<PromocaoRegra>().Property(p => p.Id).HasColumnName("ID").HasColumnType("Int").UseIdentityColumn(1,1);
            modelBuilder.Entity<PromocaoRegra>().Property(p => p.Tipo).HasColumnName("Tipo").HasColumnType("tinyint");
            modelBuilder.Entity<PromocaoRegra>()
                .HasOne(p => p.Promocao)
                .WithMany(f => f.Regras)
                .HasForeignKey("IDPromocao")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);  

            modelBuilder.Entity<PromocaoRegra>()
                .HasMany(r => r.Vendas)
                .WithOne(v => v.PromocaoRegra)
                .HasForeignKey(f => f.IDPromocaoRegra)
                .OnDelete(DeleteBehavior.Cascade);  

            modelBuilder.Entity<PromocaoRegra>()
                .HasMany(r => r.Produtos)
                .WithOne(v => v.PromocaoRegra)
                .HasForeignKey(f => f.IDPromocaoRegra)
                .OnDelete(DeleteBehavior.Cascade);   

            /* PromocaoProduto*/
            modelBuilder.Entity<PromocaoProduto>().ToTable("PromocaoProduto");
            modelBuilder.Entity<PromocaoProduto>().Property(p => p.Id).HasColumnName("ID").HasColumnType("Int").UseIdentityColumn(1,1);
            modelBuilder.Entity<PromocaoProduto>()
                .HasOne(ho => ho.PromocaoRegra)
                .WithMany(wm => wm.Produtos)
                .HasForeignKey(fk => fk.IDPromocaoRegra)
                .OnDelete(DeleteBehavior.Cascade);  

            modelBuilder.Entity<PromocaoProduto>()
                .HasMany(hm => hm.Marcas)
                .WithOne(wo => wo.PromocaoProduto)
                .HasForeignKey(fk => fk.IDPromocaoProduto)
                .OnDelete(DeleteBehavior.Cascade);                 

            /* PromocaoProdutoMarca */
            modelBuilder.Entity<PromocaoProdutoMarca>().ToTable("PromocaoProdutoMarca");
            modelBuilder.Entity<PromocaoProdutoMarca>().Property(p => p.Id).HasColumnName("ID").HasColumnType("Int").UseIdentityColumn(1,1);
            modelBuilder.Entity<PromocaoProdutoMarca>()
                .HasOne(ho => ho.PromocaoProduto)
                .WithMany(wm => wm.Marcas)
                .HasForeignKey(fk => fk.IDPromocaoProduto)
                .OnDelete(DeleteBehavior.Cascade);              
                                          
            /* PromocaoVenda */
            modelBuilder.Entity<PromocaoVenda>().ToTable("PromocaoVenda");
            modelBuilder.Entity<PromocaoVenda>().Property(p => p.Id).HasColumnName("ID").HasColumnType("Int").UseIdentityColumn(1,1);
            modelBuilder.Entity<PromocaoVenda>()
                .HasOne(p => p.PromocaoRegra)
                .WithMany(v => v.Vendas)
                .IsRequired()
                .HasForeignKey(f => f.IDPromocaoRegra)
                .OnDelete(DeleteBehavior.Cascade);               
                            
            /* PromocaoPagamento */
            modelBuilder.Entity<PromocaoPagamento>().ToTable("PromocaoPagamento");
            modelBuilder.Entity<PromocaoPagamento>().Property(p => p.Id).HasColumnName("ID").HasColumnType("Int").UseIdentityColumn(1,1);
            modelBuilder.Entity<PromocaoPagamento>()
                .HasOne(p => p.PromocaoRegra)
                .WithMany(f => f.Pagamentos)
                .HasForeignKey("IDPromocaoRegra")
                .OnDelete(DeleteBehavior.Cascade);

            /* PromocaoCliente */
            modelBuilder.Entity<PromocaoCliente>().ToTable("PromocaoCliente");
            modelBuilder.Entity<PromocaoCliente>().Property(p => p.Id).HasColumnName("ID").HasColumnType("Int").UseIdentityColumn(1,1);
            modelBuilder.Entity<PromocaoCliente>()
                .HasOne(p => p.PromocaoRegra)
                .WithMany(f => f.Clientes)
                .HasForeignKey("IDPromocaoRegra")
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}