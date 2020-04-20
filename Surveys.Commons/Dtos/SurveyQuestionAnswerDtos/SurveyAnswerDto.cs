using System;
using System.Collections.Generic;

namespace Surveys.Commons.Dtos.SurveyQuestionAnswerDtos
{
    public class SurveyAnswerDto
    {
        public Guid SurveyQuestionId { get; set; }
        public string Answer { get; set; }
    }
}
