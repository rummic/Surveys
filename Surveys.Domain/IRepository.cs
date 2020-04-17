using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Surveys.Domain.Entities;

namespace Surveys.Domain
{
    public interface IRepository
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyQuestionAnswer> SurveyQuestionAnswers { get; set; }
        public DbSet<User> Users { get; set; }
        public Task SaveChangesAsync();
    }
}
