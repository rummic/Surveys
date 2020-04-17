using System;
using System.ComponentModel.DataAnnotations;

namespace Surveys.Domain.Entities
{
    public class SurveyQuestionAnswer
    {
        [Key]
        public Guid Id { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }
        public User User { get; set; }
        public AnswerType AnswerType { get; set; }
        public string Answer { get; set; }
    }

    public enum AnswerType
    {
        Free,
        Flags
    }
}
