﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public  class Service
    {
       
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

    }
}
