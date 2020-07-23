using MixQuestionAnswer.DAL.Repositories.Abstract;
using MixQuestionAnswer.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace MixQuestionAnswer.DAL.Repositories.Concrete
{
    public class UserRepository : Repository<User>,IUserRepository
    {
        public UserRepository(MainDbContext ctx) : base(ctx)
        {

        }
    }
}
