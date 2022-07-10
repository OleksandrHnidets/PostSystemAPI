using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.Domain.ViewModels
{
    public class PostOfficeReadView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string CityName { get; set; }
        public int PostOfficeBalance { get; set; }
        public int CountOfdeliveries { get; set; }
        public CityReadView City { get; set; }
    }
}
