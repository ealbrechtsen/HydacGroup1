﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPersistence.Model
{
    public class Department
    {
        public string Name { get; set; }
        

        public Department(string Name) 
        { 
            this.Name = Name;
        }
    }
}
