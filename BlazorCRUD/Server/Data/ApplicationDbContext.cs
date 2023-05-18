
namespace BlazorCRUD.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<SuperHero> SuperHeroes { get; set; }
        public DbSet<Comic> Comics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comic>().HasData(
                new Comic { Id = 1, Name = "Marvel" },
                new Comic { Id = 2, Name = "DC" }
                );

            modelBuilder.Entity<SuperHero>().HasData(
                new SuperHero
                {
                    Id = 1,
                    FirstName = "Peter",
                    LastName = "Parker",
                    HeroName = "Spiderman",
                    ComicId = 1,
                },
             new SuperHero
             {
                 Id = 2,
                 FirstName = "Bruce",
                 LastName = "Wayne",
                 HeroName = "Batman",
                 ComicId = 2
             });
        }
    }
}
