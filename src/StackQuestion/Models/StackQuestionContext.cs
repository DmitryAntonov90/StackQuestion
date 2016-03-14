using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace StackQuestion.Models
{
    public class StackQuestionContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Answer> Answer { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<QuestionTag>()
                .HasKey(qt => new { qt.QuestionId, qt.TagId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-U2D1TCQ\\SQLEXPRESS;Database=StackQuestion;Trusted_Connection=True;Integrated Security=True;multipleactiveresultsets=True;");
        }
    }
}
