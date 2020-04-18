using System;
using System.ComponentModel.DataAnnotations;

namespace Surveys.Domain.Entities
{
    public class Survey
    {
        [Key]
        public Guid Id { get; set; }
     
    }
}
