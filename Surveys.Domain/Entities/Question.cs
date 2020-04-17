using System;
using System.ComponentModel.DataAnnotations;

namespace Surveys.Domain.Entities
{
    public class Question
    {
        [Key]
        public Guid Id { get; set; }

        public string Text { get; set; }
    }
}
