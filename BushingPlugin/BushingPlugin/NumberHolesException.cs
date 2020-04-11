using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BushingPlugin
{
    /// <summary>
    /// Исключение, выбрасываемое, если параметр "Количество отверстий" задан некорректно
    /// </summary>
    public class NumberHolesException : ApplicationException
    {
        /// <summary>
        /// Конструктор исключения
        /// </summary>
        /// <param name="message"></param>
        public NumberHolesException(string message) : base(message)
        {
        }
    }
}
