using Microsoft.EntityFrameworkCore;
using TrashRecolectorBackend.Entidades;

namespace TrashRecolectorBackend.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        //mapeo de datos para realizar el crud
        public DbSet<Login> Logins { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Comentarios> Comentarios { get; set; }
        public DbSet<Sector> Sectores { get; set; }


        //Muestra la relacción entre el login y el usuario mediante la obtención del Id
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>()
                .HasOne(l => l.Usuario)
                .WithOne()
                .HasForeignKey<Login>(l => l.UsuarioId);


            modelBuilder.Entity<Comentarios>()
                .HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId);


        }
    }
}
