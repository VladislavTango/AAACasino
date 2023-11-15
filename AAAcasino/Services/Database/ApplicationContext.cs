using AAAcasino.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AAAcasino.Services.Database
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<UserModel> userModels { get; set; } = null!;
        public DbSet<QuizModel> quizModels { get; set; } = null!;
        public DbSet<QuizNode> quizNodes {  get; set; } = null!;
        public DbSet<Answer> answers { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AAACasinoDB;Trusted_Connection=True;");
        }
        public UserModel? ExistUser(string? username, string? password)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                return (from um in db.userModels.ToList()
                        where username == um.Username && password == um.Password
                        select um).ToList().FirstOrDefault();
            }
        }
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
