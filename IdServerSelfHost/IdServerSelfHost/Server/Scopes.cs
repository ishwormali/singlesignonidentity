﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdServerSelfHost.Server
{
    using IdentityServer3.Core.Models;
    public class Scopes
    {
        public static List<Scope> Get()
        {
            return new List<Scope>
            {
                new Scope {Name="api1" }
            };
        }
    }
}
