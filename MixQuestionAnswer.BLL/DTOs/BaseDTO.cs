using System;
using System.Collections.Generic;
using System.Text;

namespace MixQuestionAnswer.BLL.DTOs
{
    public class BaseDTO
    {
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
