﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMauiApp.Models
{
    public class Group
    {
        public string Name { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}
