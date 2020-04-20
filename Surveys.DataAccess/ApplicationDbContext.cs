using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Surveys.Domain;
using Surveys.Domain.Entities;
using System.Threading.Tasks;
using Surveys.Commons.Helpers;

namespace Surveys.DataAccess
{
    public class ApplicationDbContext : DbContext, IRepository
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var salt = PasswordHelper.CreateSalt();
            var user = new
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "john@john.com",
                Role = "Admin",
                Salt = salt,
                Password = PasswordHelper.HashPassword("john123", salt)
            };

            var question1 = new Question { Id = Guid.NewGuid(), Text = "What is your company size?" };
            var question2 = new Question { Id = Guid.NewGuid(), Text = "What is your IT team size (if any)?" };
            var question3 = new Question { Id = Guid.NewGuid(), Text = "What is your growth ambition?" };
            var question4 = new Question { Id = Guid.NewGuid(), Text = "Do you own/maintain your own IT?" };
            var questions = new List<Question>
            {
                question1, question2, question3, question4
            };
            var survey = new Survey { Id = Guid.NewGuid(),Name = "Main Survey", CreatorEmail = "john@john.com" };

            var surveyQuestions = new List<object> {
                new  { Id = Guid.NewGuid(), SurveyId = survey.Id, QuestionId = question1.Id },
                new  { Id = Guid.NewGuid(), SurveyId = survey.Id, QuestionId = question2.Id },
                new  { Id = Guid.NewGuid(), SurveyId = survey.Id, QuestionId = question3.Id },
                new  { Id = Guid.NewGuid(), SurveyId = survey.Id, QuestionId = question4.Id }};

            modelBuilder.Entity<User>().HasData(user);
            modelBuilder.Entity<Question>().HasData(questions);
            modelBuilder.Entity<Survey>().HasMany(x => x.SurveyQuestions).WithOne(x => x.Survey);
            modelBuilder.Entity<Survey>().HasData(survey);
            modelBuilder.Entity<SurveyQuestion>().HasData(surveyQuestions);
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
