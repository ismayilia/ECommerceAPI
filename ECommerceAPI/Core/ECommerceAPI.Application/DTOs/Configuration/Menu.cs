﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs.Configuration
{
	public class Menu
	{
        public string Name { get; set; }
        public List<Action> Actions { get; set; }
    }
}
