using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.Domain.ViewModels
{
    public class PostOfficeView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CityId { get; set; }
    }

    public class PostOfficeForCreate
    {
        public string Id { get;set; }
        public string Name { get;set; }
    }
}
