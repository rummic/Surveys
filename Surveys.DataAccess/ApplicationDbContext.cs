using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Surveys.Domain;

namespace Surveys.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyQuestionAnswer> SurveyQuestionAnswers { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
