using AAAcasino.Models;
using Microsoft.EntityFrameworkCore;

namespace AAAcasino.Services.Database
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<UserModel> userModels { get; set; } = null!;
        public DbSet<QuizModel> quizModels { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AAACasinoDB;Trusted_Connection=True;");
        }
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
