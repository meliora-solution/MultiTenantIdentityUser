﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedServices.Models.Account
{
    public class UserDetails
    {
        public string? Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
    }
}
