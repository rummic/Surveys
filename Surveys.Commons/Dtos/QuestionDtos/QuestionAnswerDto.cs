using System;
using System.Collections.Generic;
using System.Text;

namespace Surveys.Commons.Dtos.QuestionDtos
{
    public class QuestionAnswerDto
    {

        public string Text { get; set; }
        public Guid SurveyQuestionId { get; set; }
        public int AnswerType { get; set; }
        public string Answer { get; set; }
    }
}
