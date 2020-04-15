using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Surveys.Domain
{
    public class SurveyQuestionAnswer
    {
        [Key]
        public Guid Id { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }
        public AnswerType AnswerType { get; set; }
        public string Answer { get; set; }
    }

    public enum AnswerType
    {
        Free,
        Flags
    }
}
