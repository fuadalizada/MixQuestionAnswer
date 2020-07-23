using MixQuestionAnswer.DAL.Repositories.Abstract;
using MixQuestionAnswer.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MixQuestionAnswer.DAL.Repositories.Concrete
{
    public class BlogRepository : Repository<Blog>,IBlogRepository
    {
        public BlogRepository(MainDbContext ctx) :base(ctx)
        {
        }
    }
}
