using MixQuestionAnswer.DAL.Repositories.Abstract;
using MixQuestionAnswer.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace MixQuestionAnswer.DAL.Repositories.Concrete
{
    public class RoleRepository : Repository<Role>,IRoleRepository
    {
        public RoleRepository(MainDbContext ctx) : base(ctx)
        {

        }
    }
}
