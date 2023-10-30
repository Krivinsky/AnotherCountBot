using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherCountBot.Services
{
    public class WorkService : IService
    {
        public int Count(string str)
        {
            return str.Length;
        }

        public int Sum(string str)
        {
            List<string> strings = str.Split(' ').ToList();
            var ints = strings.Select (i => Int32.Parse(i));
            int result = ints.Sum();
            return result;
        }
    }
}
