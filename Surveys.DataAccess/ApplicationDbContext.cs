using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Surveys.Domain;
using Surveys.Domain.Entities;

namespace Surveys.DataAccess
{
    public class ApplicationDbContext : DbContext, IRepository
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
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
