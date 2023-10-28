using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherCountBot.Services
{
    public interface IService
    {
        /// <summary>
        /// Метод подсчета суммы чисел
        /// </summary>
        int Sum(ICollection<int> list);


        /// <summary>
        /// Метод подсчета количества символов в строке
        /// </summary>
        int Count(ICollection<string> list);
    }
}
