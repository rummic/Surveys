using System;
using System.ComponentModel.DataAnnotations;

namespace Surveys.Domain.Entities
{
    public class SurveyQuestion
    {
        [Key]
        public Guid Id { get; set; }
        public Survey Survey { get; set; }
        public Question Question { get; set; }
    }
}
