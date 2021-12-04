using PostSystemAPI.DAL.Context;
using PostSystemAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.DAL.Repository
{
    public class SenderRepo: Repository<Sender>, ISenderRepo
    {
        public SenderRepo(PostSystemContext context)
            : base(context) { }
    }
}
