using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lections.Models
{
    public class Likes
    {
        public int Id { get; set; }
        public int userStar { get; set; }

        public int UserId { get; set; }
        public int LectionId { get; set; }

   //     public Lection Lection { get; set; }
    }
}
