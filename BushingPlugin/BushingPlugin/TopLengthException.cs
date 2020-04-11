using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BushingPlugin
{
    /// <summary>
    /// Исключение, выбрасываемое, если параметр "Длина верхней части втулки" задан некорректно
    /// </summary>
    public class TopLengthException : ApplicationException
    {
        /// <summary>
        /// Конструктор исключения
        /// </summary>
        /// <param name="message"></param>
        public TopLengthException(string message) : base(message)
        {
        }
    }
}
