using System;
using Microsoft.EntityFrameworkCore;
using Surveys.Domain;
using Surveys.Domain.Entities;
using System.Threading.Tasks;

namespace Surveys.DataAccess
{
    public class ApplicationDbContext : DbContext, IRepository
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var question1 = new Question { Id = Guid.NewGuid(), Text = "What is your company size?" };
            var question2 = new Question { Id = Guid.NewGuid(), Text = "What is your IT team size (if any)?" };
            var question3 = new Question { Id = Guid.NewGuid(), Text = "What is your growth ambition?" };
            var question4 = new Question { Id = Guid.NewGuid(), Text = "Do you own/maintain your own IT?" };
            var survey = new Survey { Id = Guid.NewGuid() };
            var surveyQuestion = new { Id = Guid.NewGuid(), SurveyId = survey.Id, QuestionId = question1.Id };

            modelBuilder.Entity<Question>().HasData(question1, question2, question3, question4);
            modelBuilder.Entity<Survey>().HasData(survey);
            modelBuilder.Entity<SurveyQuestion>().HasData(surveyQuestion);
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyQuestionAnswer> SurveyQuestionAnswers { get; set; }
        public DbSet<User> Users { get; set; }

        public Task SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
