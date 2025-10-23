﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Graphql.DataAccess.Entities
{
    public class User
    {
        public string Id { get; set; }
        public bool Active { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string? Phone { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
    }
}
