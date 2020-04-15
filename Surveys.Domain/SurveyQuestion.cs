using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Surveys.Domain
{
    public class SurveyQuestion
    {
        [Key]
        public Guid Id { get; set; }
        public Survey Survey { get; set; }
        public Question Question { get; set; }
    }
}
