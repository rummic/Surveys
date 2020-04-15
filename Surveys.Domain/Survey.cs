using System;
using System.Collections.Generic;
using System.Text;

namespace Surveys.Domain
{
    public class Survey
    {
        public Guid Id { get; set; }
        public DateTime SubmittedAt { get; set; }
        public User User { get; set; }
    }
}
