using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Surveys.Commons;
using Surveys.Commons.Dtos.SurveyQuestionAnswerDtos;

namespace Surveys.Domain.Entities
{
    public class SurveyQuestionAnswer
    {
        [Key]
        public Guid Id { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }
        public User User { get; set; }
        public string Answer { get; set; }
        public DateTime SubmittedAt { get; set; }

        
    }

   
}
