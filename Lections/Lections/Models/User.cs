﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lections.Models
{
    public class User
    {
        public int Id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool emailConfirmed { get; set;}
        public bool isAdmin  {get; set;}

        public User()
        {
            emailConfirmed = false;
            isAdmin = false;
        }
    }
}
