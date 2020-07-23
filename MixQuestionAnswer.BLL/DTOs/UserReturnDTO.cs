using System;
using System.Collections.Generic;
using System.Text;

namespace MixQuestionAnswer.BLL.DTOs
{
    public class UserReturnDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
