using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeClassLibrary
{
    public class ErrorHandler : Exception
    {
        public static Exception GetInnerException(Exception e)
        {
                //return e.GetBaseException();          
                while (e.InnerException != null)
            {
                return e.InnerException;
            }
            return e;
        }
    }
}
