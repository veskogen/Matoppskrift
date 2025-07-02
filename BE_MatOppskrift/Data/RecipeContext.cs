using Microsoft.EntityFrameworkCore;
using BE_MatOppskrift.Models;

namespace BE_MatOppskrift.Data
{
    public class RecipeContext : DbContext
    {
        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
        {
        }

        // Add this parameterless constructor to allow EF Core to create the context without options
        // This is useful for migrations and other scenarios where options are not provided like during design time
        // Note: This constructor should not be used in production code, as it may lead to unintended consequences
        public RecipeContext() : base()
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        // Optional: Fluent API for more detailed model configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Recipe entity
            modelBuilder.Entity<Recipe>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configure Ingredient entity
            modelBuilder.Entity<Ingredient>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Ingredient>()
                .Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configure the relationship between Recipe and Ingredient
            modelBuilder.Entity<Ingredient>()
                .HasOne(i => i.Recipe)
                .WithMany(r => r.Ingredients)
                .HasForeignKey(i => i.RecipeId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    if (!optionsBuilder.IsConfigured)
    {
        optionsBuilder.UseSqlite("DataSource=BE_MatOppskrift.db");
    }
}
    }
}