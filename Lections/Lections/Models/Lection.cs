using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lections.Models
{
    public class Lection
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string smallDescription { set; get; }
        public string text { set; get; }
        public int stars { set; get; }
        public int userId { set; get; }
    }
}
