using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly:InternalsVisibleTo("GenericSearchUnitTest")]
namespace GenericSearch
{
    public static class StringSearcher
    {
        /// <summary>
        /// Takes two arrays of strings and searches the first for the queries from the second. Will return true if any of the searched for elements are found.
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="queries"></param>
        /// <returns></returns>
        public static bool StringArrayOrSearch(string[] dataSource, string[] queries)
        {
            foreach (string str in dataSource)
            {
                foreach (string q in queries)
                {
                    if (str.ToLower().Contains(q.ToLower().Trim()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// If I wrote this correctly it should return true only if each query is found in the datasource.
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="queries"></param>
        /// <returns></returns>
        public static bool StringArrayAndSearch(string[] dataSource, string[] queries)
        {
            
            foreach (string q in queries)
            {
                bool MatchFound = false;
                foreach (string str in dataSource)
                {

                    if (str.ToLower().Contains(q.Trim().ToLower()))
                    {
                        MatchFound = true;
                        break;
                    }
                }
                if (MatchFound == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
