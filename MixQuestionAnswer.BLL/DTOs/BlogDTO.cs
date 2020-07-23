using System;
using System.Collections.Generic;
using System.Text;

namespace MixQuestionAnswer.BLL.DTOs
{
    public class BlogDTO : BaseDTO
    {
        public String Title { get; set; }
        public String Content { get; set; }
        public Guid UserId { get; set; }

        public UserDTO User { get; set; }
    }
}
