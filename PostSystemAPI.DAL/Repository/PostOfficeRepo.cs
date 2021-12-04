using PostSystemAPI.DAL.Context;
using PostSystemAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.DAL.Repository
{
    public class PostOfficeRepo: Repository<PostOffice>, IPostOfficeRepo
    {
        public PostOfficeRepo(PostSystemContext context)
            : base(context) { }
    }
}
