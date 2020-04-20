using System;
using System.Collections.Generic;
using System.Text;
using Surveys.Commons.Dtos.QuestionDtos;

namespace Surveys.Commons.Dtos.SurveyDtos
{
    public class SurveyAnswersDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<QuestionAnswerDto> QuestionAnswerDtos { get; set; }
    }
}
