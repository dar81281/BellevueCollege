﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeClassLibrary
{
    public class Dessert : Recipe
    {

        public override string ToString()
        {
            return "Dessert - " + base.ToString();
        }
    }
}
