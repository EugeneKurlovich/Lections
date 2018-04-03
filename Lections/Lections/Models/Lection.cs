﻿using System;
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
        public DateTime dateCreate { set; get; }
        public DateTime dateUpdate { set; get; }
        public int UserId { set; get; }

        public User User { get; set; }

    }
}
