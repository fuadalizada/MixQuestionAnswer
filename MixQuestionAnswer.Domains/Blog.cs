using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MixQuestionAnswer.Domains
{
    public class Blog : BaseEntity
    {
        public String Title { get; set; }
        public String Content { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
