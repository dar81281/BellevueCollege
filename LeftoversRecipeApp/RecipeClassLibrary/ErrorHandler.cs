﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeClassLibrary
{
    public class ErrorHandler : Exception
    {
        public Exception GetInnerException(Exception e)
        {
                return e.GetBaseException();          
        }
    }
}